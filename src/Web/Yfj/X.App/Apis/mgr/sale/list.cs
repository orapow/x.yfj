using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.sale {
    public class list : xmg {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public string cate { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override Web.Com.XResp Execute() {
            var r = new Resp_List();
            r.page = page;

            var q = from d in DB.x_sale
                    orderby d.goods_id descending
                    select new {
                        id = d.sale_id,
                        goods_id = d.goods_id,
                        city_id = d.city_id,
                        limit=d.limit,
                        count=d.count,
                        price=d.price,
                        ctime=d.ctime,
                        etime=d.etime,//这里的etime等直接对应list.html里的cfg
                        name=d.x_goods.name//父子表可以直接读取
                    };

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key));
            if (!string.IsNullOrEmpty(cate)) {
                var cids = DB.x_dict.Where(o => o.code == "sale" && o.upval.Contains(cate) || o.value == cate).Select(o => o.value);
                q = q.Where(o => cids.Contains(o.name));
            }

            r.items = q.Skip((page - 1) * limit).Take(limit);
            r.count = q.Count();
            return r;
        }

    }
}
