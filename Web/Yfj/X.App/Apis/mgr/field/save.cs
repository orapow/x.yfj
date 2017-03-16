using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.field
{
    public class save : xmg
    {
        public int id { get; set; }
        [ParmsAttr(req = true, name = "属性名称")]
        public string name { get; set; }
        [ParmsAttr(name = "商品品类", min = 1)]
        public int pid { get; set; }
        public int type { get; set; }
        public string sw { get; set; }
        public int unprice { get; set; }
        public int unstock { get; set; }
        public int muti { get; set; }
        public int upimg { get; set; }

        protected override XResp Execute()
        {
            //x_goods_field ent = null;
            //if (id > 0) ent = DB.x_goods_field.FirstOrDefault(o => o.goods_field_id == id);
            //if (ent == null) ent = new x_goods_field() { cate_id = pid };

            //ent.name = name;
            //ent.type = type;
            //ent.upimg = sw.Contains("[1]") ? 1 : 2;
            //ent.muti_val = sw.Contains("[2]") ? 1 : 2;
            //ent.ch_price = sw.Contains("[3]") ? 1 : 2;
            //ent.ch_stock = sw.Contains("[4]") ? 1 : 2;

            //if (id == 0) DB.x_goods_field.InsertOnSubmit(ent);

            //SubmitDBChanges();

            return new XResp();
        }
    }
}
