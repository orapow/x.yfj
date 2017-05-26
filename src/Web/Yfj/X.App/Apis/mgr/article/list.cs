using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.article
{
    public class list : xmg
    {

        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        protected override int powercode {
            get {
                return 1;
            }
        }
        protected override XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from et in DB.x_article
                    select et;

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.title.Contains(key));

            r.items = q.OrderByDescending(o => o.mtime).Skip((page - 1) * limit).Take(limit).ToList().Select(et => new
            {
                id = et.article_id,
                et.title,
                cate = GetDictName("article.cate", et.cate),
                et.tourl,
                et.hits,
                et.sort,
                mtime = et.mtime.Value.ToString("yyyy-MM-dd HH:mm")
            });
            r.count = q.Count();

            return r;
        }

    }
}
