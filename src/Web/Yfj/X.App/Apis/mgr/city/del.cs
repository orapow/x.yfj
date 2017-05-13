using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.city
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) throw new XExcep("0x0015");

            DB.x_dict.DeleteOnSubmit(ent);

            SubmitDBChanges();

            CacheHelper.Remove("dict." + ent.code);

            return new XResp();
        }
    }
}
