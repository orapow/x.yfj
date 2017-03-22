using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.wx.addr
{
    public class edit : _wx
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id-sel";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("cs", GetDictList("sys.city", cu.city + ""));
            if (id > 0)
            {
                var ent = cu.x_address.FirstOrDefault(o => o.address_id == id);
                dict.Add("ent", ent);
            }
        }
    }
}
