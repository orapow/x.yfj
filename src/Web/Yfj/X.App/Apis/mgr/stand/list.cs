using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.stand
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
        

        protected override Web.Com.XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from d in GetDictList("goods.stand", "0")
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
            if (!string.IsNullOrEmpty(cate)) q = q.Where(o => o.f3 == cate);

            r.items = q.Skip((page - 1) * limit).Take(limit);
            r.count = q.Count();
            return r;
        }

    }
}
