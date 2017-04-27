using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.wx.order {
    public class succ : _wx {
        public int id { get; set; }
        public int fromDeposit { get; set; }
        protected override string GetParmNames {
            get {
                return "id-fromDeposit";
            }
        }
        protected override void InitDict() {
            base.InitDict();
            if (fromDeposit == 2)
                dict.Add("od", cu.x_charge.FirstOrDefault(o => o.charge_id == id));
            else
                dict.Add("od", cu.x_order.FirstOrDefault(o => o.order_id == id));
        }
    }
}
