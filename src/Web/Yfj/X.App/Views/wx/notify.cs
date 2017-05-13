using System;
using System.IO;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Plugin;
using X.Core.Utility;

namespace X.App.Views.wx
{
    public class notify : _wx
    {
        [ParmsAttr(name = "订单号", req = true)]
        public int id { get; set; }
        public int tp { get; set; }
        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }
        protected override string GetParmNames
        {
            get
            {
                return "tp-id";
            }
        }
        public override byte[] GetResponse()
        {
            GetPageParms();
            cfg = Config.LoadConfig();
            var ntxml = string.Empty;
            using (var sr = new StreamReader(Context.Request.InputStream)) ntxml = sr.ReadToEnd();

            if (string.IsNullOrEmpty(ntxml)) return null;
            ntxml = ntxml.Replace("xml", "Ntxml");
            var nt = Serialize.FormXml<Wx.Pay.Ntxml>(ntxml);

            if (!Wx.Pay.ValidNotify(nt, cfg.wx_mch_id, cfg.wx_appid, cfg.wx_paykey))
            {
                Loger.Info("回调验证失败，地址：" + Context.Request.RawUrl + "，参数：" + ntxml);
                return null;
            }

            switch (tp)
            {
                case 1:
                    ValidOrder(nt.transaction_id);
                    break;
                case 2:
                    ValidCharge();
                    break;
            }

            SubmitDBChanges();

            var okxml = @"<xml>
                            <return_code><![CDATA[SUCCESS]]></return_code>
                            <return_msg><![CDATA[OK]]></return_msg>
                        </xml>";

            return Encoding.UTF8.GetBytes(okxml);
        }

        private void ValidCharge()
        {
            var chg = DB.x_charge.SingleOrDefault(o => o.charge_id == id);
            if (chg == null) Loger.Info("充值记录不存在，订单号：" + id);
            else if (!string.IsNullOrEmpty(chg.result)) return;
            else
            {
                if (cfg.chg_audit == 2)
                {
                    chg.x_user.balance += chg.amount;
                    chg.audit_status = 2;
                    chg.audit_time = DateTime.Now;
                }
                chg.result = "成功";
                chg.audit_status = 1;
            }
        }

        private void ValidOrder(string wx_no)
        {
            var order = DB.x_order.SingleOrDefault(o => o.order_id == id);
            if (order == null) Loger.Info("订单不存在，订单ID：" + id);
            if (order.status != 1 || order.pay_way != 1) Loger.Info("订单确认失败，订单号：" + id);
            else
            {
                order.wx_no = wx_no;
                order.pay_time = DateTime.Now;
                order.pay_amount = order.yf_amount;
                order.status = 2;
            }
        }
    }
}
