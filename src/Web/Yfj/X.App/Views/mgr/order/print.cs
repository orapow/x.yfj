using System;
using System.Linq;
using X.Web;

namespace X.App.Views.mgr.order
{
    public class print : xmg
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
            dict.Add("od", ent);
            dict.Add("gs", ent.x_order_detail.Select(o => new { o.name, o.count, o.stand, o.unit, o.price, o.total_price }));
            dict.Add("dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dict.Add("sum", ent.x_order_detail.Sum(o => o.count));
        }
    }
}
