using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using X.App.Com;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.com
{
    public class sendcode : xapi
    {
        [ParmsAttr(name = "验证码", req = true)]
        public string code { get; set; }
        [ParmsAttr(name = "手机号", req = true)]
        public string tel { get; set; }
        protected override XResp Execute()
        {
            var sc = CacheHelper.Get<string>("img.code." + tel);
            if (sc != code) throw new XExcep("0x0005");
            CacheHelper.Remove("img.code." + tel);
            try
            {
                var smscode = Tools.GetRandRom(4, 1);
                var re = Sms.SendSms(tel, "您的验证码为：" + smscode + "，请保密。");
                var m = new Regex(",([\\d+])\n").Match(re);
                if (m.Success && m.Groups[1].Value == "0")
                {
                    CacheHelper.Save("sms.code." + tel, smscode, 5 * 60);
                    return new XResp();
                }
                else if (m.Groups.Count > 0) throw new Exception(m.Groups[1].Value);
                else throw new Exception(re);
            }
            catch (Exception e)
            {
                throw new XExcep("0x0006", e.Message);
            }
        }
    }
}
