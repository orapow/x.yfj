﻿using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Web.Com;

namespace X.App.Apis.mgr
{
    public class logout : xmg
    {
        protected override int powercode
        {
            get
            {
                return 0;
            }
        }
        protected override XResp Execute()
        {
            var k = GetReqParms("mgr_ad");
            CacheHelper.Remove("mgr." + k);
            return new XResp();
        }
    }
}
