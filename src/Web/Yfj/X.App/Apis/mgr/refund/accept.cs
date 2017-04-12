using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;
using X.App.Com;

namespace X.App.Apis.mgr.refund {
    public class accept : xmg {
        public int id { get; set; }
        public int refundFee { get; set; }

        protected override XResp Execute() {
            var od = DB.x_refund.FirstOrDefault(o => o.refund_id == id);

            if (od == null) throw new XExcep("T订单不存在");
            if (od.status != 1) throw new XExcep("T订单状态不正确");

            refundFee = (int)(od.x_order.yf_amount * 100);

            var response = Wx.Pay.Refund(cfg.wx_appid, cfg.wx_mch_id, od.x_order.x_user.wx_opid, od.x_order.no, od.refund_id.ToString(), 
                (int)(od.x_order.yf_amount * 100)+"", refundFee.ToString(), cfg.wx_paykey, cfg.wx_certpath);
            if (response.result_code.Equals("SUCCESS")) {
                od.status = 2;
                od.aname = mg.name;
                od.atime = DateTime.Now;
                SubmitDBChanges();
            } else
                throw new XExcep("T退款失败,原因: " + response.err_code_des);

            return new XResp();
        }
    }
}
