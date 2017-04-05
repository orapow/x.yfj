using System.Linq;
using X.Web;

namespace X.App.Views.mgr.order
{
    public class cash : xmg
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
            dict.Add("amount", ent.yf_amount);
        }
    }
}
