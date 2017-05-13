using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.deposit
{
    public class manual : xmg
    {
        public string uid { get; set; }
        public decimal amount { get; set; }

        protected override int powercode
        {
            get
            {
                return 2;
            }
        }
        protected override XResp Execute()
        {
            var user = DB.x_user.SingleOrDefault(o => o.uid == uid || o.tel == uid);
            if (user == null) throw new XExcep("0x0018");
            if (amount <= 0) throw new XExcep("0x0019");

            var depositItem = new x_charge();
            depositItem.x_user = user;
            depositItem.amount = amount;
            depositItem.result = "后台充值成功";
            depositItem.user_id = user.user_id;
            depositItem.audit_status = 2;
            depositItem.audit_time = DateTime.Now;
            depositItem.audit_user = mg.mgr_id;
            depositItem.ctime = DateTime.Now;

            user.balance += amount;

            SubmitDBChanges();
            return new XResp();
        }
    }
}
