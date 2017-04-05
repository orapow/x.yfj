using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var key = GetReqParms("code.send");
            var sc = CacheHelper.Get<string>("code." + key);
            CacheHelper.Remove("code." + key);
            if (sc != code) throw new XExcep("T图片验证码不正确");

            var t = true;
            try
            {
                t = Sms.SendCode(tel, Tools.GetRandRom(4, 1), "签名", "SMS_58210145");
            }
            catch
            {
                t = false;
            }
            if (!t) throw new XExcep("T短信发送失败");
            return new XResp();
        }
    }
}
