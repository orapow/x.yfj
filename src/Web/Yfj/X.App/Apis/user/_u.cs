using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;

namespace X.App.Apis.user
{
    public class _u : xapi
    {
        protected x_user cu { get; set; }
        protected virtual bool needuser { get { return true; } }
        protected override void InitApi()
        {
            base.InitApi();

            var ukey = GetReqParms("ukey");
            if (!string.IsNullOrEmpty(ukey)) cu = CacheHelper.Get<x_user>("u.cu." + ukey);
            cu = DB.x_user.FirstOrDefault(o => o.uid == "test");

            if (cu == null && needuser) throw new XExcep("T用户未登陆或登陆超时");

        }
    }
}
