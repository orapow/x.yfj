using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;
using System.Web;

namespace X.App.Apis.wx{
    public class reg : _wx {
        public String tel { get; set; }
        public String newPswd { get; set; }
        public String checkCode { get; set; }
        protected override bool nd_user {
            get {
                return false;
            }
        }
        protected override XResp Execute() {
            if (String.IsNullOrWhiteSpace(tel) || String.IsNullOrWhiteSpace(newPswd)
                || String.IsNullOrWhiteSpace(checkCode))
                throw new XExcep("T输入不正确");
            if (!checkCode.Equals(CacheHelper.Get<string>("sms.code." + tel)))
                throw new XExcep("T短信验证码不正确");
            else
                CacheHelper.Remove("sms.code." + tel);
            if (DB.x_user.FirstOrDefault(o => o.tel == tel) != null)
                throw new XExcep("T该手机号已被注册");
            var user = new x_user();
            user.tel = tel;
            user.pwd=Secret.MD5(newPswd);
            user.balance = 0;
            user.exp = user.used_exp = 0;
            user.ctime = DateTime.Now;
            cu = user;
            cu.ukey = Secret.MD5(Guid.NewGuid().ToString());//保存cookie状态,这个key用以识别用户身份
            DB.x_user.InsertOnSubmit(user);
            SubmitDBChanges();
            Context.Response.SetCookie(new HttpCookie("cu_key", cu.ukey));//保存cookie状态
            return new XResp();
        }
    }
}
