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
        public string pwd { get; set; }//密码
        public string mail { get; set; }
        public int role { get; set; }//权限(1:客服,2:财务)
        public int ct { get; set; }
        protected override int powercode
        {
            get
            {
                return 3;
            }
        }



        protected override Web.Com.XResp Execute()
        {
            x_mgr ad = null;

            if (id > 0) ad = DB.x_mgr.SingleOrDefault(o => o.mgr_id == id);
            if (ad == null) ad = new x_mgr() { ctime = DateTime.Now };

            if (mg.city == 62 && ad.mgr_id != 1) ad.city = ct;
            else ad.city = mg.city;
            if (mg.role_id == 3) ad.role_id = role;

            if (DB.x_mgr.Count(o => o.uid == uid && o.mgr_id != ad.mgr_id) > 0) throw new XExcep("0x0061");

            ad.uid = uid;
            if (!string.IsNullOrEmpty(pwd) && id > 0) ad.pwd = Secret.MD5(pwd);
            ad.name = name;
            ad.tel = tel;
            ad.email = mail;
            ad.ctime = DateTime.Now;

            if (ad.mgr_id == 0) DB.x_mgr.InsertOnSubmit(ad);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
