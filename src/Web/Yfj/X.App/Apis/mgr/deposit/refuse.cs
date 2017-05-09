using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.deposit
{
    public class refuse : xmg
    {
        public int deposit_id { get; set; }
        public string reason { get; set; }
        protected override int powercode
        {
            get
            {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var depositItem = DB.x_charge.FirstOrDefault(o => o.charge_id == deposit_id);
            if (depositItem == null) throw new XExcep("0x0016");
            if (depositItem.audit_status != 1) throw new XExcep("0x0017");
            ////可能的退款操作

            depositItem.audit_status = 3;
            depositItem.audit_time = DateTime.Now;
            depositItem.audit_user = mg.mgr_id;
            depositItem.fail_reason = reason;
            SubmitDBChanges();

            return new XResp();
        }
    }
}
