using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.wx.addr
{
    public class save : _wx
    {
        public int id { get; set; }
        public int c1 { get; set; }
        public int c2 { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string addr { get; set; }
        protected override XResp Execute()
        {
            x_address ad = null;
            if (id > 0) ad = cu.x_address.FirstOrDefault(o => o.address_id == id);
            if (ad == null) ad = new x_address() { ctime = DateTime.Now, user_id = cu.id };

            ad.shi = GetDictName("sys.city", cu.city + "");
            ad.qu = GetDictName("sys.city", c1);
            ad.zhen = GetDictName("sys.city", c2);
            ad.tel = tel;
            ad.addr = addr;
            ad.name = name;

            if (ad.address_id == 0) DB.x_address.InsertOnSubmit(ad);
            SubmitDBChanges();

            return new XResp() { msg = ad.address_id + "" };
        }
    }
}
