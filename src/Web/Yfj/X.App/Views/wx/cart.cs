using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;

namespace X.App.Views.wx
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
            dict.Add("gs", cu.x_cart.ToList());
            dict.Add("gc", cu.x_cart.Where(o => o.sel == true).Sum(o => o.count));
            dict.Add("ps", cu.x_cart.Where(o => o.sel == true).Sum(o => o.price * o.count));

            if (aid > 0) dict.Add("ad", cu.x_address.FirstOrDefault(o => o.address_id == aid));

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
}
