using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using X.Core.Utility;

namespace X.App.Com
{
    public class Sms
    {
        public static bool SendCode(string to, string code,String key, String sign,string tpid)
        {
            var dc = new Dictionary<string, string>();
            dc.Add("Format", "JSON");
            dc.Add("Version", "2016-09-27");
            dc.Add("SignatureMethod", "HMAC-SHA1");
            dc.Add("SignatureNonce", Tools.GetRandRom(16, 3));
            dc.Add("SignatureVersion", "1.0");

            //dc.Add("AccessKeyId", "LTAI2UoL4CNU5myd");
            dc.Add("Signature", sign);

            dc.Add("Timestamp", DateTime.Now.AddHours(-8).ToString("yyyy-MM-ddTHH:mm:ssZ"));
            //新增AccessKeyId
            dc.Add("AccessKeyId", key);
            dc.Add("Action", "SingleSendSms");
            dc.Add("SignName", "fwerwerw");
            dc.Add("TemplateCode", tpid);
            dc.Add("RecNum", to);
            dc.Add("ParamString", "{\"no\":\"" + code + "\"}");
            dc.Add("Signature", getSign(dc));

            var url = new StringBuilder("https://sms.aliyuncs.com/?");
            foreach (var d in dc) url.Append(d.Key + "=" + d.Value + "&");
            var rsp = Tools.GetHttpData(url.ToString());

            return true;
        }

        static string getSign(Dictionary<string, string> ps)
        {
            var parms = ps.OrderBy(o => o.Key);
            var sb_str = new StringBuilder("GET&%2F&");
            foreach (var p in parms)
            {
                if (p.Key != "AccessKeyId") sb_str.Append("%26");
                sb_str.Append(HttpUtility.UrlEncode(p.Key) + "%3D" + UrlEncode(UrlEncode(p.Value)));
            }
            return sha1(sb_str.ToString());
        }
        static string UrlEncode(string value)
        {
            StringBuilder result = new StringBuilder();
            string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1) result.Append(symbol);
                else result.Append('%' + String.Format("{0:X2}", (int)symbol));
            }
            return result.ToString();
        }
        static string sha1(string str)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes("f5iuwkI1lId3ThcfDS2HyfbKbiAsJ9&");
            byte[] dataBuffer = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }

    }
}
