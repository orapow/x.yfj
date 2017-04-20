using System.Linq;
using X.Web;

namespace X.App.Views.mgr.refund
{
    public class edit : xmg
    {
        
        public int id { get; set; }

        public decimal ramount { get; set; }
        public string remark { get; set; }
        protected override string GetParmNames  
        {
            get
            {
                return "id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var ent = DB.x_refund.FirstOrDefault(o => o.refund_id == id);
            if (ent == null) throw new XExcep("0x0005");
            //if (ramount > ent.x_order.pay_amount)
            //    throw new XExcep("T退款金额不能大于用户支付金额");
            //ent.ramount = ramount;
            //ent.remark = remark;
            //SubmitDBChanges();
            dict.Add("itemRefund", ent);
        }
    }
}
