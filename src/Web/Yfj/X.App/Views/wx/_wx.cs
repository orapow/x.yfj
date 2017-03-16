using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using X.App.Com;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;

namespace X.App.Views.wx
{
    public class _wx : xview
    {
        protected virtual bool nd_user { get { return true; } }
        protected string opid { get; set; }
        protected x_user cu { get; set; }
        protected long city_id { get; set; }
        public string city_name { get; set; }
        protected bool isWx { get; set; }

        private void initCity()
        {
            var c = Tools.GetHttpData("http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + Tools.GetClientIP(), Encoding.GetEncoding("GB2312")); //1 - 1 - 1  中国 上海  上海
            x_dict city = null;
            if (!string.IsNullOrEmpty(c) && c[0] == '1' && c.Length >= 6)
            {
                var cn = c.TrimEnd('\t').Split('\t').LastOrDefault();
                city = DB.x_dict.FirstOrDefault(o => o.name == cn && o.code == "sys.city");
            }
            if (city == null) city = DB.x_dict.FirstOrDefault(o => o.name == "长沙" && o.code == "sys.city");
            city_id = long.Parse(city.value);
            city_name = city.name;
        }
        private void initUser()
        {
            if (!isWx) if (Context.Request.RawUrl != "/wx/login.html") Context.Response.Redirect("/wx/login.html");

            var code = GetReqParms("code");
            if (string.IsNullOrEmpty(code)) toWxUrl("snsapi_base");

            var opid = "";
            if (!string.IsNullOrEmpty(code))
            {
                var tk = Wx.GetWebToken(cfg.wx_appid, cfg.wx_scr, code);
                if (!string.IsNullOrEmpty(tk.errcode)) toWxUrl("snsapi_base");
                opid = tk.openid;
            }

            cu = DB.x_user.FirstOrDefault(o => o.wx_opid == opid);

            if (cu == null) cu = new x_user() { ctime = DateTime.Now, etime = DateTime.Now, wx_opid = opid, balance = 0, group = 1, exp = 0, used_exp = 0, invter = 0 };

            if (cu.id == 0 || cu.etime.Value.AddDays(7) < DateTime.Now)
            {
                var uk = Wx.GetToken(cfg.wx_appid, cfg.wx_scr);
                var us = Wx.User.GetUserInfo(cu.wx_opid, uk);
                cu.nickname = us.nickname;
                cu.headimg = us.headimgurl;
                cu.sex = us.sex == 1 ? "男" : us.sex == 2 ? "女" : "未知";
            }

            cu.ukey = Secret.MD5(Guid.NewGuid().ToString());
            if (cu.id == 0) DB.x_user.InsertOnSubmit(cu);

            Context.Response.SetCookie(new HttpCookie("cu_key", cu.ukey));
        }
        private void toWxUrl(string scope)
        {
            var url = Context.Request.RawUrl.Split('?')[0];
            Context.Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + cfg.wx_appid + "&redirect_uri=" + Context.Server.UrlEncode("http://" + cfg.domain + url) + "&response_type=code&scope=" + scope + "&state=" + Tools.GetRandRom(6, 3) + "#wechat_redirect");
            Context.Response.End();
        }
        private void initWx()
        {
            var ts = Tools.GetGreenTime("");
            var no = Tools.GetRandRom(6);

            dict.Add("wx_appid", cfg.wx_appid);
            dict.Add("wx_ts", ts);
            dict.Add("wx_no", no);

            var tk = Wx.GetToken(cfg.wx_appid, cfg.wx_scr);
            var tick = Wx.GetJsTicket(tk);
            if (string.IsNullOrEmpty(tk)) tick = Wx.GetJsTicket(Wx.GetToken(cfg.wx_appid, cfg.wx_scr, true), true);

            var dt = new List<string>();
            dt.Add("noncestr=" + no);
            dt.Add("jsapi_ticket=" + tick);
            dt.Add("timestamp=" + ts);
            dt.Add("url=http://" + cfg.domain + Context.Request.RawUrl);
            dt.Sort();

            var sign = Secret.SHA1(string.Join("&", dt));

            dict.Add("wx_sign", sign.ToLower());
        }

        protected override void InitView()
        {
            base.InitView();

            var cu_key = GetReqParms("cu_key");
            if (!string.IsNullOrEmpty(cu_key)) cu = DB.x_user.FirstOrDefault(o => o.ukey == cu_key);

            isWx = Context.Request.UserAgent.Contains("MicroMessenger");

            initCity();

            if (nd_user && cu == null) initUser();

            if (cu != null)
            {
                cu.etime = DateTime.Now;
                SubmitDBChanges();
            }

        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("iswx", isWx);
            dict.Add("cu", cu);
            dict.Add("cityid", city_id);
            dict.Add("city_name", city_name);
            if (isWx) initWx();
        }
    }
}
