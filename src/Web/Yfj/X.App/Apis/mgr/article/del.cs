using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.article
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var ent = DB.x_article.FirstOrDefault(o => o.article_id == id);
            if (ent == null) throw new XExcep("T文章不存在");

            DB.x_article.DeleteOnSubmit(ent);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
