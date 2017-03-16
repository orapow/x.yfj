using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.addr
{
    public class del : _u
    {
        [ParmsAttr(name = "id", min = 1)]
        public int id { get; set; }


        protected override XResp Execute()
        {
            var ad = DB.x_address.FirstOrDefault(o => o.address_id == id);
            if (ad == null) throw new XExcep("T收货地址不存在");
            if (ad.user_id != cu.user_id) throw new XExcep("T收货地址不属于你");

            DB.x_address.DeleteOnSubmit(ad);

            SubmitDBChanges();

            return new XResp();

        }
    }
}
