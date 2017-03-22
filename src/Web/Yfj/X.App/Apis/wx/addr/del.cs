using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.wx.addr
{
    public class del : _wx
    {
        public int id { get; set; }
        protected override XResp Execute()
        {
            var ad = cu.x_address.FirstOrDefault(o => o.address_id == id);
            if (ad == null) throw new XExcep("T地址不存在");

            DB.x_address.DeleteOnSubmit(ad);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
