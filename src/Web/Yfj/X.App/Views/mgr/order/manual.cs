using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views.mgr.order {
    public class manual : xmg {
        public int id { get; set; }
        public int cp { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }


        protected override string GetParmNames {
            //传参数
            get {
                return "id-cp";
            }
        }

        //List<x_goods_field> fs = null;

        protected override void InitDict() {
            base.InitDict();
        }
    }
}
