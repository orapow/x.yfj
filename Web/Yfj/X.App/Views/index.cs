using System;
using System.Collections.Generic;
using System.Linq;
using X.Data;

namespace X.App.Views
{
    public class index : xview
    {
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("ss", DateTime.Now);
            dict.Add("f1", getgoods("0"));
            dict.Add("f2", getgoods("0"));
            dict.Add("f3", getgoods("0"));
            dict.Add("f4", getgoods("0"));
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="cate">品类</param>
        /// <returns></returns>
        public List<x_goods> getgoods(string cate)
        {
            return DB.x_goods.Where(o => o.cate_id == cate).ToList();
        }

        //public List<x_dict> getcate() {
            //return DB.x_dict.Where().ToList();
        //}
    }
}
