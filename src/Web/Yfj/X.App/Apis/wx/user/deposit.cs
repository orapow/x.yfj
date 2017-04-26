using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx.user {
    public class deposit : _wx {
        public decimal amount { get; set; }

        protected override XResp Execute() {
            if (amount > cfg.max_deposit || amount < cfg.min_deposit)
                throw new XExcep("T充值金额需要在许可范围内");
            var depositLog = new x_charge();
            depositLog.amount = amount;
            //depositLog.user_id=cu.user_id;
            //depositLog.
            cu.x_charge.Add(depositLog);
            SubmitDBChanges();

            var c = CacheHelper.Get<string>("pay." + cu.id);
            if (!string.IsNullOrEmpty(c)) throw new XExcep("T当前订单正在处理中，请稍后...");

            CacheHelper.Save("pay." + cu.id, "1");
            var co = Wx.Pay.MdOrder(cu.nickname + "_" + cu.id + "充值", "chg_" + depositLog.charge_id + "", amount * 100 + "", "http://" + cfg.domain + "/wx/notify-2" + depositLog.charge_id + ".html", cu.wx_opid, cfg.wx_appid, cfg.wx_mch_id, cfg.wx_paykey, false);

            CacheHelper.Remove("pay." + cu.id);

            if (co.return_code == "FAIL") throw new XExcep(co.return_msg);
            if (co.result_code == "FAIL") throw new XExcep(co.err_code + "," + co.err_code_des);
            if (string.IsNullOrEmpty(co.prepay_id)) throw new XExcep("T预付款号为空");

            //od.wx_no = co.prepay_id;

            var ps = new Dictionary<string, string>();
            ps.Add("appId", cfg.wx_appid);
            ps.Add("timeStamp", Tools.GetGreenTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            ps.Add("nonceStr", Tools.GetRandRom(24, 3));
            ps.Add("package", "prepay_id=" + co.prepay_id);
            ps.Add("signType", "MD5");

            var r = new od() {
                id = depositLog.charge_id,
                amount = amount + "",
                ns = ps["nonceStr"],
                ts = ps["timeStamp"],
                pkg = ps["package"],
                sign = Wx.ToSign(ps, false, cfg.wx_paykey)
            };

            return r;

        }
        public class od : XResp {
            public long id { get; set; }
            public string amount { get; set; }
            public string ts { get; set; }
            public string ns { get; set; }
            public string pkg { get; set; }
            public string sign { get; set; }
        }
    }
}
