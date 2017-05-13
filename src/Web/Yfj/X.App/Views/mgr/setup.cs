using System;
using System.Collections.Generic;
using System.Linq;

namespace X.App.Views.mgr
{
    public class setup : xmg
    {
        public int tb { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "tb";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict["tb"] = tb;
        }
    }
}
