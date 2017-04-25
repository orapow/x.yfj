using System.Linq;
using X.Web;

namespace X.App.Views.mgr.brand
{
    public class edit : xmg
    {
        public int id { get; set; }
        public string ct { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }
        protected override string GetParmNames
        {
            get
            {
                return "id-ct";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            if (id > 0)
            {
                var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
                ct = ent.f3;
            }
            if (!string.IsNullOrEmpty(ct)) dict.Add("cate", ct + "|" + GetDictName("goods.cate", ct));
        }
    }
}
