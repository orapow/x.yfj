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
        public string no { get; set; }
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
                return "no";
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

            var no = dict.GetString("no");
            if (string.IsNullOrEmpty(no)) return null;
            if (nt.out_trade_no != no)
            {
                Loger.Info("订单号不正确，地址：" + Context.Request.RawUrl + "，提交单号：" + nt.out_trade_no);
                return null;
            }

            var order = DB.x_order.SingleOrDefault(o => o.no == no);

            var okxml = @"<xml>
                            <return_code><![CDATA[SUCCESS]]></return_code>
                            <return_msg><![CDATA[OK]]></return_msg>
                        </xml>";

            if (order == null)
            {
                Loger.Info("订单不存在，订单号：" + no);
                return Encoding.UTF8.GetBytes(okxml);
            }

            if (order.status != 1 || order.pay_way != 1)
            {
                Loger.Info("订单确认失败，订单号：" + no);
                return Encoding.UTF8.GetBytes(okxml);
            }
            else
            {
                order.wx_no = nt.transaction_id;
                order.pay_time = DateTime.Now;
                order.pay_amount = order.yf_amount;
                order.status = 2;
            }

            SubmitDBChanges();

            return Encoding.UTF8.GetBytes(okxml);
        }
    }
}
