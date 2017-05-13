using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;

namespace X.App.Views.mgr.brand
{
    public class select : xmg
    {
        public string cate { get; set; }
        protected override int powercode
        {
            get
            {
                return 0;
            }
        }

        protected override string GetParmNames
        {
            get
            {
                return "key-cate";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var list = GetDictList("goods.brand", "0").Where(o => o.f3 == cate).ToList();
            list.Insert(0, new x_dict() { name = "请选择", value = "0" });
            dict.Add("dict", list);
        }
        public override string GetTplFile()
        {
            return "/com/dict";
        }
    }
}
