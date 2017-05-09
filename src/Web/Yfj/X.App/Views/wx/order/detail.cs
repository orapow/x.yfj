﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Web;

namespace X.App.Views.wx.order
{
    public class detail : _wx
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var od = cu.x_order.FirstOrDefault(o => o.order_id == id);
            var itemRefund = od.x_refund.FirstOrDefault(o => o.order_id == id);
            if (od == null) throw new XExcep("0x0024");
            dict.Add("od", od);
            if (od.send_man != null) dict.Add("sd", od.send_man.Split(' '));
            dict.Add("itemRefund", itemRefund);
        }
    }
}
