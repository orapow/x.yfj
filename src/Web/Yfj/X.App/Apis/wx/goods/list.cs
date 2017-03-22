using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.wx.goods
{
    public class list : _wx
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public string cate { get; set; }
        public int bd { get; set; }
        public int gg { get; set; }
        protected override XResp Execute()
        {
            var q = from g in DB.x_goods
                    where g.status == 2
                    select g;

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key) || o.remark.Contains(key));
            if (!string.IsNullOrEmpty(cate))
            {
                var cids = DB.x_dict.Where(o => o.code == "goods.cate" && o.upval.Contains(cate) || o.value == cate).Select(o => o.value);
                q = q.Where(o => cids.Contains(o.cate_id));
            }

            var r = new Resp_List();
            r.page = page;
            r.count = q.Count();
            r.items = q
                .OrderByDescending(o => o.ctime)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList()
                .Select(o => new
                {
                    id = o.goods_id,
                    o.name,
                    o.alias,
                    price = o.new_price,
                    o.cover,
                    o.unit,
                    count = getcount(o.goods_id)
                });
            return r;
        }
        int getcount(long gid)
        {
            if (cu == null) return 0;
            var c = cu.x_cart.SingleOrDefault(o => o.sel == true && o.goods_id == gid);
            if (c == null) return 0;
            return c.count.Value;
        }
    }
}
