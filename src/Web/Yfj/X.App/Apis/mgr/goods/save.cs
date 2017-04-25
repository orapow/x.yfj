using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.goods
{
    public class save : xmg
    {
        public int id { get; set; }
        /// <summary>
        /// 是否是复制
        /// </summary>
        public int iscp { get; set; }
        public string cate { get; set; }//分类
        public int mch_id { get; set; }//商户
        public string name { get; set; }//名称
        public string no { get; set; }//编号
        public int brand { get; set; }
        public string alias { get; set; }//别名
        public string remark { get; set; }//简介
        public string tags { get; set; }//标签
        public string cover { get; set; }//封面
        public string imgs { get; set; }//图集
        public int sort { get; set; }//排序
        public string desc { get; set; }//描述
        public int stock { get; set; }//库存
        public int limit { get; set; }//限购
        public string unit { get; set; }
        public decimal op { get; set; }//原价
        public decimal np { get; set; }//现价
        public int re_it { get; set; }//返积分
        public int red { get; set; }//支持退款
        public int rnd { get; set; }//是否配送

        public int hot { get; set;}//是否热销
        protected override int powercode {
            get {
                return 1;
            }
        }
        protected override XResp Execute()
        {
            x_goods ent = null;
            if (id > 0)
            {
                ent = DB.x_goods.SingleOrDefault(o => o.goods_id == id);
                if (ent == null) throw new XExcep("0x0005");
            }
            if (ent == null || iscp == 1) ent = new x_goods() { ctime = DateTime.Now };

            var ct = DB.x_dict.FirstOrDefault(o => o.code == "goods.cate" && o.value == cate);
            if (ct == null) throw new XExcep("T分类不存在");

            ent.cate_id = cate;
            ent.unit = ct.f3 ?? unit;
            ent.city = mg.city;
            ent.name = name;
            ent.no = no;
            ent.alias = alias;
            ent.remark = remark;
            ent.tags = tags;
            ent.brand = brand;
            ent.cover = (string.IsNullOrEmpty(cover) && !string.IsNullOrEmpty(imgs)) ? imgs.Split(',')[0] : cover;
            ent.imgs = imgs;
            ent.sort = sort;
            ent.desc = desc;
            ent.stock = stock;
            ent.limit = limit;
            ent.old_price = op;
            ent.new_price = np;
            ent.return_exp = re_it;
            ent.refunded = red == 1;
            ent.sended = rnd == 1;
            ent.hot = hot;
            if (ent.goods_id == 0) ent.status = 2;

            if (ent.goods_id == 0)
            {
                DB.x_goods.InsertOnSubmit(ent);
                SubmitDBChanges();
            }

            //if (gclass > 0 && !string.IsNullOrEmpty(ex_ids))
            //{
            //    var vals = DB.x_goods_val.Where(o => ex_ids.Split(',').ToList().Contains(o.goods_val_id + ""));
            //    foreach (var v in vals) v.goods_id = ent.goods_id;
            //}

            //if (!string.IsNullOrEmpty(ex_stock))
            //{
            //    var old_st = DB.x_goods_stock.Where(o => o.goods_id == ent.goods_id).ToList();
            //    DB.x_goods_stock.DeleteAllOnSubmit(old_st);
            //    SubmitDBChanges();
            //    var new_st = new List<x_goods_stock>();
            //    foreach (var str in ex_stock.Split(','))
            //    {
            //        var st = str.Split('|');
            //        var d = new x_goods_stock()
            //        {
            //            goods_id = ent.goods_id,
            //            ids = st[0],
            //            names = st[1],
            //            stock = int.Parse(st[2]),
            //            price = decimal.Parse(st[3])
            //        };
            //        new_st.Add(d);
            //    }
            //    DB.x_goods_stock.InsertAllOnSubmit(new_st);
            //}

            SubmitDBChanges();

            return new XResp();
        }
    }
}
