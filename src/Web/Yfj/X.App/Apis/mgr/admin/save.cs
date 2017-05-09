using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.admin
{
    public class save : xmg
    {
        public int id { get; set; }
        public string name { get; set; }//用户名称
        public string tel { get; set; }//联系电话
        public string uid { get; set; }//帐号
        public string password { get; set; }//密码
        public String mail { get; set; }
        public int role { get; set; }//权限(1:客服,2:财务)
        protected override int powercode
        {
            get
            {
                return 1;
            }
        }



        protected override Web.Com.XResp Execute()
        {
            x_mgr ad = new x_mgr();
            if (id > 0)
            {
                ad = DB.x_mgr.SingleOrDefault(o => o.mgr_id == id);
                if (ad == null) throw new XExcep("0x0005");
            }
            else
            {
                ad = DB.x_mgr.SingleOrDefault(o => o.name == name || o.uid == uid);
                if (ad != null) throw new XExcep("0x0007");
                ad = new x_mgr();
            }
            ad.uid = uid;
            if (!string.IsNullOrEmpty(password)) ad.pwd = Secret.MD5(password);
            ad.name = name;
            ad.tel = tel;
            ad.role_id = role;
            ad.email = mail;
            ad.ctime = DateTime.Now;

            if (ad.mgr_id == 0) DB.x_mgr.InsertOnSubmit(ad);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
