using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user
{
    public class addr : _u
    {
        protected override void InitDict()
        {
            base.InitDict();

            var def = cu.x_address.FirstOrDefault(o => o.def == true);
            if (def != null) dict.Add("def", def);

            dict.Add("list", cu.x_address.Where(o => o.def != true).ToList());

        }
    }
}
