using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx.order
{
    public class del : _wx
    {
        [ParmsAttr(name = "订单id", min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var od = cu.x_order.FirstOrDefault(o => o.order_id == id);

            if (od == null) throw new XExcep("0x0024");
            if (od.status != 5 && od.status != 6) throw new XExcep("0x0059");

            od.isdel = true;
            SubmitDBChanges();

            return new XResp();

        }
    }
}
