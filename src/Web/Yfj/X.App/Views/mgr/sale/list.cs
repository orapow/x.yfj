using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.sale {
    public class list : xmg {
        /// <summary>
        /// 状态：
        /// 1、草稿
        /// 2、已上架
        /// 3、已下架
        /// 4、已删除
        /// </summary>
        public String st { get; set; }
        protected override string GetParmNames {
            get {
                return "st";
            }
        }

        protected override void InitDict() {
            base.InitDict();
            if (!string.IsNullOrEmpty(st)) dict.Add("sale", st + "|" + GetDictName("sale", st));//?????
        }
    }
}
