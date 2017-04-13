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

namespace X.App.Apis.wx.order {
    public class refund : _wx {
        [ParmsAttr(name = "订单id", min = 1)]
        public int id { get; set; }
        public String reason { get; set; }

        //public Decimal amount { get; set; }

        protected override XResp Execute() {
            var od = cu.x_order.FirstOrDefault(o => o.order_id == id);
            //if (od.status == 1 || od.status == 2)
            //    od.status = 6;
            //else 
            //    throw new XExcep("T当前订单状态无法取消");
            if (od == null)
                throw new XExcep("订单不存在");

            var refundItem = new x_refund();

            refundItem.amount = cu.x_order.FirstOrDefault(o=>o.order_id==id).pay_amount;//默认用户全款申请
            refundItem.ctime = DateTime.Now;
            refundItem.reason = reason;
            refundItem.x_order = od;
            refundItem.status = 1;

            od.x_refund.Add(refundItem);

            SubmitDBChanges();
            return new XResp();

        }
    }
}
