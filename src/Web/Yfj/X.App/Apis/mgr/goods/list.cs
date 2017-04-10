using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.goods {
    public class list : xmg {
        /// <summary>
        /// 状态：
        /// 1、草稿
        /// 2、已上架
        /// 3、已下架
        /// 4、已删除
        /// </summary>
        [ParmsAttr(name = "状态")]
        public int st { get; set; }
        public string cate { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        protected override Web.Com.XResp Execute() {
            var r = new Resp_List();
            r.page = page;

            var q = from et in DB.x_goods
                    orderby et.goods_id descending
                    select new {
                        id = et.goods_id,
                        cate = GetDictName("goods.cate", et.cate_id),
                        cid = et.cate_id,
                        et.name,
                        et.remark,
                        et.cover,
                        et.sort,
                        et.stock,
                        et.old_price,
                        new_price = ((DB.x_sale.SingleOrDefault(o => o.goods_id == et.goods_id)) == null ? et.new_price : (DB.x_sale.SingleOrDefault(o => o.goods_id == et.goods_id).price)),
                        isSale = (DB.x_sale.SingleOrDefault(o => o.goods_id == et.goods_id)) == null ? "" : "抢",
                        endTime = (DB.x_sale.SingleOrDefault(o => o.goods_id == et.goods_id)) == null ? "" : GetDateString(DB.x_sale.SingleOrDefault(o => o.goods_id == et.goods_id).etime, "结束于yyyy-MM-dd HH:mm"),
                        //这还有没有别的办法
                        et.status,
                        time = GetDateString(et.ctime, "yyyy-MM-dd HH:mm"),
                        tk = et.refunded == true ? 1 : 0,
                        ps = et.sended == true ? 1 : 0
                    };


            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key) || o.remark.Contains(key));
            if (!string.IsNullOrEmpty(cate)) {
                var cids = DB.x_dict.Where(o => o.code == "goods.cate" && o.upval.Contains(cate) || o.value == cate).Select(o => o.value);
                q = q.Where(o => cids.Contains(o.cid));
            }

            if (st > 0) q = q.Where(o => o.status == st);
            else q = q.Where(o => o.status != 4);

            r.items = q.Skip((page - 1) * limit).Take(limit);
            r.count = q.Count();
            return r;
        }

    }
}
