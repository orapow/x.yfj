using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.sale {
    public class save : xmg {
        public int sale_id { get; set; }
        /// <summary>
        /// 是否是复制
        /// </summary>
        /// 
        public long id { get; set; }//商品的id 
        public long city_id { get; set; }
        public int limit { get; set; }//0k
        public int count { get; set; }//ok
        public decimal price { get; set; }//ok
        public DateTime ctime { get; set; }//ok
        public DateTime etime { get; set; }//ok 结束时间




        protected override XResp Execute() {
            if (limit > count)
                throw new XExcep("T限购量超出促销量");

            x_goods ent = null;
            if (id > 0) {
                ent = DB.x_goods.SingleOrDefault(o => o.goods_id == id);
                if (ent == null) throw new XExcep("0x0005");
            }
            if (ent == null)
                throw new XExcep("T商品不存在，无法加入促销列表");
            if (ent.stock < count)
                throw new XExcep("T内容错误，促销量超出库存");

            x_sale saleItem = null;
            saleItem = DB.x_sale.SingleOrDefault(o => o.goods_id == id);
            if (saleItem != null)
                throw new XExcep("T此商品已存在，无法重复加入，请先删除再操作");
            if(ctime.CompareTo(etime)>0)
                throw new XExcep("T内容错误，开始时间晚于结束时间");

            //复制属性
            saleItem = new x_sale();

            saleItem.goods_id = ent.goods_id;
            saleItem.city_id = (int)ent.city;//????会不会有问题
            saleItem.limit = limit;
            saleItem.count = count;
            saleItem.price = price;
            saleItem.ctime = ctime;
            saleItem.etime = etime;

            //if (ent.goods_id == 0) ent.status = 2;

            if (saleItem.sale_id == 0) {//?????
                DB.x_sale.InsertOnSubmit(saleItem);
                SubmitDBChanges();
            }


            SubmitDBChanges();

            return new XResp();
        }
    }
}
