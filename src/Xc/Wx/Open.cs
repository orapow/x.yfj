using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using X.Core.Cache;
using X.Core.Plugin;
using X.Core.Utility;
using X.Wx.Com;
using X.Wx.Mp.Com;

namespace X.Wx
{
    public class Open
    {
        private static string ticket_file = HttpContext.Current.Server.MapPath("/dat/wxmp.x");
        /// <summary>
        /// 微信推送Key
        /// </summary>
        private static string verify_ticket
        {
            get
            {
                var ticket = CacheHelper.Get<string>("wx.verify_ticket");
                if (string.IsNullOrEmpty(ticket)) ticket = File.ReadAllText(ticket_file);
                CacheHelper.Save("wx.verify_ticket", ticket, 5 * 60 * 1000);//有效期 10 分钟 此处存5分钟
                return ticket;
            }
        }

        public static string appid = "wx45d422d419115eb5";
        private static string appsecret = "7c5643e3c1c192ac342bf1d1e9d78d14";

        /// <summary>
        /// 第三方令牌
        /// 需要做缓存
        /// </summary>
        /// <returns></returns>
        static string component_access_token()
        {
            var acc_token = CacheHelper.Get<string>("wx.component_access_token");
            if (!string.IsNullOrEmpty(acc_token)) return acc_token;

            var dict = new Dictionary<string, string>();
            dict.Add("component_appid", appid);
            dict.Add("component_appsecret", appsecret);
            dict.Add("component_verify_ticket", verify_ticket);

            var url = "https://api.weixin.qq.com/cgi-bin/component/api_component_token";

            var json = Tools.PostHttpData(url, Serialize.ToJson(dict));
            var token = Serialize.FromJson<component_token>(json);

            if (token == null || string.IsNullOrEmpty(token.component_access_token)) throw new WxExcep(json);

            CacheHelper.Save("wx.component_access_token", token.component_access_token, 7200);

            return token.component_access_token;
        }
        /// <summary>
        /// 获取预授权码
        /// </summary>
        /// <returns></returns>
        static string api_create_preauthcode()
        {
            var tk = component_access_token();
            if (string.IsNullOrEmpty(tk)) return "";

            var dict = new Dictionary<string, string>();
            dict.Add("component_appid", appid);

            var url = "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token=" + tk;

            var json = Tools.PostHttpData(url, Serialize.ToJson(dict));
            var pre = Serialize.FromJson<create_preauthcode>(json);

            if (pre == null || string.IsNullOrEmpty(pre.pre_auth_code)) throw new WxExcep(json);

            return pre.pre_auth_code;

        }

        /// <summary>
        /// 设置验证Key
        /// </summary>
        /// <param name="ticket"></param>
        public static void SetVerify_Ticket(string verify_ticket)
        {
            File.WriteAllText(ticket_file, verify_ticket);
            CacheHelper.Save("wx.verify_ticket", verify_ticket, 5 * 60 * 1000);//有效期 10 分钟 此处存5分钟
        }

        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="tourl"></param>
        /// <returns></returns>
        public static string Get_AuthUrl(string tourl)
        {
            return "https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid=" + appid + "&pre_auth_code=" + api_create_preauthcode() + "&redirect_uri=" + tourl;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="tk_xml">
        /// 消息 xml
        /// </param>
        /// <returns></returns>
        public static Push Revice(string tk_xml, string sign, string nonce, string timestamp)
        {
            try
            {
                var xml = Crypt.DecryptMsg(appid, sign, timestamp, nonce, tk_xml);
                return new Push(xml);
            }
            catch (WxExcep wex)
            {
                Loger.Error(wex);
            }
            return null;
        }

        #region RegionName
        /// <summary>
        /// 获取公众号授权信息
        /// </summary>
        /// <param name="auth_code">
        /// 查询授权码
        /// </param>
        /// <returns></returns>
        public static MpAuth_Info Get_AuthInfo(string auth_code)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("component_appid", appid);
            dict.Add("authorization_code", auth_code);

            var json = Api.PostData("component/api_query_auth?component_access_token=" + component_access_token(), Serialize.ToJson(dict));
            var info = Serialize.FromJson<MpAuth_Info>(json);
            if (info == null || !string.IsNullOrEmpty(info.errmsg)) throw new WxExcep(json);
            return info;
        }

        /// <summary>
        /// 获取公众号信息
        /// </summary>
        /// <param name="auth_code"></param>
        public static MpInfo Get_MpInfo(string auth_appid)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("component_appid", appid);
            dict.Add("authorizer_appid", auth_appid);

            var json = Api.PostData("component/api_get_authorizer_info?component_access_token=" + component_access_token(), Serialize.ToJson(dict));
            var info = Serialize.FromJson<MpInfo>(json);
            if (info == null || !string.IsNullOrEmpty(info.errmsg)) throw new WxExcep(json);
            return info;
        }

        /// <summary>
        /// 获取、刷新公众号授权令牌
        /// </summary>
        /// <param name="auth_appid"></param>
        /// <param name="re_token"></param>
        /// <returns></returns>
        public static MpAuth_Token Get_MpAuth_Token(string auth_appid, string re_token)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("component_appid", appid);
            dict.Add("authorizer_appid", auth_appid);
            dict.Add("authorizer_refresh_token", re_token);

            var json = Api.PostData("component/api_authorizer_token?component_access_token=" + component_access_token(), Serialize.ToJson(dict));
            var tk = Serialize.FromJson<MpAuth_Token>(json);

            if (tk == null || !string.IsNullOrEmpty(tk.errmsg)) throw new WxExcep(json);
            return tk;
        }
        #endregion
        #region

        class component_token : msgbase
        {
            public string component_access_token { get; set; }
            public int expires_in { get; set; }
        }
        class create_preauthcode : msgbase
        {
            public string pre_auth_code { get; set; }
            public int expires_in { get; set; }
        }
        #endregion

        public class Push
        {
            public string AppId { get { return GetValue("AppId"); } }
            public string CreateTime { get { return GetValue("CreateTime"); } }
            public string InfoType { get { return GetValue("InfoType"); } }

            private Dictionary<string, string> ps = new Dictionary<string, string>();

            /// <summary>
            /// Initializes a new instance of the Push class.
            /// </summary>
            public Push(string xml)
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                var root = doc.FirstChild;
                foreach (XmlNode n in root.ChildNodes)
                {
                    var v = "";
                    if (n.NodeType == XmlNodeType.CDATA) v = n.FirstChild.InnerText;
                    else v = n.InnerText;
                    ps.Add(n.Name, v);
                }
            }

            public string GetValue(string name)
            {
                if (ps.ContainsKey(name)) return ps[name];
                return "";
            }
        }

        [XmlType("xml")]
        public class Un_Auth
        {
            public string AppId { get; set; }
            public string CreateTime { get; set; }
            public string InfoType { get; set; }
            public string AuthorizerAppid { get; set; }
        }

        /// <summary>
        /// 权限对象
        /// </summary>
        public class Func_Category
        {
            /// <summary>
            /// 1、消息与菜单权限集
            /// 2、用户管理权限集
            /// 3、帐号管理权限集
            /// 4、网页授权权限集
            /// 5、微信小店权限集
            /// 6、多客服权限集
            /// 7、业务通知权限集
            /// 8、微信卡券权限集
            /// 9、微信扫一扫权限集
            /// 10、微信连WIFI权限集
            /// 11、素材管理权限集
            /// 12、摇一摇周边权限集
            /// 13、微信门店权限集
            /// </summary>
            public int id { get; set; }
        }

        /// <summary>
        /// 公众号授权令牌
        /// </summary>
        public class MpAuth_Token : msgbase
        {
            public string authorizer_access_token { get; set; }
            public string authorizer_refresh_token { get; set; }
            public int expires_in { get; set; }
        }

        /// <summary>
        /// 公众号信息
        /// </summary>
        public class MpInfo : msgbase
        {
            public Authorizer authorizer_info { get; set; }
            public Authorization authorization_info { get; set; }
            public class Authorizer
            {
                public string nick_name { get; set; }
                public string head_img { get; set; }
                public string user_name { get; set; }
                public string alias { get; set; }
                public string qrcode_url { get; set; }
                public Func_Category service_type_info { get; set; }
                public Func_Category verify_type_info { get; set; }
                public Business business_info { get; set; }
            }

            public class Business
            {
                /// <summary>
                /// 是否开通微信门店功能
                /// 0、代表未开通
                /// 1、代表已开通
                /// </summary>
                public int open_store { get; set; }
                /// <summary>
                /// 是否开通微信扫商品功能
                /// 0、代表未开通
                /// 1、代表已开通
                /// </summary>
                public int open_scan { get; set; }
                /// <summary>
                /// 是否开通微信支付功能
                /// 0、代表未开通
                /// 1、代表已开通
                /// </summary>
                public int open_pay { get; set; }
                /// <summary>
                /// 是否开通微信卡券功能
                /// 0、代表未开通
                /// 1、代表已开通
                /// </summary>
                public int open_card { get; set; }
                /// <summary>
                /// 是否开通微信摇一摇功能
                /// 0、代表未开通
                /// 1、代表已开通
                /// </summary>
                public int open_shake { get; set; }
            }
        }

        /// <summary>
        /// 授权信息对象
        /// </summary>
        public class Authorization
        {
            public string appid { get; set; }
            /// <summary>
            /// 授权appid
            /// </summary>
            public string authorizer_appid { get; set; }
            /// <summary>
            /// 授权令牌
            /// </summary>
            public string authorizer_access_token { get; set; }
            /// <summary>
            /// 授权刷新令牌
            /// </summary>
            public string authorizer_refresh_token { get; set; }
            /// <summary>
            /// 过期时间 2 小时
            /// </summary>
            public int expires_in { get; set; }
            /// <summary>
            /// 权限集
            /// </summary>
            public List<Func_Category> func_info { get; set; }
        }

        /// <summary>
        /// 公众号授权信息
        /// </summary>
        public class MpAuth_Info : msgbase
        {
            /// <summary>
            /// 授权信息
            /// </summary>
            public Authorization authorization_info { get; set; }
        }
    }
}
