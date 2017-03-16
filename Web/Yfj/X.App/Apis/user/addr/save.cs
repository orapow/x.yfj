using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.addr
{
    public class save : _u
    {
        public int id { get; set; }
        [ParmsAttr(name = "收货人", req = true)]
        public string name { get; set; }
        [ParmsAttr(name = "联系电话", req = true)]
        public string tel { get; set; }
        public string she { get; set; }
        public string shi { get; set; }
        public string qu { get; set; }
        public string zh { get; set; }
        public string addr { get; set; }
        public string st { get; set; }
        public int def { get; set; }

        protected override XResp Execute()
        {
            x_address ad = null;
            if (id > 0)
            {
                ad = DB.x_address.FirstOrDefault(o => o.address_id == id);
                if (ad == null) throw new XExcep("T收货地址不存在");
                if (ad.user_id != cu.user_id) throw new XExcep("T收货地址不属于你");
            }
            else ad = new x_address() { user_id = cu.user_id, ctime = DateTime.Now };

            ad.name = name;
            ad.tel = tel;
            ad.sheng = she;
            ad.shi = shi;
            ad.qu = qu;
            ad.zhen = zh;
            ad.addr = addr;
            ad.stime = st;
            ad.range = GetDictName("sys.city", she + "," + shi + "," + qu + "," + zh, " ");

            if (id == 0)
            {
                if (cu.x_address.Count() == 0) ad.def = true;
                DB.x_address.InsertOnSubmit(ad);
            }
            else
            {
                if (def == 1)
                {
                    var d = DB.x_address.FirstOrDefault(o => o.address_id != id && o.user_id == cu.user_id && o.def == true);
                    if (d != null) d.def = false;
                    ad.def = true;
                }
                else ad.def = false;
            }

            SubmitDBChanges();

            return new XResp();

        }
    }
}
