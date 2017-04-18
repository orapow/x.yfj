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
    public class acpt : _wx
    {
        [ParmsAttr(name = "订单id", min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var od = cu.x_order.FirstOrDefault(o => o.order_id == id);
            if (od.status == 5) throw new XExcep("T当前订单已经确认收货");
            if (od.status != 4) throw new XExcep("T当前订单状态不正确");

            //od.=DateTime.Now;
            od.status = 5;
            SubmitDBChanges();

            return new XResp();

        }
    }
}
