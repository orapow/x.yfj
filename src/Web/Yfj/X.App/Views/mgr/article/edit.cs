using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views.mgr.article
{
    public class edit : xmg
    {
        public int id { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override string GetParmNames
        {
            //传参数
            get
            {
                return "id";
            }
        }

        //List<x_goods_field> fs = null;

        protected override void InitDict()
        {
           
            base.InitDict();
            if (id > 0)
            {
                var ent = DB.x_article.SingleOrDefault(o => o.article_id == id);
                if (ent == null) throw new XExcep("0x0005");

                dict.Add("item", ent);
                dict.Add("cate", ent.cate + "|" + GetDictName("article.cate", ent.cate));
            }
        }
    }
}
