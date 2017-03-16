using System;
using System.Collections.Generic;
using System.Linq;
using X.Data;
using X.Web;

namespace X.App.Views
{
    public class detail : xview
    {
        [ParmsAttr(name = "商品ID", min = 1)]
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }

        List<x_goods_field> fs = null;

        protected override void InitDict()
        {
            base.InitDict();

            var gd = DB.x_goods.FirstOrDefault(o => o.goods_id == id);
            if (gd == null || gd.status == 4) throw new XExcep("T商品不存在或已经删除");

            dict.Add("g", gd);

            fs = DB.x_goods_field.Where(o => DB.x_goods_val.Where(v => v.goods_id == gd.goods_id).Select(f => f.goods_field_id).Contains(o.goods_field_id)).ToList();

            dict.Add("fs", fs);

        }

        public List<x_goods_val> getvals(long fid)
        {
            return DB.x_goods_val.Where(o => o.goods_id == id && o.goods_field_id == fid).ToList();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="t">
        ///// 1、关联库存和价格的字段
        ///// 2、未关联库存和价格的字段
        ///// </param>
        ///// <returns></returns>
        //public List<x_goods_val> get_exattr(int t)
        //{
        //    var vals = DB.x_goods_val.Where(o => o.goods_id == id);
        //    if (t == 1)
        //    {
        //        vals = vals.Where(o => o.x_goods_field.ch_price == 1 || o.x_goods_field.ch_stock == 1);
        //    }
        //    else
        //    {
        //        vals = vals.Where(o => o.x_goods_field.ch_price != 1 && o.x_goods_field.ch_stock != 1);
        //    }
        //    return vals.ToList();
        //}

    }
}
