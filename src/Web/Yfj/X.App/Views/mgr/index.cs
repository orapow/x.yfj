using System;
using System.Collections.Generic;
using System.Linq;

namespace X.App.Views.mgr
{
    public class index : xmg
    {
        protected override int powercode
        {
            get
            {
                return 0;
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("cities", GetDictList("sys.city", "0"));
        }
    }
}
