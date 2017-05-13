using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;
using X.App.Com;

namespace X.App.Apis.mgr.deposit
{
    public class accept : xmg
    {
        public int deposit_id { get; set; }

        protected override int powercode
        {
            get
            {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var chg = DB.x_charge.FirstOrDefault(o => o.charge_id == deposit_id);
            if (chg == null) throw new XExcep("0x0016");
            if (chg.audit_status != 1) throw new XExcep("0x0017");

            chg.audit_status = 2;
            chg.audit_time = DateTime.Now;
            chg.audit_user = mg.mgr_id;

            chg.x_user.balance += chg.amount;

            SubmitDBChanges();

            return new XResp();
        }
    }
}
