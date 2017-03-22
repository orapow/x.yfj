using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.order
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int st { get; set; }
        protected override XResp Execute()
        {
            var r = new Resp_List();
            var q = from o in DB.x_order
                    select o;

            if (st > 0) q.Where(o => o.status == st);
            r.count = q.Count();
            r.items = q.OrderByDescending(o => o.ctime).Select(o => new
            {
                gs = o.x_order_detail.Select(d => new { d.name, d.cover }),
                o.rec_man,
                o.rec_tel,
                o.rec_addr,
                o.yf_amount
            });

            r.page = page;

            return r;
        }

    }
}
