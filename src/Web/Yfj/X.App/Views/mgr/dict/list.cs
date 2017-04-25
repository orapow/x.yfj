using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.dict {
    public class list : xmg {
        [ParmsAttr(name = "代号", req = true)]
        public string code { get; set; }
        /// <summary>
        /// 是否有子级
        /// 1、有
        /// </summary>
        public int hasc { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override string GetParmNames {
            get {
                return "code-hasc";
            }
        }
        protected override void InitDict() {
            base.InitDict();
            var dt = DB.x_dict.FirstOrDefault(o => o.code == code && o.value == null);
            if (dt != null) dict.Add("dname", dt.name);
        }
    }
}
