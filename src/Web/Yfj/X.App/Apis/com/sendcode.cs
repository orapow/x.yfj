using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.com {
    public class sendcode : xapi {
        [ParmsAttr(name = "验证码", req = true)]
        public string code { get; set; }
        [ParmsAttr(name = "手机号", req = true)]
        public string tel { get; set; }
        protected override XResp Execute() {
            var key = GetReqParms("code.send");
            var sc = CacheHelper.Get<string>("code." + key);
            if (sc != code) throw new XExcep("T图片验证码不正确");
            CacheHelper.Remove("code." + key);
            var t = true;
            var smscode = Tools.GetRandRom(4, 1);
            try {
                //CacheHelper.Save("sms.code." + tel, smscode);
                // key ::  f5iuwkI1lId3ThcfDS2HyfbKbiAsJ9/!作废!/
                //secret:   S0DiD9VFx5gC4fhnCKWy3wMONy3J7z
                //key: LTAIP8aKCv1LUo4P
                //t = Sms.SendCode(tel, Tools.GetRandRom(4, 1), "LTAIP8aKCv1LUo4P", "优辅家", "SMS_63315079");
                //t = Sms.sendSmsViaSDK("优辅家", "LTAIP8aKCv1LUo4P", "S0DiD9VFx5gC4fhnCKWy3wMONy3J7z", "SMS_63315079",tel, Tools.GetRandRom(4, 1));
                t = Sms.sendSmsViaMnsSDK("优辅家", "LTAIP8aKCv1LUo4P", "S0DiD9VFx5gC4fhnCKWy3wMONy3J7z", "SMS_63315079", tel, Tools.GetRandRom(4, 1), "https://1625487773132895.mns.cn-hangzhou.aliyuncs.com/", "sms.topic-cn-hangzhou");
            } catch(Exception e) {

                t = false;
            }
            if (!t) throw new XExcep("T短信发送失败");
            return new XResp() { msg = smscode };
        }
    }
}
