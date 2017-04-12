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
    public class cancel : _wx {
        [ParmsAttr(name = "订单id", min = 1)]
        public int id { get; set; }

        protected override XResp Execute() {
            var od = cu.x_order.FirstOrDefault(o => o.order_id == id);
            if (od.status == 1 || od.status == 2)
                od.status = 6;
            else 
                throw new XExcep("T当前订单状态无法取消");

            SubmitDBChanges();
            return new XResp();

        }
    }
}
