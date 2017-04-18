using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;

namespace X.App.Apis.wx {
    public class login : _wx {
        public string tel { get; set; }
        public string code { get; set; }

        public String password { get; set; }
        protected override bool nd_user {
            get {
                return false;
            }
        }
        protected override Web.Com.XResp Execute() {
            var cachecode = CacheHelper.Get<string>("sms.code." + tel);
            if (code != null && !String.IsNullOrWhiteSpace(code)) { 
                //验证码登陆模式
                var user = DB.x_user.FirstOrDefault(o => o.tel == tel);
                if (user == null)
                    throw new XExcep("T该电话用户不存在");
                if (!code.Equals(cachecode))
                    throw new XExcep("T验证码错误");
                CacheHelper.Remove("sms.code." + tel);
                cu = user;//登陆成功
            } else if (password != null && !String.IsNullOrWhiteSpace(password)) { 
                //密码登录模式
                var user = DB.x_user.FirstOrDefault(o => o.tel == tel);
                if (user == null)
                    throw new XExcep("T该电话用户不存在");
                if (!user.pwd.Equals(Secret.MD5(password)))
                    throw new XExcep("T密码错误");
                cu = user;//登陆成功
            }
            cu.ukey = Secret.MD5(Guid.NewGuid().ToString());
            SubmitDBChanges();
            Context.Response.SetCookie(new HttpCookie("cu_key", cu.ukey));//保存cookie状态
            return new Web.Com.XResp();
        }
    }
}
