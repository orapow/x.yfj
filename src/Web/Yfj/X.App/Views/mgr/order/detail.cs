using System.Linq;
using X.Web;

namespace X.App.Views.mgr.order {
    public class detail : xmg {
        public int id { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override string GetParmNames {
            get {
                return "id";
            }
        }
        protected override void InitDict() {
            base.InitDict();
            var ent = DB.x_order.FirstOrDefault(o => o.order_id == id);
            if (ent == null) throw new XExcep("0x0005");
            dict.Add("od", ent);
            dict.Add("gs", ent.x_order_detail.Select(o => new { o.name, o.cover, o.count, o.stand, o.unit, o.price, o.total_price }));
            dict.Add("st", "|待付款|待确认|待发货|待签收|已完成|已取消".Split('|')[ent.status.Value]);
            dict.Add("sum", ent.x_order_detail.Sum(o => o.count));
            dict.Add("itemRefund", DB.x_refund.FirstOrDefault(o => o.order_id == id));
        }
    }
}
