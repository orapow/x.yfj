using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.sale {
    public class status : xmg {
        [ParmsAttr(min = 1)]
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ParmsAttr(name = "排序值")]
        public int val { get; set; }
        /// <summary>
        /// 1、退款
        /// 2、配送
        /// 3、上架
        /// 4、下架
        /// 5、还原
        /// </summary>
        public int type { get; set; }

        protected override XResp Execute() {
            var ent = DB.x_goods.FirstOrDefault(o => o.goods_id == id);
            if (ent == null) throw new XExcep("T商品不存在");

            switch (type) {
                case 1:
                    ent.sended = !ent.sended;
                    break;
                case 2:
                    ent.refunded = !ent.refunded;
                    break;
                case 3:
                    ent.status = 2;
                    break;
                case 4:
                    ent.status = 3;
                    break;
                case 5:
                    ent.status = 1;
                    break;
                case 6:
                    ent.sort = val;
                    break;
            }

            SubmitDBChanges();

            return new XResp();
        }
    }
}
