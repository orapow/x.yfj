using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;

namespace X.App.Apis.wx
{
    public class _wx : xapi
    {
        protected x_user cu = null;

        protected virtual bool nd_user { get { return true; } }

        protected override void InitApi()
        {
            base.InitApi();
            var cu_key = GetReqParms("cu_key");
            if (!string.IsNullOrEmpty(cu_key)) cu = DB.x_user.FirstOrDefault(o => o.ukey == cu_key);
            if (cu == null && nd_user) throw new XExcep("0x0004");
        }
    }
}
