using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr
{
    public class main : xmg
    {
        protected override int powercode
        {
            get
            {
                return 0;
            }
        }
        public int st { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "st";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("refundCount", DB.x_refund.Where(o => o.status == 1 && o.x_order.city == cityid).Count());
            dict.Add("payCount", DB.x_order.Where(o => o.status == 1 && o.city == cityid).Count());//待付款
            dict.Add("confirmCount", DB.x_order.Where(o => o.status == 2 && o.city == cityid).Count());//待确认
            dict.Add("receiveCount", DB.x_order.Where(o => o.pay_way == 2 && o.city == cityid && o.status > 2 && !(o.pay_amount > 0)).Count());//待收款
            dict.Add("sendCount", DB.x_order.Where(o => o.status == 3 && o.city == cityid).Count());//待发货
            dict.Add("chgCount", DB.x_charge.Where(o => o.x_user.city == cityid && o.audit_status == 1).Count());
        }
    }
}
