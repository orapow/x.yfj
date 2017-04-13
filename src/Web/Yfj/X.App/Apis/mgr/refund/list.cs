using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.refund
{
    public class list : xmg//类名必须跟前段api调用的名称相同
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int st { get; set; }
        public string key { get; set; }
        protected override XResp Execute()
        {
            var r = new Resp_List();
            var q = from o in DB.x_refund select o;

            if (mg.x_role.power != "###") q = q.Where(o => o.x_order.city == mg.city);

            if (st > 0)
            {
                if (st == 1) q = q.Where(o => o.x_order.pay_way == 1);
                if (st == 4) q = q.Where(o => o.x_order.pay_way == 2 && o.status > 2 && !(o.x_order.pay_amount > 0));
                else q = q.Where(o => o.status == st);
            }

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.x_order.no == key || o.x_order.user_remark.Contains(key) || o.x_order.rec_man.Contains(key) || o.x_order.rec_tel.Contains(key));

            r.count = q.Count();
            var sts = "|待付款|待确认|待发货|待签收|已完成".Split('|');
            r.items = q.OrderByDescending(o => o.ctime).ToList().Select(o => new
            {
                id = o.refund_id,//退款操作传参均用refund_id
                order_id=o.order_id,
                uid = o.x_order.user_id,
                un = o.x_order.x_user.nickname,
                up = o.x_order.x_user.headimg,
                gs = string.Join(" ", o.x_order.x_order_detail.Select(d => "<img src='" + d.cover + "' class='gd' title='" + d.name + "' />").ToArray()),
                o.x_order.no,
                way = o.x_order.pay_way == 1 ? "在线支付" : "货到付款",
                o.x_order.rec_man,
                o.x_order.rec_tel,
                o.status,
                st_name = o.status > 0 && o.status < 6 ? sts[o.status.Value] : "未知：" + o.status,
                o.x_order.rec_addr,
                o.x_order.yf_amount,
                o.x_order.pay_amount,
                send = o.x_order.send_man,
                send_date = o.x_order.send_date.Value.ToString("yyyy-MM-dd"),
                o.x_order.send_time,
                ctime = o.ctime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                remark = o.x_order.user_remark
            });

            r.page = page;

            return r;
        }

    }
}
