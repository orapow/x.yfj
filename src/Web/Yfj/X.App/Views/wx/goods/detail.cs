using System;
using System.Linq;
using X.Web;

namespace X.App.Views.wx.goods
{
    public class detail : _wx
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var gc = 0;
            if (cu != null)
            {
                var g = cu.x_cart.FirstOrDefault(o => o.goods_id == id);
                if (g != null) gc = g.count.Value;
                dict.Add("tc", cu.x_cart.Sum(o => o.count.Value));
            }
            else
            {
                dict.Add("tc", 0);
            }
            var gd = DB.x_goods.FirstOrDefault(o => o.goods_id == id);
            var sl = gd.x_sale.FirstOrDefault(o => o.etime > DateTime.Now);

            if (sl != null) Context.Response.Redirect("/wx/goods/sale-" + sl.sale_id + ".html");

            dict.Add("g", gd);
            dict.Add("pics", gd.imgs.Split(',').ToList());
            dict.Add("desc", Context.Server.HtmlDecode(gd.desc));
            dict.Add("gc", gc);
        }
    }
}
