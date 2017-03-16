using System;
using System.Collections.Generic;
using System.Linq;
using X.Web;

namespace X.App.Views.mgr.user
{
    public class attr : xmg
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            if (id > 0)
            {
                var ent = DB.x_user.SingleOrDefault(o => o.user_id == id);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
            }
        }
    }
}
