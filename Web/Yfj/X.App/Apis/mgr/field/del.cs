using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.field
{
    public class del : xmg
    {
        [ParmsAttr(name = "属性ID", min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            //var ent = DB.x_goods_field.FirstOrDefault(o => o.goods_field_id == id);
            //if (ent == null) throw new XExcep("T属性不存在");

            //var vals = DB.x_goods_val.Where(o => o.goods_field_id == id);
            //DB.x_goods_val.DeleteAllOnSubmit(vals);

            //DB.x_goods_field.DeleteOnSubmit(ent);

            //SubmitDBChanges();

            return new XResp();
        }
    }
}
