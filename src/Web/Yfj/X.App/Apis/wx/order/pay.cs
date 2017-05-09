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

namespace X.App.Apis.wx.order
{
    public class pay : _wx
    {
        [ParmsAttr(name = "订单id", min = 1)]
        public int id { get; set; }

        public int pay_way { get; set; }

        private XResp wechatPay()
        {
            var c = CacheHelper.Get<string>("pay." + cu.id);
            if (!string.IsNullOrEmpty(c)) throw new XExcep("0x0048");

            CacheHelper.Save("pay." + cu.id, "1");
            var od = cu.x_order.FirstOrDefault(o => o.order_id == id);
            if (od.pay_amount > 0) throw new XExcep("0x0050");
            if (od.status != 1) throw new XExcep("0x0026");

            if (od.pay_way == 2) return new od() { amount = od.yf_amount.Value.ToString("F2"), id = od.order_id };

            var co = Wx.Pay.MdOrder(od.no, od.no + "", ((int)(od.yf_amount * 100)).ToString(), "http://" + cfg.domain + "/wx/notify-1-" + od.order_id + ".html", cu.wx_opid, cfg.wx_appid, cfg.wx_mch_id, cfg.wx_paykey, false);

            CacheHelper.Remove("pay." + cu.id);

            if (co.return_code == "FAIL") throw new XExcep(co.return_msg);
            if (co.result_code == "FAIL") throw new XExcep(co.err_code + "," + co.err_code_des);
            if (string.IsNullOrEmpty(co.prepay_id)) throw new XExcep("0x0051");

            od.wx_no = co.prepay_id;

            var ps = new Dictionary<string, string>();
            ps.Add("appId", cfg.wx_appid);
            ps.Add("timeStamp", Tools.GetGreenTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            ps.Add("nonceStr", Tools.GetRandRom(24, 3));
            ps.Add("package", "prepay_id=" + od.wx_no);
            ps.Add("signType", "MD5");

            var r = new od()
            {
                id = od.order_id,
                amount = od.yf_amount.Value.ToString("F2"),
                ns = ps["nonceStr"],
                ts = ps["timeStamp"],
                pkg = ps["package"],
                sign = Wx.ToSign(ps, false, cfg.wx_paykey)
            };

            return r;
        }

        private XResp balancePay()
        {

            var c = CacheHelper.Get<string>("pay." + cu.id);
            if (!string.IsNullOrEmpty(c)) throw new XExcep("0x0048");

            CacheHelper.Save("pay." + cu.id, "1");
            var od = cu.x_order.FirstOrDefault(o => o.order_id == id);
            if (od.pay_amount > 0) throw new XExcep("0x0050");
            if (od.status != 1) throw new XExcep("0x0026");

            if (od.pay_way == 2) return new od() { amount = od.yf_amount.Value.ToString("F2"), id = od.order_id };

            CacheHelper.Remove("pay." + cu.id);

            //对订单状态的处理不通过回掉进行
            if (cu.balance < od.yf_amount) throw new XExcep("0x0052");
            cu.balance -= od.yf_amount;
            od.pay_time = DateTime.Now;
            od.pay_amount = od.yf_amount;
            od.status = 2;
            SubmitDBChanges();

            var r = new od()
            {
                id = od.order_id,
                amount = od.yf_amount.Value.ToString("F2"),

            };

            return r;
        }

        protected override XResp Execute()
        {
            if (pay_way == 1)
                return wechatPay();
            else
                return balancePay();
        }
        public class od : XResp
        {
            public long id { get; set; }
            public string amount { get; set; }
            public string ts { get; set; }
            public string ns { get; set; }
            public string pkg { get; set; }
            public string sign { get; set; }
        }
    }
}
