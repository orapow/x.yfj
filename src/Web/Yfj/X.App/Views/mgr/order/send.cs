using System.Linq;
using X.Web;

namespace X.App.Views.mgr.order
{
    public class send : xmg
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var ent = DB.x_order.FirstOrDefault(o => o.order_id == id);
            if (ent == null) throw new XExcep("0x0005");
            if (!string.IsNullOrEmpty(ent.send_man))
            {
                var sm = ent.send_man.Split(' ');
                if (sm.Length == 2)
                {
                    dict.Add("name", sm[0]);
                    dict.Add("tel", sm[1]);
                }
            }
        }
    }
}
