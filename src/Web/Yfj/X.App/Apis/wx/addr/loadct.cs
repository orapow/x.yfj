using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.wx.addr
{
    public class loadct : _wx
    {
        public string up { get; set; }
        protected override XResp Execute()
        {
            var r = new Resp_List() { };
            r.items = GetDictList("sys.city", up).Select(o => new { o.name, o.value });
            return r;
        }
    }
}
