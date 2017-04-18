﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.sman {
    public class stati : xmg {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        protected override XResp Execute() {
            var r = new Resp_List();
            r.page = page;

            var q = from d in GetDictList("user.sman", "0")
                    select d.value;

            var ods = from d in DB.x_order
                      where q.Contains(d.x_user.invter + "")
                      select d;
            //var us = DB.x_user.Where(u => q.Contains(u.invter + "")).Select(u => (long?)u.user_id);
            var list = ods.Where(o=>o.city==mg.city).GroupBy(o => o.x_user.invter).Select(g => new {
                name = GetDictName("user.sman", g.Key),
                member_count = DB.x_user.Where(u => u.invter == g.Key).Count(),
                order_total = g.Count(),
                order_amount = g.Sum(order => order.yf_amount),
                finish_amount = ods.Where(od => od.status == 5 && od.x_user.invter == g.Key)//&& od.x_refund.First(o=>o.order_id==od.order_id) != null)
                                    .Sum(order => order.yf_amount)
            }).OrderByDescending(o=>o.finish_amount);
            //list;


            //var o=DB.x_order.Where(c=>us.Contains(c.user_id));

            //var order=from od in DB.x_order.Select(o=>o.x_user.invter).GroupBy(o=>o.);

            //if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key));

            r.items = list.Skip((page - 1) * limit).Take(limit);
            r.count = list.Count();
            return r;
        }

    }
}