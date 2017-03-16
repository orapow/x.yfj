using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.field
{
    public class list : xmg
    {
        [ParmsAttr(name = "商品品类", min = 1)]
        public int pid { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        protected override XResp Execute()
        {
            var r = new Resp_List();

            //var q = from f in DB.x_goods_field
            //        where f.cate_id == pid
            //        select new
            //        {
            //            id = f.goods_field_id,
            //            f.name,
            //            type = GetDictName("field.type", f.type),
            //            price = f.ch_price == 1 ? "是" : "否",
            //            stock = f.ch_stock == 1 ? "是" : "否",
            //            muti = f.muti_val == 1 ? "是" : "否"
            //        };

            //r.items = q.Skip((page - 1) * limit).Take(limit).ToList();
            //r.count = q.Count();

            return r;
        }

    }
}
