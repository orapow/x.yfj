using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.sale {
    public class del : xmg {
        [ParmsAttr(min = 1)]
        public int id { get; set; }
        /// <summary>
        /// 为4时彻底删除
        /// </summary>
        public int st { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute() {
            var ent = DB.x_sale.FirstOrDefault(o => o.sale_id == id);
            if (ent == null) throw new XExcep("T商品不存在");

            //if (st == 4) {
            //    //var vals = DB.x_goods_val.Where(o => o.goods_id == id);
            //    //DB.x_goods_val.DeleteAllOnSubmit(vals);

            //    //var sts = DB.x_goods_stock.Where(o => o.goods_id == id);
            //    //DB.x_goods_stock.DeleteAllOnSubmit(sts);

            //    DB.x_sale.DeleteOnSubmit(ent);
            //} else {
            //    /*----------------
            //    *1已录入
            //    *2已上架
            //    *3已下架
            //    *4已删除
            //    ----------------*/
            //    ent.x_goods.status = 4;
            //}
            DB.x_sale.DeleteOnSubmit(ent);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
