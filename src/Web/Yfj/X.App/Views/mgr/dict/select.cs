﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;

namespace X.App.Views.mgr.dict {
    public class select : xmg {
        [ParmsAttr(name = "代号", req = true)]
        public string code { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override string GetParmNames {
            get {
                return "code";
            }
        }
        private List<item> data = new List<item>();

        protected override void InitDict() {
            var tree = new XTree();
            tree.LoadList += tree_LoadList;
            tree.InitTree("");
            var list = tree.OutTree();
            list.Insert(0, new item() { id = "0", name = "无" });
            dict.Add("dict", list);
        }

        List<TreeNode> tree_LoadList(object id) {
            var list = GetDictList(code, id + "");
            return list.Select(m => new item() {
                name = m.name,
                id = m.upval == "0" ? m.value : m.upval + "-" + m.value,
                cid = m.dict_id,
                value = m.value,
                img = m.img,
                jp = m.jp
            }).ToList<TreeNode>();
        }

        public class item : TreeNode {
            public long cid { get; set; }
            public string img { get; set; }
            public string value { get; set; }
            public string jp { get; set; }
            public item() : base("") { }
        }

        public override string GetTplFile() {
            return "com/dict";
        }
    }
}
