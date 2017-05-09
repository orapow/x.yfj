﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx.user
{
    public class deposit : _wx
    {
        public decimal amount
        {
            get; set;
        }

        protected override XResp Execute()
        {
            if (amount > cfg.max_deposit || amount < cfg.min_deposit)
                throw new XExcep("0x0056");

            var depositLog = new x_charge();
            depositLog.amount = amount;
            depositLog.ctime = DateTime.Now;
            depositLog.audit_status = 1;//1为待审核
            cu.x_charge.Add(depositLog);
            SubmitDBChanges();

            var c = CacheHelper.Get<string>("pay." + cu.id);
            if (!string.IsNullOrEmpty(c)) throw new XExcep("0x0048");

            CacheHelper.Save("pay." + cu.id, "1");
            var co = Wx.Pay.MdOrder(cu.nickname + "_" + cu.id + "充值" + amount, "chg_" + depositLog.charge_id + "", (int)(amount * 100) + "", "http://" + cfg.domain + "/wx/notify-2-" + depositLog.charge_id + ".html", cu.wx_opid, cfg.wx_appid, cfg.wx_mch_id, cfg.wx_paykey, false);
            CacheHelper.Remove("pay." + cu.id);
            SubmitDBChanges();

            if (co.return_code == "FAIL") throw new XExcep(co.return_msg);
            if (co.result_code == "FAIL") throw new XExcep(co.err_code + "," + co.err_code_des + "," + depositLog.charge_id);
            if (string.IsNullOrEmpty(co.prepay_id)) throw new XExcep("0x0051");

            var ps = new Dictionary<string, string>();
            ps.Add("appId", cfg.wx_appid);
            ps.Add("timeStamp", Tools.GetGreenTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            ps.Add("nonceStr", Tools.GetRandRom(24, 3));
            ps.Add("package", "prepay_id=" + co.prepay_id);
            ps.Add("signType", "MD5");

            var r = new od()
            {
                id = depositLog.charge_id,
                amount = depositLog.amount.Value,
                ns = ps["nonceStr"],
                ts = ps["timeStamp"],
                pkg = ps["package"],
                sign = Wx.ToSign(ps, false, cfg.wx_paykey),
                fromDeposit = 2
            };

            return r;

        }
        public class od : XResp
        {
            public long id
            {
                get; set;
            }
            public decimal amount
            {
                get; set;
            }
            public string ts
            {
                get; set;
            }
            public string ns
            {
                get; set;
            }
            public string pkg
            {
                get; set;
            }
            public string sign
            {
                get; set;
            }
            public int fromDeposit { get; set; }
        }
    }
}
