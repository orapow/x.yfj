using System.Linq;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx.user
{
    public class savepswd : _wx
    {
        [ParmsAttr(name = "电话", req = true)]
        public string tel { get; set; }
        [ParmsAttr(name = "密码", req = true)]
        public string pwd { get; set; }
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
            CacheHelper.Remove("sms.code." + tel);

            var u = DB.x_user.FirstOrDefault(o => o.tel == tel);
            if (u == null) throw new XExcep("0x0039");

            u.pwd = Secret.MD5(pwd);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
