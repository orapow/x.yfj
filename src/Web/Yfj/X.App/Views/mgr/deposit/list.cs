using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.deposit
{
    public class list : xmg
    {
        public int st { get; set; }
        protected override int powercode
        {
            get
            {
                return 2;
            }
        }
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
            dict["st"] = st == 0 ? 1 : st;
        }
    }
}
