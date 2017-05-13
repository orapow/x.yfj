using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;

namespace X.App.Apis.sder
{
    public class _sd : xapi
    {
        protected x_dict sd = null;

        protected override void InitApi()
        {
            base.InitApi();
            var sd_key = GetReqParms("sd_key");
            if (!string.IsNullOrEmpty(sd_key))
                sd = DB.x_dict.FirstOrDefault(o => o.code == "user.sender" && o.jp == sd_key);
            if (sd == null) throw new XExcep("0x0004");
        }
    }
}
