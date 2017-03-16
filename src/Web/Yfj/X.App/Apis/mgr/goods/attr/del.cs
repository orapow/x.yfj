using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.goods.attr
{
    public class del : xmg
    {
        [ParmsAttr(name = "属性ID", req = true)]
        public string ids { get; set; }

        protected override XResp Execute()
        {
            //var vals = DB.x_goods_val.Where(o => ids.Split(',').Contains(o.goods_val_id + ""));

            //DB.x_goods_val.DeleteAllOnSubmit(vals);

            //SubmitDBChanges();

            return new XResp();
        }
    }
}
