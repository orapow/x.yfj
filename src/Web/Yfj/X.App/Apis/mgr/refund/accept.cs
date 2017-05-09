using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;
using X.App.Com;

namespace X.App.Apis.mgr.refund
{
    public class accept : xmg
    {
        public int id { get; set; }
        public decimal ramount { get; set; }

        public String remark { get; set; }

        protected override int powercode
        {
            get
            {
                return 2;
            }
        }

        protected override XResp Execute()
        {
            var od = DB.x_refund.FirstOrDefault(o => o.refund_id == id);

            if (od == null) throw new XExcep("0x0024");
            if (od.status != 1) throw new XExcep("0x0026");
            od.remark = remark;
            od.ramount = ramount;

            if (od.x_order.pay_way == 1)
            {
                var response = Wx.Pay.Refund(cfg.wx_appid, cfg.wx_mch_id, od.x_order.x_user.wx_opid, od.x_order.no, od.refund_id.ToString(),
                    (int)(od.x_order.yf_amount * 100) + "", ((int)(ramount * 100)).ToString(), cfg.wx_paykey, cfg.wx_certpath);
                if (response.return_code.Equals("FAIL")) throw new XExcep("0x0031", response.return_msg + "，" + response.err_code_des);
            }
            else if (od.x_order.pay_way == 3)
            {
                od.x_order.x_user.balance += ramount;
            }

            od.status = 2;
            od.aname = mg.name;
            od.atime = DateTime.Now;

            SubmitDBChanges();

            return new XResp();
        }
    }
}
