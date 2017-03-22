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

namespace X.App.Apis.wx.order
{
    public class md : _wx
    {
        [ParmsAttr(name = "支付方式", min = 1)]
        public int way { get; set; }
        [ParmsAttr(name = "收货地址", min = 1)]
        public int ad { get; set; }
        //public int upst { get; set; }
        //public int fl { get; set; }
        [ParmsAttr(name = "配送日期", req = true)]
        public string date { get; set; }
        [ParmsAttr(name = "送货时间", req = true)]
        public string time { get; set; }
        public string remark { get; set; }
        protected override XResp Execute()
        {
            var c = CacheHelper.Get<string>("pay." + cu.id);
            if (!string.IsNullOrEmpty(c)) throw new XExcep("T当前订单正在处理中，请稍后...");

            var adr = cu.x_address.FirstOrDefault(o => o.address_id == ad);
            if (adr == null) throw new XExcep("T收货地址不存在");

            var gds = cu.x_cart.Where(o => o.sel == true);
            if (gds == null || gds.Count() == 0) throw new XExcep("T购物车内没有商品");

            CacheHelper.Save("pay." + cu.id, "1");

            var od = new x_order();
            od.no = DateTime.Now.ToString("yyyyMMddHHmmssff") + Tools.GetRandNext(100, 999);
            od.ctime = DateTime.Now;
            od.status = 1;

            foreach (var g in gds)
            {
                od.x_order_detail.Add(new x_order_detail()
                {
                    count = g.count,
                    cover = g.cover,
                    goods_id = g.goods_id,
                    name = g.goods_name,
                    price = g.price,
                    stand = g.desc,
                    total_price = g.count * g.price
                });
            }

            od.fav_amount = 0;//优惠金额
            od.fav_remark = "";//优惠说明
            od.freight_amount = 0;//云费
            //od.onfloor = fl;
            od.amount = od.x_order_detail.Sum(o => o.count * o.price);
            od.pay_way = way;

            od.send_date = DateTime.Parse(date);
            od.send_time = time;

            od.rec_addr = adr.shi + " " + adr.qu + " " + adr.zhen + " " + adr.addr;
            od.rec_man = adr.name;
            od.rec_tel = adr.tel;
            od.ret_integral = 0;
            //od.uptype = upst;
            od.up_amount = 0;//上楼费用
            od.user_remark = remark;
            od.yf_amount = od.amount - od.fav_amount + od.up_amount + od.freight_amount;//应用金额

            cu.x_order.Add(od);

            DB.x_cart.DeleteAllOnSubmit(gds.ToList());
            if (od.pay_way == 2) od.status = 2;
            SubmitDBChanges();

            CacheHelper.Remove("pay." + cu.id);

            if (od.pay_way == 2) return new od() { amount = od.yf_amount.Value.ToString("F2"), id = od.order_id };

            var co = Wx.Pay.MdOrder(od.no, od.order_id + "", ((int)(od.yf_amount * 100)).ToString(), "http://" + cfg.domain + "/wx/notify-" + od.no + ".html", cu.wx_opid, cfg.wx_appid, cfg.wx_mch_id, cfg.wx_paykey, false);

            if (co.return_code == "FAIL") throw new XExcep(co.return_msg);
            if (co.result_code == "FAIL") throw new XExcep(co.err_code + "," + co.err_code_des);
            if (string.IsNullOrEmpty(co.prepay_id)) throw new XExcep("T预付款号为空");

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
