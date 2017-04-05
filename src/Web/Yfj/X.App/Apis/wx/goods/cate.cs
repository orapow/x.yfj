using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.wx.goods
{
    public class cate : _wx
    {
        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }
        public string pid { get; set; }
        protected override XResp Execute()
        {
            var r = new Resp_List();
            r.items = GetDictList("goods.cate", pid).Select(o => new { o.value, o.name });
            return r;
        }
    }
}
