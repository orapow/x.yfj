using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user
{
    public class login : _u
    {
        /// <summary>
        /// 用户名、邮箱、手机号
        /// </summary>
        [ParmsAttr(req = true, name = "用户名")]
        public string uid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [ParmsAttr(req = true, name = "用户密码")]
        public string pwd { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [ParmsAttr(len = "5", name = "图形验证码")]
        public string code { get; set; }

        protected override bool needuser
        {
            get
            {
                return false;
            }
        }

        protected override XResp Execute()
        {
            var yzm = CacheHelper.Get<string>("code." + uid);
            if (yzm == null || yzm != code) throw new XExcep("T验证码不正确");

            var c = CacheHelper.Get<int>("u." + uid + ".lc");
            if (c >= 5) throw new XExcep("T登陆错误次数过多，请过会再登陆");

            var r = new logresp();

            var u = DB.x_user.FirstOrDefault(o => o.uid == uid || o.email == uid || o.tel == uid);
            if (u == null || u.pwd != pwd)
            {
                c++;
                r.lc = 5 - c;
                CacheHelper.Save("u." + uid + ".lc", yzm, 60 * 5);
                throw new XExcep("T用户名或密码错误，请检查是否正确");
            }

            CacheHelper.Remove("u." + uid + ".lc");

            var ukey = r.ukey = Secret.MD5(Guid.NewGuid().ToString());
            CacheHelper.Save("u.cu." + ukey, u);

            return r;
        }
        class logresp : XResp
        {
            public string ukey { get; set; }
            public int lc { get; set; }
        }
    }
}
