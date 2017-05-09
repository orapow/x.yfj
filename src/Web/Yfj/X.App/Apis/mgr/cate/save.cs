﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.cate
{
    public class save : xmg
    {
        public int id { get; set; }
        [ParmsAttr(req = true, name = "名称")]
        public string name { get; set; }
        public string upv { get; set; }
        public string img { get; set; }
        public string unit { get; set; }
        public int sort { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            x_dict ent = null;
            var code = "goods.cate";
            if (id > 0) ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) ent = new x_dict() { code = code };

            ent.name = name;
            ent.img = img;
            ent.f3 = unit;
            ent.sort = sort;

            var upval = ent.upval;

            if (!string.IsNullOrEmpty(upv) && upv != "0")
            {
                var up = DB.x_dict.FirstOrDefault(o => o.code == code && o.value == upv);
                if (id > 0 && up.value.StartsWith(ent.upval)) throw new XExcep("T不能将 " + name + " 调整到其下级里面");
                if (up.upval == "0") ent.upval = up.value;
                else ent.upval = up.upval + "-" + up.value;
            }
            else
            {
                ent.upval = "0";
            }

            if (id > 0)
            {
                if (upval == "0")
                {
                    var childs = DB.x_dict.Where(o => o.upval == ent.value);
                    foreach (var e in childs.ToList())
                    {
                        if (e.upval != ent.value) e.upval = e.upval.Replace("-" + ent.value, "");
                    }
                }
                else
                {
                    var childs = DB.x_dict.Where(o => o.upval.StartsWith(upval + "-"));
                    foreach (var e in childs.ToList()) e.upval = e.upval.Replace("-" + ent.value, "");
                }
            }
            else
            {
                DB.x_dict.InsertOnSubmit(ent);
                SubmitDBChanges();
            }

            ent.value = ent.dict_id + "";

            SubmitDBChanges();

            CacheHelper.Remove("dict." + ent.code);

            return new XResp();
        }
    }
}
