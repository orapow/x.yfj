using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.city
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        protected override XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from d in GetDictList("sys.city", "0")
                    select new
                    {
                        d.id,
                        d.name,
                        code = d.f3,
                        d.sort
                    };

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key));

            r.items = q.Skip((page - 1) * limit).Take(limit);
            r.count = q.Count();
            return r;
        }

    }
}
