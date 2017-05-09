using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;

namespace X.App.Views.wx
{
    public class index : _wx
    {
        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }

        protected override void InitDict()
        {
            base.InitDict();
            var sales = DB.x_sale.Where(o => o.etime >= DateTime.Now && o.x_goods != null).Select(o => new
            {
                name = o.x_goods.name,
                cover = o.x_goods.cover,
                price = o.price,
                id = o.sale_id
            });
            dict.Add("sales", sales.ToList());
        }

        public List<object> get_goods(string cate, int top)
        {
            var cids = DB.x_dict.Where(o => o.code == "goods.cate" && o.upval.Contains(cate) || o.value == cate).Select(o => o.value);

            var q = from g in DB.x_goods
                    where g.status == 2 && cids.Contains(g.cate_id) && g.hot == 1
                    orderby g.ctime descending
                    select new
                    {
                        g.city,
                        title = g.name,
                        price = g.new_price,
                        id = g.goods_id,
                        g.cover,
                    };

            if (cu != null) q = q.Where(o => o.city == cu.city);
            if (top > 0) q = q.Take(top);

            return q.ToList<object>();
        }
    }
}
