using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.deposit {
    public class manual : xmg {
        public int id { get; set; }
        /// <summary>
        /// 是否是复制
        /// </summary>
        public int user_id { get; set; }
        public decimal amount { get; set; }
        public String reason { get; set; }

        protected override int powercode {
            get {
                return 1;
            }
        }
        protected override XResp Execute() {
            var user = DB.x_user.SingleOrDefault(o => o.user_id == user_id);
            if (user == null)
                throw new XExcep("T用户不存在");
            if (amount <= 0)
                throw new XExcep("T充值金额非法");
            x_charge depositItem = new x_charge();
            depositItem.x_user = user;
            depositItem.amount = amount;
            depositItem.result = "MANUAL";
            depositItem.user_id = user.user_id;
            depositItem.audit_status = 2;
            depositItem.audit_time = DateTime.Now;
            depositItem.audit_user = mg.mgr_id;
            depositItem.ctime = DateTime.Now;
            depositItem.fail_reason = reason;

            user.balance += amount;

            SubmitDBChanges();

            return new XResp();
        }
    }
}
