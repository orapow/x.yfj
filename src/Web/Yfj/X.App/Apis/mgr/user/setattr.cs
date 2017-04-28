using System.Linq;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.user
{
    /// <summary>
    /// 设置属性
    /// </summary>
    public class update : xmg
    {
        public int id { get; set; }
        public int level { get; set; }
        public int sman { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var u = DB.x_user.FirstOrDefault(o => o.user_id == id);
            if (u == null) throw new XExcep("T用户不存在");

            u.group = level;
            u.invter = sman;
            u.name = name;
            u.tel = tel;

            SubmitDBChanges();

            return new XResp();
        }

    }
}
