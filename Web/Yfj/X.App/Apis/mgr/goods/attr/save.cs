using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.goods.attr
{
    public class save : xmg
    {
        public int id { get; set; }
        public int fid { get; set; }
        public string val { get; set; }
        public string img { get; set; }

        protected override XResp Execute()
        {
            //x_goods_val ent = null;
            //if (id > 0) ent = DB.x_goods_val.FirstOrDefault(o => o.goods_val_id == id);
            //if (ent == null) ent = new x_goods_val() { goods_field_id = fid };

            //ent.val = val;
            //ent.img = img;

            //if (id == 0) DB.x_goods_val.InsertOnSubmit(ent);

            //SubmitDBChanges();

            return new XResp();// { msg = ent.goods_val_id + "" };
        }
    }
}
