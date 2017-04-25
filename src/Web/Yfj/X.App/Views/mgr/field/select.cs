using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;

namespace X.App.Views.mgr.field {
    public class select : xmg {
        [ParmsAttr(name = "商品品类", min = 1)]
        public int pid { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }


        protected override string GetParmNames {
            get {
                return "pid";
            }
        }

        protected override void InitDict() {
            //var q = from f in DB.x_goods_field.Where(o => o.cate_id == pid)
            //        select new
            //        {
            //            f.name,//fid-type-pr-st-mt
            //            value = f.goods_field_id + "-" + (f.type ?? 0) + "-" + (f.ch_price ?? 0) + "-" + (f.ch_stock ?? 0) + "-" + (f.muti_val ?? 0)
            //        };

            //dict.Add("dict", q.ToList());

            //if (q.Count() == 0) dict["nodata"] = "此品类还没配置字段";
        }

        public override string GetTplFile() {
            return "com/dict";
        }
    }
}
