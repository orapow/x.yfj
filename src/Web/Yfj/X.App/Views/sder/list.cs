using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.sder
{
    public class list : _sd
    {
        public int st { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "st";
            }
        }

        protected override void InitDict()
        {
            base.InitDict();

            var q = from o in DB.x_order
                    where o.send_man.EndsWith(" " + sd.value)
                    select new
                    {
                        o.no,
                        id = o.order_id,
                        o.rec_addr,
                        o.rec_man,
                        o.status,
                        o.rec_tel
                    };

            if (st == 1) q = q.Where(o => o.status == 5);
            else q = q.Where(o => o.status == 4);

            dict.Add("ods", q.ToList());

        }
    }
}
