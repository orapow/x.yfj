using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using X.Core.Plugin;
using X.Core.Utility;
using X.Wx.Com;

namespace X.Wx.Mp
{
    /// <summary>
    /// 消息
    /// </summary>
    public class Msg
    {
        public static MsgObj Get(string tk_xml, string sign, string nonce, string timestamp)
        {
            try
            {
                var xml = Crypt.DecryptMsg(Open.appid, sign, timestamp, nonce, tk_xml);
                var obj = new MsgObj(xml);
                return obj;
            }
            catch (WxExcep wex)
            {
                Loger.Error(wex);
                return null;
            }
        }

        ///message/custom/send?access_token=ACCESS_TOKEN
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="mg"></param>
        //public static void Send(MsgBase msg) { }
        //public static void Reply(MsgBase msg) { }
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="tk_xml"></param>
        /// <param name="sign"></param>
        /// <param name="nonce"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        //public static MsgBase GetMsg(string tk_xml, string sign, string nonce, string timestamp)
        //{

        //}
        /// <summary>
        /// 加密消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public static string EncodeMsg(MsgBase msg)
        //{
        //    var xml = Serialize.ToXml(msg);
        //    Crypt.EncryptMsg(xml, Tools.GetGreenTime(""), Tools.GetRandRom(8, 3), ref xml);
        //    return xml;
        //}

        public class MsgObj
        {
            public string FromUserName { get { return GetString("FromUserName"); } }
            public string ToUserName { get { return GetString("ToUserName"); } }
            public int CreateTime { get { return GetInt("CreateTime"); } }
            public string MsgType { get { return GetString("MsgType"); } }

            private Dictionary<string, string> dict = new Dictionary<string, string>();

            public string GetString(string name)
            {
                if (dict.ContainsKey(name)) return dict[name];
                return "";
            }

            public int GetInt(string name)
            {
                if (dict.ContainsKey(name)) return dict[name] == null ? 0 : int.Parse(dict[name]);
                return 0;
            }

            /// <summary>
            /// Initializes a new instance of the MsgObj class.
            /// </summary>
            public MsgObj(string xml)
            {
                if (string.IsNullOrEmpty(xml)) return;
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                var root = doc.FirstChild;
                foreach (XmlNode n in root.ChildNodes)
                {
                    var v = "";
                    if (n.NodeType == XmlNodeType.CDATA) v = n.FirstChild.InnerText;
                    else v = n.InnerText;
                    dict.Add(n.Name, v);
                }
            }

            public string ToXml()
            {
                var sb_str = new StringBuilder();
                sb_str.Append("<xml>");
                foreach (var k in dict.Keys)
                {
                    if ("CreateTime|Latitude|Longitude|Precision".IndexOf(k) < 0) sb_str.Append("<" + k + "><![CDATA[" + dict[k] + "]]></" + k + ">");
                    else sb_str.Append("<" + k + ">" + dict[k] + "</" + k + ">");
                }
                sb_str.Append("</xml>");
                return Crypt.EncryptMsg(Open.appid, sb_str.ToString(), Tools.GetGreenTime(""), Tools.GetRandRom(9, 3));
            }

            public void AddValue(string name, string value)
            {
                dict.Add(name, value);
            }

        }
    }
}
