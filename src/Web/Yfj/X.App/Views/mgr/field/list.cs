using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.field
{
    public class list : xmg
    {
        [ParmsAttr(name = "商品品类", min = 1)]
        public int pid { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "pid";
            }
        }
    }
}
