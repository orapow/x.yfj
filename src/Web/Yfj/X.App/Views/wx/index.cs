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
        }

        public List<object> get_goods(string cate, int top)
        {
            var cids = DB.x_dict.Where(o => o.code == "goods.cate" && o.upval.Contains(cate) || o.value == cate).Select(o => o.value);

            var q = from g in DB.x_goods
                    where g.status == 2 && cids.Contains(g.cate_id)
                    orderby g.ctime descending
                    select new
                    {
                        title = g.name,
                        price = g.new_price,
                        id = g.goods_id,
                        g.cover,
                    };

            return q.Take(top).ToList<object>();
        }
    }
}
