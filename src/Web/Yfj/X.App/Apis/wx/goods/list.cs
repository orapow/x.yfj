using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.wx.goods
{
    public class list : _wx
    {
        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public string cate { get; set; }
        public int bd { get; set; }
        public string md { get; set; }
        protected override XResp Execute()
        {
            var q = from g in DB.x_goods
                    where g.status == 2
                    select g;

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key) || o.alias.Contains(key) || o.remark.Contains(key));
            if (!string.IsNullOrEmpty(cate))
            {
                var cids = DB.x_dict.Where(o => o.code == "goods.cate" && o.upval.Contains(cate) || o.value == cate).Select(o => o.value);
                q = q.Where(o => cids.Contains(o.cate_id));
            }

            if (cu != null) q = q.Where(o => o.city == cu.city);
            if (!string.IsNullOrEmpty(md)) q = q.Where(o => o.alias.Contains(md));
            if (bd > 0) q = q.Where(o => o.brand == bd);

            var r = new rsp();
            r.page = page;
            r.count = q.Count();

            r.mds = q.GroupBy(o => o.alias).Select(o => o.Key).ToArray();
            r.bds = q.GroupBy(o => o.brand).Select(o => new { id = o.Key, name = GetDictName("goods.brand", o.Key) }).ToArray();

            r.items = q
                .OrderByDescending(o => o.sort).ThenByDescending(o => o.ctime)
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

        class rsp : Resp_List
        {
            public string[] mds { get; set; }
            public object[] bds { get; set; }
        }
    }
}
