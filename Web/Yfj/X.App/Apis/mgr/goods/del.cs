using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.goods
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }
        /// <summary>
        /// 为4时彻底删除
        /// </summary>
        public int st { get; set; }

        protected override XResp Execute()
        {
            var ent = DB.x_goods.FirstOrDefault(o => o.goods_id == id);
            if (ent == null) throw new XExcep("T商品不存在");

            if (st == 4)
            {
                //var vals = DB.x_goods_val.Where(o => o.goods_id == id);
                //DB.x_goods_val.DeleteAllOnSubmit(vals);

                //var sts = DB.x_goods_stock.Where(o => o.goods_id == id);
                //DB.x_goods_stock.DeleteAllOnSubmit(sts);

                DB.x_goods.DeleteOnSubmit(ent);
            }
            else
            {
                /*----------------
                *1已录入
                *2已上架
                *3已下架
                *4已删除
                ----------------*/
                ent.status = 4;
            }

            SubmitDBChanges();

            return new XResp();
        }
    }
}
