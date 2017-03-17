using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.wx
{
    public class cart : _wx
    {
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("gs", cu.x_cart.ToList());
        }
    }
}
