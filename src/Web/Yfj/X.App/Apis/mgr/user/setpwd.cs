using System.Linq;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.user
{
    /// <summary>
    /// 删除用户
    /// </summary>
    public class setpwd : xmg
    {
        public int id { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var u = DB.x_user.FirstOrDefault(o => o.user_id == id);
            if (u == null) throw new XExcep("0x0039");

            u.pwd = Secret.MD5("123456");

            SubmitDBChanges();

            return new XResp();
        }

    }
}
