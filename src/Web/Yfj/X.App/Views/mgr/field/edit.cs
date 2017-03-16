using System.Linq;
using X.Web;

namespace X.App.Views.mgr.field
{
    public class edit : xmg
    {
        [ParmsAttr(name = "商品品类", min = 1)]
        public int pid { get; set; }
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "pid-id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            //if (id > 0)
            //{

            //    var ent = DB.x_goods_field.FirstOrDefault(o => o.goods_field_id == id);
            //    if (ent == null) throw new XExcep("0x0005");
            //    dict.Add("item", ent);
            //    dict.Add("sw", (ent.upimg == 1 ? "[1]" : "") + (ent.muti_val == 1 ? "[2]" : "") + (ent.ch_price == 1 ? "[3]" : "") + (ent.ch_stock == 1 ? "[4]" : ""));
            //}
        }
    }
}
