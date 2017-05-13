using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.sder
{
    public class list : _sd
    {
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

            dict.Add("ods", q.Where(o => o.status == 4).ToList());
            dict.Add("ods1", q.Where(o => o.status == 5).ToList());

        }

    }
}
