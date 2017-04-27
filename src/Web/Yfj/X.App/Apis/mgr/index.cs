using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Apis;
using X.Web.Com;
using X.App.Apis;
using X.App.Apis.mgr.order;
using X.Core.Utility;

namespace X.App.Apis.mgr {
    public class index : Api {
        //public String require { get; set; }

        protected override XResp Execute() {
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            int[][] result=new int[3][];
            result[0]=Statistics.getOrderStati(startTime, 24, DB);
            result[1] = Statistics.getMemberStati(startTime, 24, DB);
            result[2]=Statistics.getMemberStati(startTime, 24, DB);
            
            return new XResp() { 
                msg = Serialize.ToJson(result),
                
            };
        }
    }
}
