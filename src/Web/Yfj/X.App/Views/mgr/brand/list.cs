using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.brand
{
    public class list : xmg
    {
        public string ct { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "ct";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            if (!string.IsNullOrEmpty(ct)) dict.Add("cate", ct + "|" + GetDictName("goods.cate", ct));
        }
    }
}
