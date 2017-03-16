using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;

namespace X.App.Views.user
{
    public class addr_edit : _u
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
            if (id > 0)
            {

                var ad = DB.x_address.FirstOrDefault(o => o.address_id == id);
                if (ad == null) throw new XExcep("T收货地址不存在");
                if (ad.user_id != cu.user_id) throw new XExcep("T收货地址不属于你");

                dict.Add("item", ad);

            }
        }

    }
}
