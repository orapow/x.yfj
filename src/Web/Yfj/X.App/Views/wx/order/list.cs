using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;

namespace X.App.Views.wx.order
{
    public class list : _wx
    {
        public int st { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "st";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var ods = cu.x_order.Where(o => o.status == st).OrderByDescending(o => o.ctime).Select(o => new
            {
                id = o.order_id,
                gs = o.x_order_detail.Select(d => new { d.name, d.cover }),
                o.rec_man,
                o.rec_tel,
                o.rec_addr,
                o.yf_amount
            });
            dict.Add("ods", ods);
        }
    }
}
