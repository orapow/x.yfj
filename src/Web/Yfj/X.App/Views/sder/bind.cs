using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Web;

namespace X.App.Views.sder
{
    public class bind : _sd
    {
        protected override void InitDict()
        {
            base.InitDict();
            if (sd != null) Context.Response.Redirect("/sder/list.html");
            var u = Com.Wx.User.GetUserInfo(opid, Com.Wx.GetToken(cfg.wx_appid, cfg.wx_scr));
            if (string.IsNullOrEmpty(u.errmsg))
            {
                dict.Add("img", u.headimgurl);
                dict.Add("nk", u.nickname);
                dict.Add("opid", opid);
            }
            else throw new XExcep("请先关注公众号");
        }
    }
}
