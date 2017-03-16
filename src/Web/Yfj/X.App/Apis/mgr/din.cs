using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Core.Plugin;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Apis;
using X.Web.Com;

namespace X.App.Apis.mgr
{
    public class din : Api
    {
        public string name { get; set; }
        public string val { get; set; }
        public string up { get; set; }
        protected override XResp Execute()
        {
            Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if (DB.x_dict.Count(o => o.value == val && o.code == "sys.city") > 0) throw new XExcep("T值已经存在", name + "," + val);

            var ct = new x_dict()
            {
                name = name,
                value = val,
                code = "sys.city",
                img = ""
            };
            if (!string.IsNullOrEmpty(up))
            {
                var u = DB.x_dict.FirstOrDefault(o => o.value == up && o.code == "sys.city");
                ct.upval = u.upval + "-" + val;
            }
            else
            {
                ct.upval = "0";
            }

            DB.x_dict.InsertOnSubmit(ct);

            SubmitDBChanges();

            return new XResp() { msg = "成功" };

        }
    }
}
