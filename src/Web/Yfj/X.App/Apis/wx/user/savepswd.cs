using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx.user {
    public class savepswd : _wx {
        public String tel { get; set; }
        public String newPswd { get; set; }
        public String checkCode { get; set; }

        protected override XResp Execute() {
            if (String.IsNullOrWhiteSpace(tel) || String.IsNullOrWhiteSpace(newPswd)
                || String.IsNullOrWhiteSpace(checkCode))
                throw new XExcep("T输入不正确");
            if (!checkCode.Equals(CacheHelper.Get<string>("sms.code." + tel)))
                throw new XExcep("T短信验证码不正确");
            else
                CacheHelper.Remove("sms.code." + tel);
            cu.pwd = Secret.MD5(newPswd);
            SubmitDBChanges();
            return new XResp();
        }
    }
}
