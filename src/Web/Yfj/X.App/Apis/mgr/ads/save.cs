using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.ads
{
    public class save : xmg
    {
        public int id { get; set; }
        public int pos { get; set; }//分类
        public string name { get; set; }//名称
        public string url { get; set; }
        public string pic { get; set; }
        public string desc { get; set; }
        public int sort { get; set; }

        protected override XResp Execute()
        {
            x_ad ent = null;
            if (id > 0)
            {
                ent = DB.x_ad.SingleOrDefault(o => o.ad_id == id);
                if (ent == null) throw new XExcep("0x0005");
            }

            if (pos > 0)
            {
                var ct = DB.x_dict.FirstOrDefault(o => o.code == "ads.pos" && o.value == pos + "");
                if (ct == null) throw new XExcep("T广告位不存在");
            }

            if (ent == null) ent = new x_ad() { ctime = DateTime.Now };

            ent.name = name;
            ent.pos = pos;
            ent.url = url;
            ent.pic = pic;
            ent.sort = sort;
            ent.remark = desc;

            if (ent.ad_id == 0)
            {
                DB.x_ad.InsertOnSubmit(ent);
                SubmitDBChanges();
            }

            SubmitDBChanges();

            return new XResp();
        }
    }
}
