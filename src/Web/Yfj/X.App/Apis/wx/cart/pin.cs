using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx.cart
{
    public class pin : _wx
    {
        public int gid { get; set; }
        public int count { get; set; }
        protected override XResp Execute()
        {
            var gd = DB.x_goods.FirstOrDefault(o => o.goods_id == gid && o.status == 2);
            if (gd == null) throw new XExcep("0x0020");
            if (gd.city != cu.city) throw new XExcep("0x0042");

            var sl = gd.x_sale.FirstOrDefault(o => o.etime > DateTime.Now);

            var g = cu.x_cart.FirstOrDefault(o => o.goods_id == gid);
            if (g == null)
            {
                g = new Data.x_cart()
                {
                    ctime = DateTime.Now,
                    cover = gd.cover,
                    desc = gd.alias,
                    count = count > 0 ? count : 1,
                    unit = gd.unit,
                    goods_id = gid,
                    goods_name = gd.name,
                    price = sl == null ? gd.new_price : sl.price,
                    user_id = cu.id,
                    calcfreight = gd.calcfreight,
                    sel = true
                };
                cu.x_cart.Add(g);
            }
            else
            {
                g.sel = true;
                if (count > 0) g.count = count;
                else g.count++;
            }

            if (sl != null && g.count > sl.limit) throw new XExcep("0x0043", sl.limit + "件");
            if (sl != null && sl.count <= 0) throw new XExcep("0x0044");
            g.total_price = g.price * g.count;

            SubmitDBChanges();

            decimal shipAmount = cu.x_cart.Where(o => o.calcfreight == 2 && o.sel == true).Sum(o => o.price * o.count).Value;
            return new back()
            {
                gs = cu.x_cart.Where(o => o.sel == true).Count(),
                gc = cu.x_cart.Where(o => o.sel == true).Sum(o => o.count.Value),
                ct = g.count.Value,
                ps = cu.x_cart.Where(o => o.sel == true).Sum(o => o.price * o.count).Value,
                shipfee = shipAmount >= cfg.free_ship || g.count == 0 ? 0 : cfg.shipfee
            };
        }

        class back : XResp
        {
            public int gs { get; set; }
            public int gc { get; set; }
            public int ct { get; set; }
            public decimal ps { get; set; }
            public decimal shipfee { get; set; }
        }
    }
}
