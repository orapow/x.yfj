using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;

namespace X.App.Views.wx.user {
    public class mpwd : _wx {
        protected override void InitDict() {
            base.InitDict();
            var key = Tools.GetRandRom(12, 3);
            dict.Add("key", key);
            Context.Response.SetCookie(new System.Web.HttpCookie("code.send", key));
        }
    }
}
