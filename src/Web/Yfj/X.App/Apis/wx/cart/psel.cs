using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.wx.cart
{
    public class psel : _wx
    {
        public int gid { get; set; }
        protected override XResp Execute()
        {
            var g = cu.x_cart.FirstOrDefault(o => o.goods_id == gid);
            if (g == null) return new XResp();

            g.sel = !g.sel;
            SubmitDBChanges();
            decimal shipAmount = cu.x_cart.Where(o => o.calcfreight == 2 && o.sel == true).Sum(o => o.price * o.count).Value;
            return new back()
            {
                gs = cu.x_cart.Where(o => o.sel == true).Count(),
                gc = cu.x_cart.Where(o => o.sel == true).Sum(o => o.count.Value),
                ct = g.count.Value,
                ps = cu.x_cart.Where(o => o.sel == true).Sum(o => o.price * o.count).Value,
                sel = g.sel == true ? 1 : 2,
                shipfee = shipAmount >= cfg.free_ship || g.count == 0 ? 0 : cfg.shipfee
            };

        }

        class back : XResp
        {
            public int gs { get; set; }
            public int gc { get; set; }
            public int ct { get; set; }
            public decimal ps { get; set; }
            public int sel { get; set; }
            public decimal shipfee { get; set; }
        }
    }
}
