using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.wx.succ
{
    public class succ : _wx
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("od", cu.x_order.FirstOrDefault(o => o.order_id == id));
        }
    }
}
