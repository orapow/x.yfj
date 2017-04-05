using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.order
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int st { get; set; }
        public string key { get; set; }
        protected override XResp Execute()
        {
            var r = new Resp_List();
            var q = from o in DB.x_order
                    select o;

            if (mg.x_role.power != "###") q = q.Where(o => o.city == mg.city);

            if (st > 0)
            {
                if (st == 1) q = q.Where(o => o.pay_way == 1);
                if (st == 4) q = q.Where(o => o.pay_way == 2 && o.status > 2 && !(o.pay_amount > 0));
                else q = q.Where(o => o.status == st);
            }

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.no == key || o.user_remark.Contains(key) || o.rec_man.Contains(key) || o.rec_tel.Contains(key));

            r.count = q.Count();
            var sts = "|待付款|待确认|待发货|待签收|已完成".Split('|');
            r.items = q.OrderByDescending(o => o.ctime).ToList().Select(o => new
            {
                id = o.order_id,
                uid = o.user_id,
                un = o.x_user.nickname,
                up = o.x_user.headimg,
                gs = string.Join(" ", o.x_order_detail.Select(d => "<img src='" + d.cover + "' class='gd' title='" + d.name + "' />").ToArray()),
                o.no,
                way = o.pay_way == 1 ? "在线支付" : "货到付款",
                o.rec_man,
                o.rec_tel,
                o.status,
                st_name = o.status > 0 && o.status < 6 ? sts[o.status.Value] : "未知：" + o.status,
                o.rec_addr,
                o.yf_amount,
                o.pay_amount,
                send = o.send_man,
                send_date = o.send_date.Value.ToString("yyyy-MM-dd"),
                o.send_time,
                ctime = o.ctime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                remark = o.user_remark
            });

            r.page = page;

            return r;
        }

    }
}
