using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.city
{
    public class list : xmg
    {
        public string up { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        string code = "sys.city";

        protected override XResp Execute()
        {
            var r = new Resp_List();

            var tree = new XTree();
            tree.LoadList += tree_LoadList;
            tree.InitTree("", 5);

            var data = tree.OutTree();
            if (!string.IsNullOrEmpty(key)) r.items = data.Where(o => o.name.Contains(key)).ToList();
            else r.items = data;

            return r;
        }

        List<TreeNode> tree_LoadList(object id)
        {
            if (code == "sys.city" && id + "" == "0") id = up;
            var list = GetDictList(code, id + "");
            if (list == null) return null;
            return list.Select(m => new item()
            {
                name = m.name,
                id = m.upval == "0" ? m.value : m.upval + "-" + m.value,
                cid = m.dict_id,
                img = m.img,
                code = m.f3,
                jp = m.jp
            }).ToList<TreeNode>();
        }

        public class item : TreeNode
        {
            public long cid { get; set; }
            public string img { get; set; }
            public string code { get; set; }
            public string jp { get; set; }
            public item() : base("") { }
        }

    }
}
