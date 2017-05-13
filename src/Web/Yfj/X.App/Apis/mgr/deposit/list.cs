using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.deposit
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public int st { get; set; }

        protected override int powercode
        {
            get
            {
                return 1;
            }
        }

        protected override Web.Com.XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from d in DB.x_charge
                    orderby d.charge_id descending
                    select new
                    {
                        id = d.charge_id,
                        amount = d.amount,
                        uid = d.x_user.user_id,
                        name = d.x_user.name,
                        un = d.x_user.nickname,
                        up = d.x_user.headimg,
                        ctime = d.ctime,
                        result = d.result,
                        status = d.audit_status,
                        statusDisplay = d.audit_status == 3 ? "已拒绝" : (d.audit_status == 2 ? "已同意" : "待审核")
                    };

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key));
            if (st == 2) q = q.Where(o => o.status == 1);

            r.items = q.Skip((page - 1) * limit).Take(limit);
            r.count = q.Count();
            return r;
        }

    }
}
