using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.order
{
    public class send : xmg
    {
        public int id { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var od = DB.x_order.FirstOrDefault(o => o.order_id == id);

            if (od == null) throw new XExcep("T订单不存在");
            if (od.city != mg.city) throw new XExcep("T订单不在当前城市");
            if (od.status != 3) throw new XExcep("T订单状态不正确");

            od.status = 4;
            od.send_man = name + " " + tel;
            od.out_time = DateTime.Now;

            SubmitDBChanges();

            return new XResp();
        }
    }
}
