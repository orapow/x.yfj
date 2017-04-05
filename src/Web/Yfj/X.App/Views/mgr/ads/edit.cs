using System.Linq;
using X.Web;

namespace X.App.Views.mgr.ads
{
    public class edit : xmg
    {
        public int id { get; set; }

        protected override string GetParmNames
        {
            //传参数
            get
            {
                return "id";
            }
        }

        protected override void InitDict()
        {
            base.InitDict();
            if (id > 0)
            {
                var ent = DB.x_ad.SingleOrDefault(o => o.ad_id == id);
                if (ent == null) throw new XExcep("0x0005");

                dict.Add("item", ent);
                dict.Add("cate", ent.pos + "|" + GetDictName("ad.pos", ent.pos));
            }
        }
    }
}
