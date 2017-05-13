using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views.mgr.deposit
{
    public class manual : xmg
    {
        protected override int powercode
        {
            get
            {
                return 2;
            }
        }
    }
}
