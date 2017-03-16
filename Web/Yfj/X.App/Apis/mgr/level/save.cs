using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.level
{
    public class save : xmg
    {
        public int id { get; set; }
        public string name { get; set; }
        public int sort { get; set; }
        public int off { get; set; }
        public int nd { get; set; }
        public string img { get; set; }

        protected override XResp Execute()
        {
            x_dict ent = null;

            if (id > 0) ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) ent = new x_dict() { code = "user.level" };

            ent.name = name;
            ent.img = img;
            ent.sort = sort;
            ent.f1 = off;
            ent.f2 = nd;
            ent.upval = "0";

            if (ent.id == 0) DB.x_dict.InsertOnSubmit(ent);
            SubmitDBChanges();
            ent.value = ent.id + "";

            SubmitDBChanges();

            CacheHelper.Remove("dict." + ent.code);

            return new XResp();
        }
    }
}
