using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;

namespace X.App.Views.mgr.sale {
    public class attr : xmg {
        public int id { get; set; }
        [ParmsAttr(name = "字段ID")]
        public int fid { get; set; }

        protected override int powercode {
            get {
                return 1;
            }
        }
        protected override string GetParmNames {
            //传参数
            get {
                return "id-fid";
            }
        }
        protected override void InitDict() {
            base.InitDict();

            //if (fid > 0)
            //{
            //    dict.Add("fd", DB.x_goods_field.FirstOrDefault(o => o.goods_field_id == fid));
            //}

            //if (id > 0)
            //{
            //    var ent = DB.x_goods_val.FirstOrDefault(o => o.goods_val_id == id);
            //    if (ent == null) throw new XExcep("T字段值不存在");
            //    dict.Add("item", ent);
            //}
        }
    }
}
