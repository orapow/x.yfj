using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.refund {
    public class accept : xmg {
        public int id { get; set; }

        protected override XResp Execute() {
            var od = DB.x_refund.FirstOrDefault(o => o.refund_id == id);

            if (od == null) throw new XExcep("T订单不存在");
            if (od.status != 1) throw new XExcep("T订单状态不正确");

            od.status = 2;
            od.aname = mg.name;
            od.atime = DateTime.Now;
            SubmitDBChanges();

            return new XResp();
        }
    }
}
