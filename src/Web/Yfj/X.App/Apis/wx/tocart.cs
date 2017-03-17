using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx
{
    public class tocart : _wx
    {
        public int gid { get; set; }
        protected override XResp Execute()
        {
            var gd = DB.x_goods.FirstOrDefault(o => o.goods_id == gid && o.status == 2);
            if (gd == null) throw new XExcep("T商品存不存在或已经下架");
            if (gd.city != cu.city) throw new XExcep("T商品在此城市无货");

            var g = cu.x_cart.FirstOrDefault(o => o.goods_id == gid);
            if (g == null)
            {
                g = new Data.x_cart()
                {
                    ctime = DateTime.Now,
                    cover = gd.cover,
                    desc = gd.alias,
                    count = 1,
                    unit = gd.unit,
                    goods_id = gid,
                    goods_name = gd.name,
                    price = gd.new_price,
                    total_price = gd.new_price,
                    user_id = cu.id
                };
                cu.x_cart.Add(g);
            }
            else
            {
                g.count++;
            }
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
