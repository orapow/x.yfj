using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.ads
{
    public class list : xmg
    {

        public int page { get; set; }
        public int limit { get; set; }
        public int pos { get; set; }
        public string key { get; set; }

        protected override XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from et in DB.x_ad
                    select et;

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key));
            if (pos > 0) q = q.Where(o => o.pos == pos);

            r.items = q.OrderByDescending(o => o.ctime).Skip((page - 1) * limit).Take(limit).ToList().Select(et => new
            {
                id = et.ad_id,
                et.name,
                pos = GetDictName("ads.pos", et.pos),
                et.pic,
                et.url,
                et.remark,
                ctime = et.ctime.Value.ToString("yyyy-MM-dd HH;mm")
            });
            r.count = q.Count();
            return r;
        }

    }
}
