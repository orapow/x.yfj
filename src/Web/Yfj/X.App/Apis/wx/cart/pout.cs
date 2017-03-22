using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx.cart
{
    public class pout : _wx
    {
        public int gid { get; set; }
        public int del { get; set; }
        protected override XResp Execute()
        {
            var g = cu.x_cart.FirstOrDefault(o => o.goods_id == gid);
            if (g == null) return new XResp();

            g.sel = true;
            g.count -= del > 0 ? g.count : 1;
            if (g.count <= 0) DB.x_cart.DeleteOnSubmit(g);

            SubmitDBChanges();

            return new back()
            {
                gs = cu.x_cart.Where(o => o.sel == true).Count(),
                gc = cu.x_cart.Where(o => o.sel == true).Sum(o => o.count.Value),
                ct = g.count.Value,
                ps = cu.x_cart.Where(o => o.sel == true).Sum(o => o.price * o.count).Value
            };
        }

        class back : XResp
        {
            public int gs { get; set; }
            public int gc { get; set; }
            public int ct { get; set; }
            public decimal ps { get; set; }
        }
    }
}
