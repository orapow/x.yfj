using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.wx.user
{
    public class mpwd : _wx
    {
        public String tel { get; set; }
        public String newPswd { get; set; }
        public String checkCode { get; set; }
        protected override void InitDict() {
            base.InitDict();
            dict.Add("cu",cu);
        }
    }
}
