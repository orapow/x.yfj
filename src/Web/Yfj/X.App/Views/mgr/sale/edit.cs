using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views.mgr.sale
{
    public class edit : xmg
    {
        public int id { get; set; }
        protected override int powercode
        {
            get
            {
                return 1;
            }
        }

        protected override string GetParmNames
        {
            //传参数
            get
            {
                return "id";
            }
        }
        

        protected override void InitDict()
        {
            base.InitDict();

            var ent = DB.x_goods.SingleOrDefault(o => o.goods_id == id);
            if (ent == null) throw new XExcep("0x0005");

            dict.Add("g", ent);

        }
    }
}
