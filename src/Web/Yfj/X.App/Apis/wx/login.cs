using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;

namespace X.App.Apis.wx
{
    public class login : _wx
    {
        [ParmsAttr(name = "电话", req = true)]
        public string tel { get; set; }
        public string code { get; set; }
        public string pwd { get; set; }
        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }
        protected override Web.Com.XResp Execute()
        {
            if (!string.IsNullOrEmpty(code))
            {
                var mcode = CacheHelper.Get<string>("sms.code." + tel);
                if (mcode == null || mcode != code) throw new XExcep("0x0054");
            }
            else if (!string.IsNullOrEmpty(pwd))
            {
                var u = DB.x_user.FirstOrDefault(o => o.tel == tel);
                if (u == null || u.pwd != Secret.MD5(pwd)) throw new XExcep("0x0023");
            }
            else
            {
                throw new XExcep("参数丢失");
            }
            cu.ukey = Secret.MD5(Guid.NewGuid().ToString());
            SubmitDBChanges();
            Context.Response.SetCookie(new HttpCookie("cu_key", cu.ukey));//保存cookie状态
            return new Web.Com.XResp();
        }
    }
}
