using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.article
{
    public class save : xmg
    {
        public int id { get; set; }
        public int cate { get; set; }//分类
        public string topic { get; set; }//名称
        public string tourl { get; set; }
        public string remark { get; set; }
        public string desc { get; set; }
        public int sort { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            x_article ent = null;
            if (id > 0)
            {
                ent = DB.x_article.SingleOrDefault(o => o.article_id == id);
                if (ent == null) throw new XExcep("0x0005");
            }

            if (cate > 0)
            {
                var ct = DB.x_dict.FirstOrDefault(o => o.code == "article.cate" && o.value == cate + "");
                if (ct == null) throw new XExcep("T分类不存在");
            }

            if (ent == null) ent = new x_article() { ctime = DateTime.Now, hits = 0 };

            ent.title = topic;
            ent.cate = cate;
            ent.content = desc;
            ent.mtime = DateTime.Now;
            ent.sort = sort;
            ent.tourl = tourl;

            if (ent.article_id == 0)
            {
                DB.x_article.InsertOnSubmit(ent);
                SubmitDBChanges();
            }

            SubmitDBChanges();

            return new XResp();
        }
    }
}
