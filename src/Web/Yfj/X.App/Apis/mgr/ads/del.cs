using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.ads
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var ent = DB.x_ad.FirstOrDefault(o => o.ad_id == id);
            if (ent == null) throw new XExcep("T广告不存在");

            DB.x_ad.DeleteOnSubmit(ent);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
