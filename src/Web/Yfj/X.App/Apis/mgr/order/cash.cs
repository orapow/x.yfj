using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.order
{
    public class cash : xmg
    {
        public int id { get; set; }
        public decimal am { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var od = DB.x_order.FirstOrDefault(o => o.order_id == id);

            if (od == null) throw new XExcep("0x0024");
            if (od.city != mg.city) throw new XExcep("0x0025");
            if (od.pay_amount > 0) throw new XExcep("0x0027");
            if (od.pay_way != 2) throw new XExcep("0x0028");

            od.pay_amount = am;
            od.pay_time = DateTime.Now;
            SubmitDBChanges();

            return new XResp();
        }
    }
}
