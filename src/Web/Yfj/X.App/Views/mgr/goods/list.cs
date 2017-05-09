using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.goods {
    public class list : xmg {
        /// <summary>
        /// 状态：
        /// 1、草稿
        /// 2、已上架
        /// 3、已下架
        /// 4、已删除
        /// </summary>
        public int st { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override string GetParmNames {
            get {
                return "st";
            }
        }
    }
}
