using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx
{
    public class outcart : _wx
    {
        public int gid { get; set; }
        protected override XResp Execute()
        {
            var g = cu.x_cart.FirstOrDefault(o => o.goods_id == gid);
            if (g == null) return new XResp();

            g.count--;
            if (g.count <= 0) DB.x_cart.DeleteOnSubmit(g);

            SubmitDBChanges();

            return new back()
            {
                gs = cu.x_cart.Count(),
                gc = cu.x_cart.Sum(o => o.count.Value),
                ct = g.count.Value
            };
        }

        class back : XResp
        {
            public int gs { get; set; }
            public int gc { get; set; }
            public int ct { get; set; }
        }
    }
}
