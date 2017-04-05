using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.wx.arti
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
        protected override void InitView()
        {
            base.InitView();
            var at = DB.x_article.FirstOrDefault(o => o.article_id == id);
            if (at != null)
            {
                dict.Add("title", at.title);
                dict.Add("cot", Context.Server.HtmlDecode(at.content));
            }
        }
    }
}
