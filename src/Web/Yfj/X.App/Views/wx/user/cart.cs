using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;

namespace X.App.Views.wx.user
{
    public class cart : _wx
    {
        public int aid { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "aid";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            if (cu.x_cart.Count() == 0)
            {
                dict.Add("img", "/img/wx/uig.png");
                dict.Add("msg", "购物车是空的");
                dict.Add("bt_txt", "去选商品");
                dict.Add("bt_url", "/wx/goods/list.html");
                dict.Add("show_foot", 1);
            }
            else
            {
                dict.Add("gs", cu.x_cart.ToList());
                dict.Add("gc", cu.x_cart.Where(o => o.sel == true).Sum(o => o.count));
                dict.Add("ps", cu.x_cart.Where(o => o.sel == true).Sum(o => o.price * o.count));
                dict.Add("shipfee", cu.x_cart.Where(o => o.calcfreight == 1).Sum(o => o.price).Value >= cfg.free_ship ? 0 : cfg.shipfee);
                x_address ad = cu.x_address.FirstOrDefault(o => o.address_id == aid);
                if (ad == null) ad = cu.x_address.FirstOrDefault();
                dict.Add("ad", ad);

                var ds = new List<string>();
                var dt = DateTime.Now;
                for (var i = 0; ds.Count() < 4;)
                {
                    var d = dt.AddDays(i++);
                    if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday) continue;
                    ds.Add(d.ToString("yyyy-MM-dd"));
                }
                dict.Add("ds", ds);

            }
        }
        public override string GetTplFile()
        {
            if (cu.x_cart.Count() == 0) return "wx/no";
            return base.GetTplFile();
        }
    }
}
