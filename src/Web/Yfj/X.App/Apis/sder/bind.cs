using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.sder
{
    public class bind : xapi
    {
        [ParmsAttr(name = "Opid", req = true)]
        public string opid { get; set; }
        [ParmsAttr(name = "手机号", req = true)]
        public string tel { get; set; }
        [ParmsAttr(name = "短信验证码", req = true)]
        public string code { get; set; }
        public string img { get; set; }

        protected override XResp Execute()
        {
            var mcode = CacheHelper.Get<string>("sms.code." + tel);
            if (mcode != code) throw new XExcep("0x0054");

            var sd = DB.x_dict.FirstOrDefault(o => o.code == "user.sender" && o.f3 == tel);
            if (sd == null) throw new XExcep("0x0057");

            sd.img = img;
            sd.jp = opid;
            SubmitDBChanges();

            return new XResp();
        }

    }
}
