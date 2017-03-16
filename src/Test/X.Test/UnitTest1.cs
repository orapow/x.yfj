using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using X.Data;
using System.Collections.Generic;
using X.Core.Utility;
using X.Core.Plugin;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        protected readonly static DataClassesDataContext DB = new DataClassesDataContext();
        [TestMethod]
        public void TestMethod1()
        {
            //X.Core.Plugin.Cache.Store("zk", "周魁");
            //Console.WriteLine(X.Core.Plugin.Cache.Get<string>("zk"));
            //var d = new Dict()
            //{
            //    Code = "s.ss",
            //    Name = "第一个",
            //    Value = "1"
            //};
            //DB.Dict.InsertOnSubmit(d);
            //DB.SubmitChanges();
            //Console.WriteLine(d.DictID);

            //
            //var q = from c in DB.Dict
            //        where c.Code == "s.ss"
            //        select c;

            //var list = q.ToList();

            //Console.WriteLine(list[0].Name);

            //var json = "{\"openid\":\"OPENID\",\"nickname\": \"NICKNAME\",\"sex\":\"1\",   \"province\":\"PROVINCE\"   \"city\":\"CITY\",   \"country\":\"COUNTRY\",    \"headimgurl\":    \"\", 	\"privilege\":[	\"PRIVILEGE1\",\"PRIVILEGE2\"    ],\"unionid\": \"o6_bmasdasdsad6_2sgVt7hMZOPfL\"}";
            //var dict = X.Core.Utility.Serialize.JsonToDict(json);
            //Console.WriteLine(dict["openid"]);
            //1441808097508

            //var ps = new Dictionary<string, string>();
            //ps.Add("appid", "wx0e97a407874ebf4e");
            //ps.Add("mch_id", "1263010601");
            //ps.Add("nonce_str", Tools.GetRandRom(24, 3));
            //ps.Add("body", "测试商口");
            //ps.Add("out_trade_no", "123456789");
            //ps.Add("total_fee", "0.1");
            //ps.Add("spbill_create_ip", "9.9.9.9");
            //ps.Add("notify_url", "http://baidu.com/notify_order-{out_trade_no}.html");
            //ps.Add("trade_type", "JSAPI");

            //Console.WriteLine(WxHelper.MdOrder(ps));

            //var str = Secret.MD5("appid=wx0e97a407874ebf4e&body=测试商口&device_info=WEB&mch_id=1263010601&nonce_str=Pl5KiKrPsVeLcL024V4r9OHo&notify_url=http://dreamzk.tunnel.mobi/notify_order-123456789.html&openid=o-cfysn_pATgx57PVWrnijmKYZtY&out_trade_no=123456789&spbill_create_ip=127.0.0.1&total_fee=1&trade_type=JSAPI&key=NwK9hYS03XQ1WvFk180XFD52pZC6IAG0");
            //Console.WriteLine(str);

            //            var s = @"<x1>
            //   <return_code><![CDATA[SUCCESS]]></return_code>
            //   <return_msg><![CDATA[OK]]></return_msg>
            //</x1>";

            //            var result = Serialize.FormXml<x1>(s);
            //            Console.WriteLine(result);

            //date(1);
            //date(2);
            //date(3);
            //date(4);

            //X.Core.Cache
            //Wx_Mp.User.Group.Create("");
        }

        [TestMethod]
        public void T1()
        {
            //Loger.Error("sfwe");

            //xcmsEntities xm = new xcmsEntities();
            //var q=from t in xm.x_article
            //      select t;
            //var d = q

            //DB.x_lang.InsertOnSubmit(new x_lang()
            //{
            //    code = "zh-cn",
            //    match = "zh,zh-cn",
            //    name = "中文"
            //});
            //DB.SubmitChanges();

            Console.WriteLine(Secret.MD5("123456"));
        }

        [TestMethod]
        public void T2()
        {
            var rspnews = new Mp.Msg.MsgNews()
            {
                FromUserName = "f",
                CreateTime = "t",
                ToUserName = "to"
            };
            rspnews.AddArti("a", "b", "c", "d");
            rspnews.AddArti("a", "b", "c", "d");
            rspnews.AddArti("a", "b", "c", "d");
            rspnews.AddArti("a", "b", "c", "d");
            rspnews.AddArti("a", "b", "c", "d");
            rspnews.AddArti("a", "b", "c", "d");
            Console.WriteLine(Serialize.ToXml(rspnews));
            //Trace.WriteLine("asdfa");
//            var xml = @"<xmlbase><ToUserName><![CDATA[gh_93b5924dc6f4]]></ToUserName>
//<FromUserName><![CDATA[oB5hSuOCsjxQTBpSgtfzZ0reVLW8]]></FromUserName>
//<CreateTime>1451703643</CreateTime>
//<MsgType><![CDATA[text]]></MsgType>
//<Content><![CDATA[又]]></Content>
//<MsgId>6235019670570883498</MsgId>
//</xmlbase>";
//            var ms = Serialize.FormXml<Wx_Mp.Msg.MsgBase>(xml);
//            Console.WriteLine(ms.FromUserName);
        }

        [TestMethod]
        public void MenuGet()
        {
            
        }

        void date(int type)
        {
            Console.WriteLine("Type:" + type);
            DateTime dt_s, dt_e;
            var dt = dt_s = dt_e = DateTime.Now;
            switch (type)
            {
                case 1:
                    dt_s = dt.AddDays(1 - int.Parse(dt.DayOfWeek.ToString("d")));
                    dt_e = dt;
                    break;
                case 2:
                    var w = int.Parse(dt.DayOfWeek.ToString("d"));
                    dt_s = dt.AddDays(1 - w - 7);
                    dt_e = dt.AddDays(-w);
                    break;
                case 3:
                    var d = dt.Day;
                    dt_s = dt.AddDays(1 - dt.Day);
                    dt_e = dt;
                    break;
                case 4:
                    dt_s = dt.AddMonths(-1).AddDays(1 - dt.Day);
                    dt_e = dt.AddDays(1 - dt.Day - 1);
                    break;
            }
            Console.WriteLine(dt_s.ToString("yyyy-MM-dd"));
            Console.WriteLine(dt_e.ToString("yyyy-MM-dd"));
        }

        public class xml
        {
            public string return_code { get; set; }
            public string return_msg { get; set; }
        }

        public class x1 : xml
        {
            public string appid { get; set; }
        }

        //[TestMethod]
        //public void Test()
        //{
        //    //DBHelper.Context.Manager.InsertOnSubmit(new Manager()
        //    //{
        //    //    Name = "周魁",
        //    //    Phone = "180",
        //    //    Pwd = "123",
        //    //    Type = 1,
        //    //    Uid = "zk"
        //    //});
        //    //DBHelper.Context.SubmitChanges();

        //    var cafe = new Cafe()
        //    {
        //        Name = "周魁"
        //    };
        //    cafe.Prints.Add(new Prints()
        //    {
        //        IP = "asdfa1",
        //        IsDefault = true,
        //        Name = "总单",
        //        Num = "asdfa",
        //        Remark = "说明",
        //        Temple = "Temple"
        //    });
        //    cafe.Prints.Add(new Prints()
        //    {
        //        IP = "asdfa12",
        //        IsDefault = true,
        //        Name = "凉菜",
        //        Num = "asdfa",
        //        Remark = "说明",
        //        Temple = "Temple"
        //    });
        //    DBHelper.Context.Cafe.InsertOnSubmit(cafe);
        //    DBHelper.Context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
        //}

        //[TestMethod]
        //public void List()
        //{
        //    //var q =
        //    //    from c in DBHelper.Context.Cafe
        //    //    from b in DBHelper.Context.Prints
        //    //    where b.CafeID == c.CafeID
        //    //    select new
        //    //    {
        //    //        c.Name,
        //    //        b.Num,
        //    //        b.IP
        //    //    };
        //    //var d = q.ToList();
        //    //foreach (var p in d)
        //    //{
        //    //    Console.WriteLine(p.Name + "," + p.Num + "," + p.IP);
        //    //}

        //    //var list = DBHelper.Context.Prints.Skip(2).Take(2).OrderBy(o => o.PrintsID).ToList();

        //    var q = (
        //        from e in DBHelper.Context.Prints
        //        orderby e.PrintsID ascending
        //        select e
        //        ).Skip(2).Take(2).ToList();

        //    foreach (var p in q)
        //    {
        //        Console.WriteLine(p.PrintsID + "," + p.Num);
        //    }

        //    //var g = DBHelper.Context.Cafe.Single(o => o.CafeID == 1);
        //    //DBHelper.Context.Prints.DeleteAllOnSubmit(g.Prints);
        //    //DBHelper.Context.Cafe.DeleteOnSubmit(g);
        //    //DBHelper.Context.SubmitChanges();
        //}

        [TestMethod]
        public void LogTest()
        {
            //X.Core.Plugin.Loger.Init();
            //X.Core.Plugin.Loger.Debug(this.GetType(), new Exception("Debug"));
            //X.Core.Plugin.Loger.Error(this.GetType(), new Exception("Error"));
            //X.Core.Plugin.Loger.Info(this.GetType(), new Exception("Info"));
            //X.Core.Plugin.Loger.Warn(this.GetType(), new Exception("Warn"));
            //X.Core.Plugin.Loger.Fatal(this.GetType(), new Exception("Fatal"));
        }
        [TestMethod]
        public void Test()
        {
            Console.WriteLine(DateTime.Parse("2015-10-12") < DateTime.Now.Date);
        }
        [TestMethod]
        public void bdsmstest()
        {
            //var ps = new Dictionary<string, object>();
            //ps.Add("")
            //Tools.PostHttpData("/v1/message");
        }
    }
}
