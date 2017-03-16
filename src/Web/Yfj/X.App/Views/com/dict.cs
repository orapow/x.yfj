using System;
using System.Collections.Generic;
using System.Linq;
using X.Data;
using X.Web.Views;

namespace X.App.Views.com
{
    public class dict : xview
    {
        [ParmsAttr(name = "代号", req = true)]
        public string code { get; set; }
        public string upv { get; set; }
        public int bylet { get; set; }

        protected override void InitDict()
        {
            base.InitDict();
            if (dict.GetInt("bylet") == 1)
            {
                dict.Add("list", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList());
            }
            else
            {
                var list = GetDictList(code, upv);
                list.Insert(0, new x_dict() { name = "无", value = "0" });
                dict.Add("dict", list);
            }
        }

        protected override string GetParmNames
        {
            get
            {
                ///代号-上级值-按字母排
                return "code-upv-bylet";
            }
        }

        public List<x_dict> GetByLetter(char l)
        {
            var list = GetDictList(code, upv);
            return list.FindAll(d =>
            {
                return d.jp.ToUpper()[0] == l;
            });
        }
    }
}
