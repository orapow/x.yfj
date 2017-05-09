using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;

namespace X.App.Views.wx.order
{
    public class refund : _wx
    {
        public int id { get; set; }
        public String reason { get; set; }
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
            var order = DB.x_order.FirstOrDefault(o => o.order_id == id);
            if (order == null)
                throw new XExcep("0x0024"); ;
            dict.Add("order", order);
        }
    }
}
