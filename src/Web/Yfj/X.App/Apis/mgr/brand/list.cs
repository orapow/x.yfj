using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.brand
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public string cate { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override Web.Com.XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from d in GetDictList("goods.brand", "0")
                    select new
                    {
                        d.id,
                        d.name,
                        cate = GetDictName("goods.cate", d.f3),
                        d.f3,
                        d.sort,
                        d.img
                    };

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key));
            if (!string.IsNullOrEmpty(cate))
            {
                var cids = DB.x_dict.Where(o => o.code == "goods.cate" && o.upval.Contains(cate) || o.value == cate).Select(o => o.value);
                q = q.Where(o => cids.Contains(o.f3));
            }

            r.items = q.Skip((page - 1) * limit).Take(limit);
            r.count = q.Count();
            return r;
        }

    }
}
