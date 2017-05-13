using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.cate
{
    public class list : xmg
    {
        /// <summary>
        /// 是否有子级
        /// 1、有
        /// </summary>
        public int hasc { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "hasc";
            }
        }
        protected override int powercode {
            get {
                return 1;
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var dt = DB.x_dict.FirstOrDefault(o => o.code == "goods.cate" && o.value == null);
            if (dt != null) dict.Add("dname", dt.name);
        }
    }
}
