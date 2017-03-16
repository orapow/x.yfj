using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;

namespace X.App.Views
{
    public class list : xview
    {
        //[ParmsAttr(name = "品类ID", min = 1)]
        public string cate { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "cate";
            }
        }

        protected override void InitDict()
        {
            base.InitDict();
            //列表
            var glist = DB.x_goods.Where(o => o.cate_id == cate).ToList();
            if (glist == null) throw new XExcep("T商品类别或已经删除");

            //GetDictName()

            //类名
            //var gcate = DB.x_dict.FirstOrDefault(o => o.id == id);

            dict.Add("glist", glist);
        }
    }
}
