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

namespace X.App.Apis.sder
{
    public class acpt : xapi
    {
        [ParmsAttr(name = "订单id", min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var od = DB.x_order.FirstOrDefault(o => o.order_id == id);
            if (od == null) throw new XExcep("0x0024");
            if (od.status == 5) throw new XExcep("0x0046");
            if (od.status != 4) throw new XExcep("0x0026");

            od.sign_time = DateTime.Now;
            od.status = 5;

            //对订单积分的处理
            var credit = new x_integral_log();
            od.x_user.invter += (long)cfg.credit * (long)od.pay_amount;
            credit.user_id = od.user_id;
            credit.val = cfg.credit * (int)od.pay_amount;
            credit.remark = "订单号：" + od.order_id + " 返积分: " + credit.val + " 应付金额： " + od.pay_amount;
            credit.ctime = DateTime.Now;
            DB.x_integral_log.InsertOnSubmit(credit);
            od.x_user.invter += credit.val;

            SubmitDBChanges();

            return new XResp();

        }
    }
}
