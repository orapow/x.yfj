using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.refund {
    public class list : xmg {
        protected override int powercode {
            get {
                return 2;
            }
        }
        protected override string GetParmNames {
            get {
                return "st";
            }
        }
    }
}
