using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.user
{
    /// <summary>
    /// 删除用户
    /// </summary>
    public class del : xmg
    {
        public int id { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }
        

        protected override XResp Execute()
        {
            var u = DB.x_user.FirstOrDefault(o => o.user_id == id);
            if (u == null) throw new XExcep("0x0039");

            var ode = DB.x_order_detail.Where(o => u.x_order.Select(c => c.order_id + "").Contains(o.order_id + ""));
            //var ods = DB.x_order_send.Where(o => u.x_order.Select(c => c.order_id + "").Contains(o.order_id + ""));

            //DB.x_order_send.DeleteAllOnSubmit(ods.ToList());//发货记录
            DB.x_order_detail.DeleteAllOnSubmit(ode.ToList());//订单详情
            DB.x_order.DeleteAllOnSubmit(u.x_order.ToList());//订单
            DB.x_address.DeleteAllOnSubmit(u.x_address);//收货地址
            DB.x_cart.DeleteAllOnSubmit(u.x_cart);//购物车
            DB.x_integral_log.DeleteAllOnSubmit(u.x_integral_log);//积分记录
            DB.x_charge.DeleteAllOnSubmit(u.x_charge);//充值记录
            DB.x_user.DeleteOnSubmit(u);//删除用户

            SubmitDBChanges();

            return new XResp();
        }

    }
}
