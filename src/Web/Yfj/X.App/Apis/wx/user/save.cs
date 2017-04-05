using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.wx.user
{
    public class save : _wx
    {
        public string cn { get; set; }
        public int ct { get; set; }

        protected override XResp Execute()
        {
            if (ct > 0) cu.city = ct;
            if (!string.IsNullOrEmpty(cn)) cu.name = cn;
            SubmitDBChanges();
            return new XResp();
        }
    }
}
