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

namespace X.App.Apis.wx
{
    public class reg : _wx
    {
        [ParmsAttr(name = "电话", req = true)]
        public string tel { get; set; }
        [ParmsAttr(name = "密码", req = true)]
        public string pwd { get; set; }
        public string opid { get; set; }
        [ParmsAttr(name = "验证码", req = true)]
        public string code { get; set; }

        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }

        protected override XResp Execute()
        {
            var mcode = CacheHelper.Get<string>("sms.code." + tel);
            if (string.IsNullOrEmpty(mcode) || mcode != code) throw new XExcep("0x0054");
            if (DB.x_user.Count(o => o.tel == tel) > 0) throw new XExcep("0x0055");
            CacheHelper.Remove("sms.code." + tel);

            var user = new x_user();
            user.tel = tel;
            user.pwd = Secret.MD5(pwd);
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
