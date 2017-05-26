using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;
using X.Data;


namespace X.App.Apis.mgr.order
{
    public class Statistics
    {
        private static DateTime limitFlag = new DateTime(1970, 1, 1);
        public static int[] getOrderStati(DateTime startTime, int size, DataClassesDataContext DB, long cityid)
        {//应该可以改成table直接处理
            //startTime-开始查询的时间,此处需要已经处理好为当天时间（0:00）
            //size-实际可返回的数最大为24
            if (startTime < limitFlag) throw new XExcep("0x0030");
            int[] result = new int[size];
            int originCount = DB.x_order.Where(o => o.ctime < startTime).Count();

            var dayList = from q in DB.x_order where q.city == cityid && q.ctime > startTime select q;//取出当天订单

            var groupList = dayList.GroupBy(o => o.ctime.Value.Hour)
                            .Select(g => new
                            {
                                hour = g.Key,
                                count = dayList.Where(o => o.ctime.Value.Hour == g.Key).Count()
                            }).ToArray();

            foreach (var temp in groupList)
            {
                result[temp.hour] = temp.count;
            }//处理为返回值
            return result;
        }

        public static int[] getMemberStati(DateTime startTime, int size, DataClassesDataContext DB, long cityid)
        {//应该可以改成table直接处理
            //startTime-开始查询的时间,此处需要已经处理好为当天时间（0:00）
            //size-实际可返回的数最大为24
            if (startTime < limitFlag)
                throw new XExcep("0x0030");
            int[] result = new int[size];
            int originCount = DB.x_user.Where(o => o.ctime < startTime).Count();

            var dayList = from q in DB.x_user where q.city == cityid && q.ctime > startTime select q;//取出当天订单

            var groupList = dayList.GroupBy(o => o.ctime.Value.Hour)
                            .Select(g => new
                            {
                                hour = g.Key,
                                count = dayList.Where(o => o.ctime.Value.Hour == g.Key).Count()
                            }).ToArray();

            foreach (var temp in groupList)
            {
                result[temp.hour] = temp.count;
            }//处理为返回值
            return result;
        }


    }
}
