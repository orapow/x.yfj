using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.sale
{
    public class save : xmg
    {
        public long id { get; set; }//商品的id 
        public int limit { get; set; }//0k
        public int count { get; set; }//ok
        public decimal price { get; set; }//ok
        public DateTime ctime { get; set; }//ok
        public DateTime etime { get; set; }//ok 结束时间


        protected override int powercode
        {
            get
            {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            if (limit > count) throw new XExcep("0x0032");

            var ent = DB.x_goods.SingleOrDefault(o => o.goods_id == id);

            if (ent == null) throw new XExcep("0x0020");
            if (ent.status != 2) throw new XExcep("0x0033");
            if (ent.stock < count) throw new XExcep("0x0034");
            if (ctime.CompareTo(etime) > 0) throw new XExcep("0x0035");

            x_sale saleItem = DB.x_sale.SingleOrDefault(o => o.goods_id == id);
            if (saleItem == null) saleItem = new x_sale();

            saleItem.goods_id = ent.goods_id;
            saleItem.city_id = mg.city;
            saleItem.limit = limit;
            saleItem.count = count;
            saleItem.price = price;
            saleItem.ctime = ctime;
            saleItem.etime = etime;

            if (saleItem.sale_id == 0) DB.x_sale.InsertOnSubmit(saleItem);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
