using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Web;

namespace X.App.Views.sder
{
    public class detail : _sd
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var od = DB.x_order.FirstOrDefault(o => o.order_id == id);
            if (od == null) throw new XExcep("0x0024");
            dict.Add("od", od);
            dict.Add("sdm", od.send_man.Split(' '));
        }
    }
}
