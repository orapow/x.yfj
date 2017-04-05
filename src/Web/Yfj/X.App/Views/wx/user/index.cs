using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.wx.user
{
    public class index : _wx
    {
        protected override void InitView()
        {
            base.InitView();
            dict.Add("ac", cu.x_address.Count());
            dict.Add("o1", cu.x_order.Count(o => o.status == 1));
            dict.Add("o3", cu.x_order.Count(o => o.status == 3));
            dict.Add("o4", cu.x_order.Count(o => o.status == 4));
        }
    }
}
