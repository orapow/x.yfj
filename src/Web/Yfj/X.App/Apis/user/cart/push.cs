using System;
using System.Linq;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.cart
{
    public class push : _u
    {
        [ParmsAttr(name = "商品ID", min = 1)]
        public long gid { get; set; }
        [ParmsAttr(name = "数量", min = 1)]
        public int count { get; set; }
        /// <summary>
        /// 属性ID
        /// </summary>
        public string attrs { get; set; }

        protected override XResp Execute()
        {
            var gd = DB.x_goods.FirstOrDefault(o => o.goods_id == gid);
            if (gd == null || gd.status != 2) throw new XExcep("T商品不存在或已经下架！");

            //var ct = DB.x_cart.FirstOrDefault(o => o.user_id == cu.user_id && o.goods_id == gid && o.attr_id == attrs);
            //if (ct == null)
            //{
            //    ct = new x_cart();
            //    ct.count = count;
            //    ct.cover = gd.cover;
            //    ct.attr_id = attrs;
            //    ct.ctime = DateTime.Now;
            //    ct.desc = gd.remark;
            //    ct.goods_id = gid;
            //    ct.goods_name = gd.name;
            //    ct.price = gd.new_price;
            //    ct.total_price = gd.new_price * count;
            //    ct.user_id = cu.user_id;

            //    if (!string.IsNullOrEmpty(attrs))
            //    {
            //        var vals = DB.x_goods_val.Where(o => attrs.Split(',').Contains(o.goods_val_id + "")).ToList();
            //        var attr = "";
            //        foreach (var v in vals) attr += v.x_goods_field.name + "：" + v.val + " ";
            //        ct.attr = attr;
            //    }

            //    if (gd.mch_id > 0)
            //    {
            //        var mch = DB.x_mch.FirstOrDefault(o => o.mch_id == gd.mch_id);
            //        if (mch != null)
            //        {
            //            ct.mch_id = gd.mch_id;
            //            ct.mch_name = mch.name;
            //        }
            //    }

            //    DB.x_cart.InsertOnSubmit(ct);
            //}
            //else
            //{
            //    ct.count += count;
            //    ct.total_price = ct.count * ct.price;
            //}

            SubmitDBChanges();

            return new XResp();

        }

    }
}
