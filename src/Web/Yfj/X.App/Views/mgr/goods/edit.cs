using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views.mgr.goods
{
    public class edit : xmg
    {
        public int id { get; set; }
        public int cp { get; set; }

        protected override string GetParmNames
        {
            //传参数
            get
            {
                return "id-cp";
            }
        }

        //List<x_goods_field> fs = null;

        protected override void InitDict()
        {
            base.InitDict();
            if (id > 0)
            {
                var ent = DB.x_goods.SingleOrDefault(o => o.goods_id == id);
                if (ent == null) throw new XExcep("0x0005");

                dict.Add("item", ent);
                dict.Add("red", ent.refunded == true ? 1 : 2);
                dict.Add("rnd", ent.sended == true ? 1 : 2);
                dict.Add("cate", ent.cate_id + "|" + GetDictName("goods.cate", ent.cate_id));

                //fs = DB.x_goods_field.Where(o => DB.x_goods_val.Where(v => v.goods_id == ent.goods_id).Select(f => f.goods_field_id).Contains(o.goods_field_id)).ToList();
                //dict.Add("fs", fs);

                //var st = DB.x_goods_stock.Where(o => o.goods_id == id).Select(o => new
                //{
                //    o.stock,
                //    o.price,
                //    name = o.names,
                //    o.ids
                //}).ToList();
                //dict.Add("stock", Serialize.ToJson(st));

            }
            else
            {
                dict.Add("red", 1);
                dict.Add("rnd", 1);
            }
        }

        //public List<x_goods_val> getvals(long fid)
        //{
        //    return DB.x_goods_val.Where(o => o.goods_id == id && o.goods_field_id == fid).ToList();
        //}

    }
}
