using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.admin
{
    public class del : xmg
    {
        public int id { get; set; }

       
        protected override Web.Com.XResp Execute()
        {
            var ag = DB.x_mgr.SingleOrDefault(o => o.mgr_id == id);
            if (ag == null) throw new XExcep("x0005");

            DB.x_mgr.DeleteOnSubmit(ag);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
