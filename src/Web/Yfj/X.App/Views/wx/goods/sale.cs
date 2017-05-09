using System;
using System.Linq;
using X.Data;
using X.Web;

namespace X.App.Views.wx.goods
{
    public class sale : _wx
    {
        public int id { get; set; }
        x_sale sl = null;
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
            sl = DB.x_sale.FirstOrDefault(o => o.sale_id == id);
            if (cu != null)
            {
                var g = cu.x_cart.FirstOrDefault(o => o.goods_id == sl.goods_id);
                if (g != null) gc = g.count.Value;
                dict.Add("tc", cu.x_cart.Sum(o => o.count.Value));
            }
            else
            {
                dict.Add("tc", 0);
            }
            if (sl == null || sl.x_goods == null || sl.etime <= DateTime.Now || sl.count <= 0)
            {
                dict.Add("img", "/img/wx/uig.png");
                if (sl == null)
                {
                    dict.Add("msg", "秒杀记录不存在");
                    dict.Add("bt_txt", "返回首页");
                    dict.Add("bt_url", "/wx/index.html");
                }
                else
                {
                    dict.Add("msg", "秒杀活动已经结束");
                    dict.Add("bt_txt", "浏览此商品");
                    dict.Add("bt_url", "/wx/goods/detail-" + sl.goods_id + ".html");
                }
                dict.Add("show_foot", 0);
            }
            else
            {
                dict.Add("g", sl.x_goods);
                dict.Add("sl", sl);
                dict.Add("pics", sl.x_goods.imgs.Split(',').ToList());
                dict.Add("desc", Context.Server.HtmlDecode(sl.x_goods.desc));
                dict.Add("gc", gc);
            }
        }
        public override string GetTplFile()
        {
            if (sl == null || sl.x_goods == null || sl.etime <= DateTime.Now) return "wx/no";
            return base.GetTplFile();
        }
    }
}
