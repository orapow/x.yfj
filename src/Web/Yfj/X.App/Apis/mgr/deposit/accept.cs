using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;
using X.App.Com;

namespace X.App.Apis.mgr.deposit {
    public class accept : xmg {
        public int deposit_id { get; set; }
        
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute() {
            var depositItem = DB.x_charge.FirstOrDefault(o => o.charge_id == deposit_id);
            if (depositItem ==null)
                throw new XExcep("T充值记录不存在");
            depositItem.audit_status = 2;
            depositItem.audit_time = DateTime.Now;
            depositItem.audit_user = mg.mgr_id;
            SubmitDBChanges();
            return new XResp();
        }
    }
}
