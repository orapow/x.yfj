using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user
{
    public class reg : _u
    {
        /// <summary>
        /// 用户帐号
        /// </summary>
        [ParmsAttr(req = true, len = "8,", name = "用户名")]
        public string uid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [ParmsAttr(req = true, name = "登陆密码")]
        public string pwd { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 邀请人
        /// </summary>
        public string invt { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [ParmsAttr(req = true, name = "图片验证码", len = "5")]
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
            var uc = DB.x_user.Count(o => o.uid == uid);
            if (uc > 0) throw new XExcep("T用户名已经存在");

            if (!string.IsNullOrEmpty(email))
            {
                uc = DB.x_user.Count(o => o.email == email);
                if (uc > 0) throw new XExcep("T邮箱已经绑定到平台用户，请更换，如果忘记密码可用此邮箱找回");
            }

            var u = new x_user()
            {
                uid = uid,
                pwd = pwd,
                email = email,
                blance = 0,
                exp = 0,
                used_exp = 0
            };

            if (!string.IsNullOrEmpty(invt))
            {
                var ivt = DB.x_user.FirstOrDefault(o => o.uid == invt || o.email == invt || o.tel == invt);
                if (ivt != null && ivt.uid != uid) u.invter = ivt.id;
            }

            DB.x_user.InsertOnSubmit(u);
            SubmitDBChanges();

            return new XResp();
        }

    }
}
