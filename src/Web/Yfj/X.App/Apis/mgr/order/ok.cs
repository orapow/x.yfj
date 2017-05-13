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
    public class ok : xmg
    {
        public int id { get; set; }
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
            if (od.status != 2) throw new XExcep("0x0026");

            od.status = 3;
            SubmitDBChanges();

            return new XResp();
        }
    }
}
