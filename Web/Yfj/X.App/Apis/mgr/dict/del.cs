using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.dict
{
    public class del : xmg
    {
        [ParmsAttr(name = "代号", req = true)]
        public string code { get; set; }
        [ParmsAttr(min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) throw new XExcep("T分类不存在");

            var upv = "";
            if (ent.upval != "0") upv = ent.upval + "-" + ent.value;
            else upv = ent.value;

            var childs = DB.x_dict.Where(o => o.upval.StartsWith(upv));
            foreach (var e in childs.ToList())
            {
                if (e.upval == ent.value) e.upval = ent.upval;
                if (ent.upval == "0") e.upval = e.upval.Replace(ent.value + "-", "");
                else e.upval = e.upval.Replace("-" + ent.value, "");
            }

            var goods = DB.x_goods.Where(o => o.cate_id.StartsWith(ent.value));
            foreach (var g in goods.ToList()) g.cate_id = "";

            DB.x_dict.DeleteOnSubmit(ent);

            CacheHelper.Remove("dict." + ent.code);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
