using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Xml.Linq;
using System.Xml;
using X.Core.Utility;
using System.Xml.Serialization;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var Path = "d:\\s.xls";
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$A:Y]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet(); myCommand.Fill(ds, "table1");
            var tab = ds.Tables[0];
            for (var r = 5; r < tab.Rows.Count; r++)
            {
                for (var c = 0; c < tab.Columns.Count; c++)
                {
                    Console.Write(tab.Rows[r][c] + "\t");
                };
                Console.Write("\n");
            }
            Console.WriteLine(ds.Tables[0].Rows.Count);
        }

        [TestMethod]
        public void Test()
        {
            //var json = "{\"authorization_info\": {\"authorizer_appid\": \"wxf8b4f85f3a794e77\", \"authorizer_access_token\": \"QXjUqNqfYVH0yBE1iI_7vuN_9gQbpjfK7hYwJ3P7xOa88a89-Aga5x1NMYJyB8G2yKt1KCl0nPC3W9GJzw0Zzq_dBxc8pxIGUNi_bFes0qM\", \"expires_in\": 7200, \"authorizer_refresh_token\": \"dTo-YCXPL4llX-u1W1pPpnp8Hgm4wpJtlR6iV0doKdY\",\"func_info\": [{\"funcscope_category\": {\"id\": 1}},{\"funcscope_category\": {\"id\": 2}},{\"funcscope_category\": {\"id\":3}}]}";
            //dynamic o = X.Core.Utility.Serialize.FromJson<MyClass>(json);
            //Console.WriteLine(o.authorization_info.);

            //            var xml = @"<xml><AppId><![CDATA[wx45d422d419115eb5]]></AppId>
            //<CreateTime>1457591480</CreateTime>
            //<InfoType><![CDATA[component_verify_ticket]]></InfoType>
            //<ComponentVerifyTicket><![CDATA[ticket@@@p1qfDCVFOS5GuUPBqFWoQwhIkHLvOb6UCu_KOii3ZjlJxdUrKqgRdH6-RItfMSwz6Ig9Ocv9RXgu-K6A2pCa3g]]></ComponentVerifyTicket>
            //</xml>";
            //            dynamic o = new Push(xml);
            //            Console.WriteLine(o.CreateTime);
            //var msg = new Msg<string>();
            //msg.dict.Add("ToUserName", "1");
            //msg.dict.Add("FromUserName", "2");
            //msg.dict.Add("CreateTime", "3");
            //msg.dict.Add("MsgType", "4");
            //msg.dict.Add("Content", "5");
            //msg.dict.Add("MsgId", "6");

            //Console.WriteLine(msg.ToXml());

            //Serialize.ToJson(new X.App.Apis.app.user.coop.list() { });

            //CutImage("d:\\s.jpg", 78, 39, 120, 20);

            //var data = Convert.FromBase64String("DQo8IURPQ1RZUEUgaHRtbD4NCjxodG1sPg0KPGhlYWQ+DQoJPG1ldGEgaHR0cC1lcXVpdj0iWC1VQS1Db21wYXRpYmxlIiBjb250ZW50PSJJRT1FbXVsYXRlSUU3IiAvPg0KCTxtZXRhIGh0dHAtZXF1aXY9IkNvbnRlbnQtVHlwZSIgY29udGVudD0idGV4dC9odG1sOyBjaGFyc2V0PXV0Zi04IiAvPg0KCQ0KCTxtZXRhIG5hbWU9IktleXdvcmRzIiBjb250ZW50PSLnvo7lroXnvZEgLOWumOe9kemmlumhtSzkuKrkurrmiL/mupAs5oi/5Lqn6K+B6L+H5oi3LOS4quS6uuWHuuennyzkuKrkurrlh7rllK4s5YyX5Lqs5oi/5bGL5Ye656efLOWMl+S6rOaIv+Wxi+WHuuWUriIgLz4NCgk8bWV0YSBuYW1lPSJEZXNjcmlwdGlvbiIgY29udGVudD0i576O5a6F572R5a6Y572R6aaW6aG144CB5Liq5Lq65oi/5rqQ44CB5oi/5Lqn6K+B6L+H5oi344CB5Liq5Lq65Ye656ef44CB5Liq5Lq65Ye65ZSu44CB5YyX5Lqs5oi/5bGL5Ye656ef44CB5YyX5Lqs5oi/5bGL5Ye65ZSuIOasoui/juadpeWIsOe+juWuhee9keaJvuaIv+ebtOaOpeaJvuaIv+S4nCIgLz4NCgk8bWV0YSBuYW1lPSJiYWlkdV91bmlvbl92ZXJpZnkiIGNvbnRlbnQ9ImQ5Njg1YzBhYmFjZjg3ODNkODgyMjk2YzZjYzRjNWQzIj4NCg0KCTx0aXRsZT7nvo7lroXnvZEt5a6Y572R6aaW6aG1PC90aXRsZT4NCgk8bGluayB0eXBlPSJpbWFnZS94LWljb24iIGhyZWY9Ii9mYXZpY29uLmljbyIgcmVsPSJzaG9ydGN1dCBpY29uIj4NCgk8bGluayB0eXBlPSJ0ZXh0L2NzcyIgcmVsPSJzdHlsZXNoZWV0IiBocmVmPSIvY3NzL2NvbW1vbi5jc3MiIC8+DQoJPGxpbmsgdHlwZT0idGV4dC9jc3MiIHJlbD0ic3R5bGVzaGVldCIgaHJlZj0iL2Nzcy9uZXdpbmRleC5jc3MiIC8+DQoJPGxpbmsgdHlwZT0idGV4dC9jc3MiIHJlbD0ic3R5bGVzaGVldCIgaHJlZj0iL2Nzcy9uZXdpbmRleF90b3AuY3NzIiAvPg0KCTxsaW5rIHR5cGU9InRleHQvY3NzIiByZWw9InN0eWxlc2hlZXQiIGhyZWY9Ii9jc3MvbmV3aW5kZXhfZm9vdGVyLmNzcyIgLz4NCgk8bGluayB0eXBlPSJ0ZXh0L2NzcyIgcmVsPSJzdHlsZXNoZWV0IiBocmVmPSIvY29udGVudC9sYXllci5jc3MiIC8+DQoJDQoNCgk8c2NyaXB0IHNyYz0iL1NjcmlwdHMvanF1ZXJ5LTEuNi4yLm1pbi5qcyIgdHlwZT0idGV4dC9qYXZhc2NyaXB0Ij48L3NjcmlwdD4NCgk8c2NyaXB0IHNyYz0iL2pzL2Jhbm5lcjIuanMiIHR5cGU9InRleHQvamF2YXNjcmlwdCIgY2hhcnNldD0idXRmLTgiPjwvc2NyaXB0Pg0KCTxzY3JpcHQgdHlwZT0idGV4dC9qYXZhc2NyaXB0IiBzcmM9Ii9qcy9tYW51LmpzIj48L3NjcmlwdD4NCgkNCgk8IS0tW2lmIElFIDZdPg0KCTxzY3JpcHQgdHlwZT0idGV4dC9qYXZhc2NyaXB0IiBzcmM9Ii9TY3JpcHRzL0REX2JlbGF0ZWRQTkcuanMiPjwvc2NyaXB0Pg0KCTxzY3JpcHQ+DQoJRERfYmVsYXRlZFBORy5maXgoJ2ltZywuYXRfY3VycmVudCwuYXRfaW1ncyBsaSwubGdvbl9mbCwubGdvbl9idG0sLm9uJyk7DQoJPC9zY3JpcHQ+DQoJPCFbZW5kaWZdLS0+DQoJPHNjcmlwdCB0eXBlPSJ0ZXh0L2phdmFzY3JpcHQiPg0KCQkkKGRvY3VtZW50KS5yZWFkeShmdW5jdGlvbiAoKSB7DQoJCQkkKCIuaW5wbm9uZSIpLmZvY3VzKGZ1bmN0aW9uICgpIHsNCgkJCQkkKHRoaXMpLnBhcmVudCgpLmFkZENsYXNzKCJ1c2VyZmluZCIpOw0KCQkJfSkuYmx1cihmdW5jdGlvbiAoKSB7DQoJCQkJJCh0aGlzKS5wYXJlbnQoKS5yZW1vdmVDbGFzcygidXNlcmZpbmQiKTsNCgkJCX0pOw0KDQoJCQkvKueZu+W9lee9keermeS4i+aLieiPnOWNlemAieaLqSovDQoJCQkkKCIubXpod19zZWwgZGl2IikuY2xpY2soZnVuY3Rpb24gKCkgew0KCQkJCSQoIi5temh3X3NlbCB1bCIpLnNob3coKTsNCgkJCX0pOw0KCQkJJCgiLm16aHdfc2VsIHVsIGxpIikuaG92ZXIoZnVuY3Rpb24gKCkgew0KCQkJCSQodGhpcykuYWRkQ2xhc3MoImFjdGl2ZSIpOw0KCQkJfSwgZnVuY3Rpb24gKCkgew0KCQkJCSQodGhpcykucmVtb3ZlQ2xhc3MoImFjdGl2ZSIpOw0KCQkJfSk7DQoJCQkkKCIubXpod19zZWwgdWwgbGkiKS5jbGljayhmdW5jdGlvbiAoKSB7DQoJCQkJJCgnLm16aHdfc2VsIHVsJykuaGlkZSgpOw0KCQkJCSQoJy5temh3X3NlbCBkaXYnKS5odG1sKCQodGhpcykuaHRtbCgpKTsNCgkJCX0pOw0KDQoJCX0pOw0KCTwvc2NyaXB0Pg0KPC9oZWFkPg0KPGJvZHk+DQoJPGRpdiBzdHlsZT0iZGlzcGxheTpub25lIj48aW1nIHNyYz0iL0ltYWdlcy9tZWl6aGFpLmpwZyIgYWx0PSLnvo7lroXnvZEiPjwvZGl2Pg0KCTxkaXYgY2xhc3M9Im5ld2luZGV4dG9wIj4NCgkJPGRpdiBjbGFzcz0ibmV3aW5kZXh0b3BuciI+DQoJCQk8cD48c3Ryb25nPuWMl+S6rDwvc3Ryb25nPls8YSBocmVmPSIjIj7liIfmjaLln47luII8L2E+PGEgaHJlZj0iaHR0cDovL2hhbmRhbi5tZWl6aGFpLmNuLyI+6YKv6YO4PC9hPiA8YSBocmVmPSJodHRwOi8vd3d3Lm1laXpoYWkuY24vIj7ljJfkuqw8L2E+XTwvcD4NCgkJCQkJPHNwYW4+PGEgaHJlZj0iL0hvbWUvTXpQaG9uZSI+5omL5py654mIPC9hPiZuYnNwOyZuYnNwO3wmbmJzcDsmbmJzcDsgPHN0cm9uZz7lsI/msao8L3N0cm9uZz7vvIzmgqjlt7LnmbvlvZXvvIE8YSBocmVmPSIvQWdlbnQvc2hhcmVkL2xvZ291dCI+W+mAgOWHul08L2E+PC9zcGFuPg0KCQk8L2Rpdj4NCgk8L2Rpdj4NCg0KDQoJPCEtLWVuZCBvZiBoZWFkZXItLT4NCgkNCg0KDQoNCg0KPHNjcmlwdCBzcmM9Jy9TY3JpcHRzL2pxdWVyeS5TdXBlclNsaWRlLmpzP3Y9MjAxNTA3MTcnIHR5cGU9InRleHQvamF2YXNjcmlwdCI+PC9zY3JpcHQ+DQoNCjxzY3JpcHQgc3JjPScvU2NyaXB0cy9tZDUuanM/dj0yMDE1MDcxNycgdHlwZT0idGV4dC9qYXZhc2NyaXB0Ij48L3NjcmlwdD4NCjxzY3JpcHQgc3JjPScvU2NyaXB0cy9qTG9naW4uanM/dj0yMDE1MDcxNycgdHlwZT0idGV4dC9qYXZhc2NyaXB0Ij48L3NjcmlwdD4NCjxzY3JpcHQgc3JjPSIvU2NyaXB0cy9qcXVlcnkubGF6eWxvYWQuanM/dj0yMDE1MDcxNyIgdHlwZT0idGV4dC9qYXZhc2NyaXB0Ij48L3NjcmlwdD4NCjxpbnB1dCB0eXBlPSJoaWRkZW4iIGlkPSJoaWRSZXN1bHQiIC8+DQoNCg0KPGRpdiBjbGFzcz0iaW5kZXhtYWludG9wIj4NCgk8ZGl2IGNsYXNzPSJoZWFkZXJiaiI+DQoJCTxkaXYgY2xhc3M9ImhlYWRlciI+DQoJCQk8YSBocmVmPSIvIiBjbGFzcz0ibG9nbyI+PGltZyBzcmM9Ii9pbWFnZXMvbmV3aGVhZC9sb2dvLnBuZyIgYm9yZGVyPSIwIiB3aWR0aD0iMTIwcHgiIGhlaWdodD0iODBweCIgLz48L2E+DQoJCQk8c3Bhbj48aW1nIHNyYz0iL2ltYWdlcy9uZXdoZWFkL25ld2luZGV4MDEucG5nIiB3aWR0aD0iMTUxIiBoZWlnaHQ9IjI0IiAvPjwvc3Bhbj4NCg0KCQkJPHVsIGlkPSJuZXdpbmRleG5hdiI+DQoJCQkJPGxpPjxhIGhyZWY9Ii8iIGNsYXNzPScgIGhvdmVyICAgICAnPummlumhtTwvYT48L2xpPg0KCQkJCTxsaT4NCgkJCQkJPGEgaHJlZj0iamF2YXNjcmlwdDovLyIgY2xhc3M9ImluYXYwMiAiPuS4quS6uuaIv+a6kDwvYT4NCgkJCQkJPGRpdj4NCgkJCQkJCTxkbD48L2RsPg0KCQkJCQkJPHA+PGEgaHJlZj0iL2hvdXNlL3NhbGUiPuS4quS6uuWHuuWUrjwvYT48YSBocmVmPSIvaG91c2UvcmVudCI+5Liq5Lq65Ye656efPC9hPjwvcD4NCgkJCQkJPC9kaXY+DQoJCQkJPC9saT4NCgkJCQk8bGk+DQoJCQkJCTxhIGhyZWY9ImphdmFzY3JpcHQ6Ly8iIGNsYXNzPSJpbmF2MDMgICI+5Liq5Lq65a6i5rqQPC9hPg0KCQkJCQk8ZGl2Pg0KCQkJCQkJPGRsPjwvZGw+DQoJCQkJCQk8cD48YSBocmVmPSIvaG91c2UvYnV5Ij7kuKrkurrmsYLotK08L2E+PGEgaHJlZj0iL2hvdXNlL2hpcmUiPuS4quS6uuaxguennzwvYT48L3A+DQoJCQkJCTwvZGl2Pg0KCQkJCTwvbGk+DQoJCQkJPGxpPjxhIGhyZWY9Ii9hY3QvZ3VvaHUyL2luZGV4Lmh0bWwiPuS7o+WKnui/h+aItzwvYT48L2xpPg0KCQkJCTxsaT48YSBocmVmPSJodHRwOi8vZXNmLm1laXpoYWkuY24vIj7nnIHpkrHkubDmiL88L2E+PC9saT4NCgkJCTwvdWw+DQoJCQk8c2NyaXB0IHR5cGU9InRleHQvamF2YXNjcmlwdCI+bWVudSgpPC9zY3JpcHQ+DQoNCgkJPC9kaXY+DQoJPC9kaXY+DQoNCgk8IS0tYmFubmVyLS0+DQoJPGRpdiBjbGFzcz0iaWJhbm5lciI+DQoJCTxkaXYgY2xhc3M9ImNfaV9iYW5uZXIiPg0KCQkJPHVsIGNsYXNzPSJjX2lfYmFubmVyTWFpbiI+DQoJCQkJPGxpIHN0eWxlPSJkaXNwbGF5Omxpc3QtaXRlbTsiPjxpbWcgYWx0PSIiIHNyYz0iL2ltYWdlcy9uZXdoZWFkL2liYW5uZXIuanBnIiB0aXRsZT0iIiBib3JkZXI9JzBweCcgaGVpZ2h0PSI2MDAiIC8+PC9saT4NCgkJCQkNCgkJCQk8bGkgc3R5bGU9ImRpc3BsYXk6bm9uZTsiPjxhIGhyZWY9Imh0dHA6Ly9lc2YubWVpemhhaS5jbi8iIHRhcmdldD0iX2JsYW5rIiB0aXRsZT0iIj48aW1nIGFsdD0iIiBzcmM9Ii9pbWFnZXMvbmV3aGVhZC9pYmFubmVyMDMuanBnIiB0aXRsZT0iIiBib3JkZXI9JzBweCcgLz48L2E+PC9saT4NCgkJCQkNCgkJCTwvdWw+DQoJCQk8aW5wdXQgdHlwZT0idGV4dCIgc3R5bGU9ImRpc3BsYXk6IG5vbmU7IiB2YWx1ZT0iMTAiIGlkPSJpbWdJbnRlcnZhbHRpbWUiIC8+DQoJCQk8ZGl2IGNsYXNzPSJjX2lfYmFubmVyU21hbGwiPg0KCQkJCTxkaXYgY2xhc3M9ImNfaV9idGFiIj4NCgkJCQkJPHVsPg0KCQkJCQkJPGxpIGNsYXNzPSJvbiI+PC9saT4NCgkJCQkJCTxsaT48L2xpPg0KCQkJCQkJDQoJCQkJCTwvdWw+DQoJCQkJPC9kaXY+DQoJCQk8L2Rpdj4NCgkJPC9kaXY+DQoJPC9kaXY+DQoJPCEtLWJhbm5lciBlbmQtLT4NCgk8IS0t5Liq5Lq6LS0+DQoJPGRpdiBjbGFzcz0iaWdyIj4NCgkJPHVsPg0KCQkJPGxpPuS4quS6uuWHuuennzxmb250PjEzMzY0NjwvZm9udD7lpZc8L2xpPg0KCQkJPGxpPuS4quS6uuWHuuWUrjxmb250PjM4NzM1PC9mb250PuWllzwvbGk+DQoJCQk8bGk+5Liq5Lq65rGC56efPGZvbnQ+MDwvZm9udD7lpZc8L2xpPg0KCQkJPGxpPuS4quS6uuaxgui0rTxmb250PjA8L2ZvbnQ+5aWXPC9saT4NCgkJPC91bD4NCgk8L2Rpdj4NCgk8IS0t5Liq5Lq6IGVuZC0tPg0KDQo8L2Rpdj4NCjxkaXYgY2xhc3M9ImltYWluIj4NCgk8IS0t5Liq5Lq65Ye65ZSuLS0+DQoJPGRpdiBjbGFzcz0iaWdyY3MiPg0KCQk8ZGl2IGNsYXNzPSJpZ3Jjc3RvcCI+PGZvbnQ+5Liq5Lq65Ye65ZSuPC9mb250PjxwPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL2NoYW95YW5nLyI+5pyd6ZizPC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL2hhaWRpYW4vIj7mtbfmt4A8L2E+PGEgaHJlZj0iL2hvdXNlL3NhbGUvZmVuZ3RhaSI+5Liw5Y+wPC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL2RvbmdjaGVuZy8iPuS4nOWfjjwvYT48YSBocmVmPSIvaG91c2Uvc2FsZS94aWNoZW5nLyI+6KW/5Z+OPC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL2Nob25nd2VuLyI+5bSH5paHPC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL3h1YW53dS8iPuWuo+atpjwvYT48YSBocmVmPSIvaG91c2Uvc2FsZS9zaGlqaW5nc2hhbiI+55+z5pmv5bGxPC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL2NoYW5ncGluZyI+5piM5bmzPC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL3Rvbmd6aG91LyI+6YCa5beePC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL2RheGluLyI+5aSn5YW0PC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL3NodW5pLyI+6aG65LmJPC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlL2ZhbmdzaGFuLyI+5oi/5bGxPC9hPjxhIGhyZWY9Ii9ob3VzZS9zYWxlLyIgY2xhc3M9Imltb3JlIj7mm7TlpJo+PC9hPjwvcD48L2Rpdj4NCgkJPHVsIGNsYXNzPSJpZ3Jjc3VsIj4NCg0KCQkJCTxsaT4NCgkJCQkJPGEgaHJlZj0nL2hvdXNlL2RldGFpbC9jY2Y4OTc0My1hMmY0LTRkNTAtYTQ1Yi1lM2FiYjU1NWM3YTRfMSAnPg0KCQkJCQkJPGltZyBvcmlnaW5hbD0iaHR0cDovL2ltZzEubWVpemhhaS5jb20uY24vaW1hZ2VzL2hvdXNlbS5qcGciIHdpZHRoPSIxNjAiIGhlaWdodD0iMTIwIg0KCQkJCQkJCSBzcmM9Imh0dHA6Ly9pbWcxLm1laXpoYWkuY29tLmNuL2ltYWdlcy9ob3VzZW0uanBnIg0KCQkJCQkJCSBvbmVycm9yPSJ0aGlzLnNyYz0gJy9pbWFnZXMvaG91c2VtLmpwZyc7IiAvPg0KCQkJCQk8L2E+PGRpdj4NCgkJCQkJCeadv+ahpeaWsOiLkTxiciAvPjxmb250Pjc2PC9mb250PuS4hzxiciAvPg0KCQkJCQkJMeWupOWFtuS7li81Ni4wMOW5s+exsw0KCQkJCQk8L2Rpdj4NCgkJCQk8L2xpPg0KCQkJCTxsaT4NCgkJCQkJPGEgaHJlZj0nL2hvdXNlL2RldGFpbC9jMzkzMjExNS1jNGE3LTQyNzYtODdkNC00ZjRlYzZhZjllZTFfMSAnPg0KCQkJCQkJPGltZyBvcmlnaW5hbD0iaHR0cDovL2ltZzEubWVpemhhaS5jb20uY24vaW1hZ2VzL2hvdXNlbS5qcGciIHdpZHRoPSIxNjAiIGhlaWdodD0iMTIwIg0KCQkJCQkJCSBzcmM9Imh0dHA6Ly9pbWcxLm1laXpoYWkuY29tLmNuL2ltYWdlcy9ob3VzZW0uanBnIg0KCQkJCQkJCSBvbmVycm9yPSJ0aGlzLnNyYz0gJy9pbWFnZXMvaG91c2VtLmpwZyc7IiAvPg0KCQkJCQk8L2E+PGRpdj4NCgkJCQkJCeWNjum+meiLkeS4remHjDxiciAvPjxmb250PjE0NTwvZm9udD7kuIc8YnIgLz4NCgkJCQkJCTHlrqQx5Y6FLzUwLjAw5bmz57GzDQoJCQkJCTwvZGl2Pg0KCQkJCTwvbGk+DQoJCQkJPGxpPg0KCQkJCQk8YSBocmVmPScvaG91c2UvZGV0YWlsLzAyYTc1MWQzLTZjZWEtNDQ5MC1hOWM1LWFiNzRmNzExNWNjNV8xICc+DQoJCQkJCQk8aW1nIG9yaWdpbmFsPSJodHRwOi8vaW1nMS5tZWl6aGFpLmNvbS5jbi91cGxvYWQvY29tbVwzNFwxMDEwNDQ3MDYzXGI0YjA4ZjY4LWExM2MtNGE5Ny1hNDUzLTExMTE1MDgxZmE3ZV9tLmpwZyIgd2lkdGg9IjE2MCIgaGVpZ2h0PSIxMjAiDQoJCQkJCQkJIHNyYz0iaHR0cDovL2ltZzEubWVpemhhaS5jb20uY24vdXBsb2FkL2NvbW1cMzRcMTAxMDQ0NzA2M1wyNjViMDhiNS1iYjMyLTQ2YmUtYTcxYS1lY2ZkMzZlNzg0NzhfbS5qcGciDQoJCQkJCQkJIG9uZXJyb3I9InRoaXMuc3JjPSAnL2ltYWdlcy9ob3VzZW0uanBnJzsiIC8+DQoJCQkJCTwvYT48ZGl2Pg0KCQkJCQkJ5Y+M5rOJ5aCh55SyMeWPt+mZojxiciAvPjxmb250PjQ3MDwvZm9udD7kuIc8YnIgLz4NCgkJCQkJCTLlrqQy5Y6FLzg4LjAw5bmz57GzDQoJCQkJCTwvZGl2Pg0KCQkJCTwvbGk+DQoJCQkJPGxpPg0KCQkJCQk8YSBocmVmPScvaG91c2UvZGV0YWlsLzJiOGM4ZGI5LTIwNDAtNGU5MS04ZTYwLWQ1NzYyZWNkYmM3OF8xICc+DQoJCQkJCQk8aW1nIG9yaWdpbmFsPSJodHRwOi8vaW1nMS5tZWl6aGFpLmNvbS5jbi91cGxvYWQvY29tbVw0MVwxMDEwNzE5NDYzXDU2OWUzZTlhLWQ2YjMtNDRmMy05ODUwLTA2MTc2NjZiNzA3NV9tLmpwZyIgd2lkdGg9IjE2MCIgaGVpZ2h0PSIxMjAiDQoJCQkJCQkJIHNyYz0iaHR0cDovL2ltZzEubWVpemhhaS5jb20uY24vdXBsb2FkL2NvbW1cNDFcMTAxMDcxOTQ2M1w1NjllM2U5YS1kNmIzLTQ0ZjMtOTg1MC0wNjE3NjY2YjcwNzVfbS5qcGciDQoJCQkJCQkJIG9uZXJyb3I9InRoaXMuc3JjPSAnL2ltYWdlcy9ob3VzZW0uanBnJzsiIC8+DQoJCQkJCTwvYT48ZGl2Pg0KCQkJCQkJ6Imv5Lmh5qW45qCR5a625ZutPGJyIC8+PGZvbnQ+MTgwPC9mb250PuS4hzxiciAvPg0KCQkJCQkJMuWupDHljoUvODAuMDDlubPnsbMNCgkJCQkJPC9kaXY+DQoJCQkJPC9saT4NCgkJCQk8bGk+DQoJCQkJCTxhIGhyZWY9Jy9ob3VzZS9kZXRhaWwvYWIxYjMxMTktYTUxOC00MDAyLWI5OGMtZDI0OGNjZTE1N2FiXzEgJz4NCgkJCQkJCTxpbWcgb3JpZ2luYWw9Imh0dHA6Ly9pbWcxLm1laXpoYWkuY29tLmNuL3VwbG9hZC9jb21tXDI4XDEwMTAxNzAzNzVcMzc4ODNmZWItNWMyMS00ZjVlLWJiZTYtZjM2YTQ0NjhlOTRiX20uanBnIiB3aWR0aD0iMTYwIiBoZWlnaHQ9IjEyMCINCgkJCQkJCQkgc3JjPSJodHRwOi8vaW1nMS5tZWl6aGFpLmNvbS5jbi91cGxvYWQvY29tbVwyOFwxMDEwMTcwMzc1XGI4MmRiYWZjLTkzM2YtNDZmZC05ODQ4LTU0YWU1MjUzZmI1ZV9tLmpwZyINCgkJCQkJCQkgb25lcnJvcj0idGhpcy5zcmM9ICcvaW1hZ2VzL2hvdXNlbS5qcGcnOyIgLz4NCgkJCQkJPC9hPjxkaXY+DQoJCQkJCQnkupTmnIjljY7luq08YnIgLz48Zm9udD4yNjg8L2ZvbnQ+5LiHPGJyIC8+DQoJCQkJCQkx5a6k5YW25LuWLzUyLjAw5bmz57GzDQoJCQkJCTwvZGl2Pg0KCQkJCTwvbGk+DQoJCQk8cCBjbGFzcz0iY2xlYXIiPjwvcD4NCgkJPC91bD4NCgk8L2Rpdj4NCgk8IS0t5Liq5Lq65Ye65ZSuIGVuZC0tPg0KCTwhLS3kuKrkurrlh7rnp58tLT4NCgk8ZGl2IGNsYXNzPSJpZ3JjcyI+DQoJCTxkaXYgY2xhc3M9ImlncmNzdG9wIj48Zm9udD7kuKrkurrlh7rnp588L2ZvbnQ+PHA+PGEgaHJlZj0iL2hvdXNlL3JlbnQvY2hhb3lhbmcvIj7mnJ3pmLM8L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvaGFpZGlhbi8iPua1t+a3gDwvYT48YSBocmVmPSIvaG91c2UvcmVudC9mZW5ndGFpIj7kuLDlj7A8L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvZG9uZ2NoZW5nLyI+5Lic5Z+OPC9hPjxhIGhyZWY9Ii9ob3VzZS9yZW50L3hpY2hlbmcvIj7opb/ln448L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvY2hvbmd3ZW4vIj7ltIfmloc8L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQveHVhbnd1LyI+5a6j5q2mPC9hPjxhIGhyZWY9Ii9ob3VzZS9yZW50L3NoaWppbmdzaGFuIj7nn7Pmma/lsbE8L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvY2hhbmdwaW5nIj7mmIzlubM8L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvdG9uZ3pob3UvIj7pgJrlt548L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvZGF4aW4vIj7lpKflhbQ8L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvc2h1bmkvIj7pobrkuYk8L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvZmFuZ3NoYW4vIj7miL/lsbE8L2E+PGEgaHJlZj0iL2hvdXNlL3JlbnQvIiBjbGFzcz0iaW1vcmUiPuabtOWkmj48L2E+PC9wPjwvZGl2Pg0KCQk8dWwgY2xhc3M9ImlncmNzdWwiPg0KDQoJCQkJPGxpPg0KCQkJCQk8YSBocmVmPScvaG91c2UvZGV0YWlsL2ZjYzg4ZDI4LTBkYjUtNDE4NC05ZWM3LTNmOGYyOGI4NTRmMV8yICc+DQoJCQkJCQk8aW1nIG9yaWdpbmFsPSJodHRwOi8vaW1nMS5tZWl6aGFpLmNvbS5jbi9pbWFnZXMvaG91c2VtLmpwZyIgd2lkdGg9IjE2MCIgaGVpZ2h0PSIxMjAiDQoJCQkJCQkJIHNyYz0iaHR0cDovL2ltZzEubWVpemhhaS5jb20uY24vaW1hZ2VzL2hvdXNlbS5qcGciDQoJCQkJCQkJIG9uZXJyb3I9InRoaXMuc3JjPSAnL2ltYWdlcy9ob3VzZW0uanBnJzsiIC8+DQoJCQkJCTwvYT48ZGl2Pg0KCQkJCQkJ56Oo5Z2K5YyX6YeMPGJyIC8+PGZvbnQ+NSwwMDA8L2ZvbnQ+5YWDL+aciDxiciAvPg0KCQkJCQkJMuWupDHljoUvNzQuMDDlubPnsbMNCgkJCQkJPC9kaXY+DQoJCQkJPC9saT4NCgkJCQk8bGk+DQoJCQkJCTxhIGhyZWY9Jy9ob3VzZS9kZXRhaWwvZDJjNTQ4M2EtOTQ3Mi00NDk0LWEyYmQtM2MyMWFiMTVhOWY5XzIgJz4NCgkJCQkJCTxpbWcgb3JpZ2luYWw9Imh0dHA6Ly9pbWcxLm1laXpoYWkuY29tLmNuL3VwbG9hZC9jb21tXDI5XDEwMTAyMTUyMzlcOTQxYmY0ZWYtZDk1My00OWQ5LTkwZGMtODZmMmQ1Y2I4Y2RiX20uanBnIiB3aWR0aD0iMTYwIiBoZWlnaHQ9IjEyMCINCgkJCQkJCQkgc3JjPSJodHRwOi8vaW1nMS5tZWl6aGFpLmNvbS5jbi91cGxvYWQvY29tbVwyOVwxMDEwMjE1MjM5XDQyZDRjY2NjLTg5MGYtNGIyZC04ZDg1LWYwOGM1OWNiOTVhZV9tLmpwZyINCgkJCQkJCQkgb25lcnJvcj0idGhpcy5zcmM9ICcvaW1hZ2VzL2hvdXNlbS5qcGcnOyIgLz4NCgkJCQkJPC9hPjxkaXY+DQoJCQkJCQnmlrDljY7ogZTlrrblm63ljZfljLo8YnIgLz48Zm9udD40LDUwMDwvZm9udD7lhYMv5pyIPGJyIC8+DQoJCQkJCQky5a6kMeWOhS85NS4wMOW5s+exsw0KCQkJCQk8L2Rpdj4NCgkJCQk8L2xpPg0KCQkJCTxsaT4NCgkJCQkJPGEgaHJlZj0nL2hvdXNlL2RldGFpbC83Mzg4ZTNkZS04Yzk4LTQzYmQtYWM0ZC1hOGQxMDYwZTllNDlfMiAnPg0KCQkJCQkJPGltZyBvcmlnaW5hbD0iaHR0cDovL2ltZzEubWVpemhhaS5jb20uY24vdXBsb2FkL2NvbW1cMzlcMTAxMDY1NzMwOVwyZTcyMjEzNS02NzdkLTQ3ZTUtYmJiMC0wZDhmOGUwOWQ3NGFfbS5qcGciIHdpZHRoPSIxNjAiIGhlaWdodD0iMTIwIg0KCQkJCQkJCSBzcmM9Imh0dHA6Ly9pbWcxLm1laXpoYWkuY29tLmNuL3VwbG9hZC9jb21tXDM5XDEwMTA2NTczMDlcMmU3MjIxMzUtNjc3ZC00N2U1LWJiYjAtMGQ4ZjhlMDlkNzRhX20uanBnIg0KCQkJCQkJCSBvbmVycm9yPSJ0aGlzLnNyYz0gJy9pbWFnZXMvaG91c2VtLmpwZyc7IiAvPg0KCQkJCQk8L2E+PGRpdj4NCgkJCQkJCemmluW6p+W+oeiLkTxiciAvPjxmb250PjMsNjAwPC9mb250PuWFgy/mnIg8YnIgLz4NCgkJCQkJCTPlrqQy5Y6FLzExNy4wMOW5s+exsw0KCQkJCQk8L2Rpdj4NCgkJCQk8L2xpPg0KCQkJCTxsaT4NCgkJCQkJPGEgaHJlZj0nL2hvdXNlL2RldGFpbC83Y2E5OTllZi02ZTkxLTQ3Y2YtOWNmYy0wMDc3MGI3OTU4YTZfMiAnPg0KCQkJCQkJPGltZyBvcmlnaW5hbD0iaHR0cDovL2ltZzEubWVpemhhaS5jb20uY24vdXBsb2FkL2NvbW1cMjhcMTAxMDE4NjA2M1w3OWYxN2ZmNy1iZDc1LTRkMzctODkxMS1mOWZhODk2ZjNlZGJfbS5qcGciIHdpZHRoPSIxNjAiIGhlaWdodD0iMTIwIg0KCQkJCQkJCSBzcmM9Imh0dHA6Ly9pbWcxLm1laXpoYWkuY29tLmNuL3VwbG9hZC9jb21tXDI4XDEwMTAxODYwNjNcNzlmMTdmZjctYmQ3NS00ZDM3LTg5MTEtZjlmYTg5NmYzZWRiX20uanBnIg0KCQkJCQkJCSBvbmVycm9yPSJ0aGlzLnNyYz0gJy9pbWFnZXMvaG91c2VtLmpwZyc7IiAvPg0KCQkJCQk8L2E+PGRpdj4NCgkJCQkJCemjjuagvOa0vjxiciAvPjxmb250PjUsOTAwPC9mb250PuWFgy/mnIg8YnIgLz4NCgkJCQkJCTHlrqTlhbbku5YvNTIuMDDlubPnsbMNCgkJCQkJPC9kaXY+DQoJCQkJPC9saT4NCgkJCQk8bGk+DQoJCQkJCTxhIGhyZWY9Jy9ob3VzZS9kZXRhaWwvMDkzZGQwOGMtNTIxNS00YWU1LTk5NzQtOTZjNjU5YzgwOTgzXzIgJz4NCgkJCQkJCTxpbWcgb3JpZ2luYWw9Imh0dHA6Ly9pbWcxLm1laXpoYWkuY29tLmNuL3VwbG9hZC9jb21tXDM1XDEwMTA1MjcxOTFcNDU4ZmRmNmEtNGIzNi00YmJhLWEyMTAtMWViNTZhOWEyY2NhX20uanBnIiB3aWR0aD0iMTYwIiBoZWlnaHQ9IjEyMCINCgkJCQkJCQkgc3JjPSJodHRwOi8vaW1nMS5tZWl6aGFpLmNvbS5jbi91cGxvYWQvY29tbVwzNVwxMDEwNTI3MTkxXGViNzQ2NGU2LWM3M2UtNGQzZi04NGQwLTM0NDY3YjE2NWU1ZV9tLmpwZyINCgkJCQkJCQkgb25lcnJvcj0idGhpcy5zcmM9ICcvaW1hZ2VzL2hvdXNlbS5qcGcnOyIgLz4NCgkJCQkJPC9hPjxkaXY+DQoJCQkJCQnlkI3kvbPoirHlm63kuIDljLo8YnIgLz48Zm9udD4zLDUwMDwvZm9udD7lhYMv5pyIPGJyIC8+DQoJCQkJCQkx5a6kMeWOhS82My4wMOW5s+exsw0KCQkJCQk8L2Rpdj4NCgkJCQk8L2xpPg0KCQkJPHAgY2xhc3M9ImNsZWFyIj48L3A+DQoJCTwvdWw+DQoJPC9kaXY+DQoJPCEtLeS4quS6uuWHuuennyBlbmQtLT4NCgk8IS0t5L2j6YeRMSUtLT4NCgk8ZGl2IGNsYXNzPSJpeWoiPg0KCQk8ZGl2PjxhIGhyZWY9Imh0dHA6Ly9lc2YubWVpemhhaS5jbiI+PGltZyBzcmM9Ii9pbWFnZXMvbmV3aGVhZC9peWouanBnIiB3aWR0aD0iMTAwMCIgaGVpZ2h0PSIzMDAiIGJvcmRlcj0iMCIgLz48L2E+PC9kaXY+DQoJPC9kaXY+DQoJPCEtLeS9o+mHkTElIGVuZC0tPg0KCTwhLS3nnIHpkrHkubDmiL8tLT4NCgk8ZGl2IGNsYXNzPSJpc3FtZiI+DQoJCTxkaXYgY2xhc3M9ImlzcW1mdG9wIj48Zm9udD7nnIHpkrHkubDmiL88L2ZvbnQ+PGEgaHJlZj0iaHR0cDovL2VzZi5tZWl6aGFpLmNuIiBjbGFzcz0iaW1vcmUiPuabtOWkmj4gPC9hPjwvZGl2Pg0KCQk8dWwgY2xhc3M9ImlzcW1mdWwiPg0KCQkJPGxpPg0KCQkJCTxhIGhyZWY9Imh0dHA6Ly9lc2YubWVpemhhaS5jbi9jZHlqLmh0bWwiIGNsYXNzPSJpc3FtZnVsMDEiPjxzcGFuPjwvc3Bhbj48Zm9udD7otoXkvY7kvaPph5E8L2ZvbnQ+PGJyIC8+5Lyg57uf5Lit5LuL6LS555qEMS8zPC9hPg0KCQkJPC9saT4NCgkJCTxsaT4NCgkJCQk8YSBocmVmPSJodHRwOi8vZXNmLm1laXpoYWkuY24vIiBjbGFzcz0iaXNxbWZ1bDAyIj48c3Bhbj48L3NwYW4+PGZvbnQ+5oi/5rqQ5YWo6Z2iPC9mb250PjxiciAvPuWcqOWUruaIv+a6kDk1JeWFqOimhuebljwvYT4NCgkJCTwvbGk+DQoJCQk8bGk+DQoJCQkJPGEgaHJlZj0iaHR0cDovL2VzZi5tZWl6aGFpLmNuL3pzZ3cuaHRtbCIgY2xhc3M9ImlzcW1mdWwwMyI+PHNwYW4+PC9zcGFuPjxmb250Pui1hOa3semhvumXrjwvZm9udD48YnIgLz7ljYHlubTotYTmt7Hnu4/nuqrkuro8L2E+DQoJCQk8L2xpPg0KCQkJPGxpPg0KCQkJCTxhIGhyZWY9Imh0dHA6Ly9lc2YubWVpemhhaS5jbi9hcWJ6Lmh0bWwiIGNsYXNzPSJpc3FtZnVsMDQiPjxzcGFuPjwvc3Bhbj48Zm9udD7lronlhajkv53pmpw8L2ZvbnQ+PGJyIC8+5YWo56iL5Lqk5piTMOmjjumZqTwvYT4NCgkJCTwvbGk+DQoJCQk8cCBjbGFzcz0iY2xlYXIiPjwvcD4NCgkJPC91bD4NCgk8L2Rpdj4NCgk8IS0t55yB6ZKx5Lmw5oi/ZW5kLS0+DQoJPCEtLeS6jOaJi+aIv+iHquihjOaIkOS6pC0tPg0KCTxkaXYgY2xhc3M9Imllc2YiPg0KCQk8ZGl2IGNsYXNzPSJpZXNmMDEiPjxwPuS6jOaJi+aIv+iHquihjOaIkOS6pO+8jDxmb250Puetvue6puOAgee8tOeojuOAgei/h+aItzwvZm9udD7vvIzlhajlpZfmiYvnu63kuIDnq5nmkJ7lrprvvIE8L3A+PGEgaHJlZj0iL2FjdC9ndW9odTIvaW5kZXguaHRtbCI+5LqG6Kej6K+m5oOFPC9hPjwvZGl2Pg0KCQk8dWwgY2xhc3M9Imllc2YwMiI+DQoJCQk8bGk+PGRpdj48aW1nIHNyYz0iL2ltYWdlcy9uZXdoZWFkL25ld2luZGV4MTAucG5nIiB3aWR0aD0iMTI5IiBoZWlnaHQ9IjEyOSIgLz48cD48Zm9udD4zMDAw5YWD5pCe5a6aPC9mb250PjxiciAvPuaJgOacieS6pOaYk+aJi+e7rTwvcD48L2Rpdj48L2xpPg0KCQkJPGxpPjxkaXY+PGltZyBzcmM9Ii9pbWFnZXMvbmV3aGVhZC9uZXdpbmRleDExLnBuZyIgd2lkdGg9IjEyOSIgaGVpZ2h0PSIxMjkiIC8+PHA+PGZvbnQ+OOWkp+aOquaWvTwvZm9udD48YnIgLz7noa7kv53kuqTmmJMw6aOO6ZmpPC9wPjwvZGl2PjwvbGk+DQoJCQk8bGk+PGRpdj48aW1nIHNyYz0iL2ltYWdlcy9uZXdoZWFkL25ld2luZGV4MTIucG5nIiB3aWR0aD0iMTI5IiBoZWlnaHQ9IjEyOSIgLz48cD48Zm9udD7kuIDnq5nop6PlhrM8L2ZvbnQ+PGJyIC8+57mB55CQ6L+H5oi35omL57utPC9wPjwvZGl2PjwvbGk+DQoJCTwvdWw+DQoJPC9kaXY+DQoJPCEtLeS6jOaJi+aIv+iHquihjOaIkOS6pCBlbmQtLT4NCg0KCTwhLS1ib3R0b20tLT4NCgk8ZGl2IGNsYXNzPSJpYm90dG9tIj4NCgkJPHVsIGNsYXNzPSJpYm90dG9tdWwiPg0KCQkJPGxpPjxzcGFuPjxpbWcgc3JjPSIvaW1hZ2VzL25ld2hlYWQvbmV3aW5kZXgxNS5qcGciIC8+PC9zcGFuPjxwPjxmb250PuaOjOS4iue+juWuhTwvZm9udD48YnIgLz7pmo/ml7bpmo/lnLDmib7miL/mupA8YnIgLz48YSBocmVmPSIvSG9tZS9NelBob25lIj7pqazkuIrkuIvovb08L2E+PC9wPjwvbGk+DQoJCQk8bGk+PHNwYW4+PGltZyBzcmM9Ii9pbWFnZXMvbmV3aGVhZC9uZXdpbmRleDE3LmpwZyIgLz48L3NwYW4+PHA+PGZvbnQ+56iO6LS56K6h566X5ZmoPC9mb250PjxiciAvPuWPsuS4iuacgOeyvuehrueahOeul+eojuelnuWZqDxiciAvPjxhIGhyZWY9IiMiPuWOu+eci+ecizwvYT48L3A+PC9saT4NCgkJCTxsaT48c3Bhbj48aW1nIHNyYz0iL2ltYWdlcy9uZXdoZWFkL25ld2luZGV4MTguanBnIiAvPjwvc3Bhbj48cD48Zm9udD7kuK3ku4vnlLXor53mn6Xor6I8L2ZvbnQ+PGJyIC8+5Y2B5bm05rKJ56ev5pyA5YWo5Lit5LuL55S16K+dPGJyIC8+PGEgaHJlZj0iL2hvbWUvVGhlbWVQaG9uZSI+5Y6755yL55yLPC9hPjwvcD48L2xpPg0KCQk8L3VsPg0KCTwvZGl2Pg0KCTwhLS1ib3R0b20gZW5kLS0+DQoNCg0KPC9kaXY+DQo8ZGl2IHN0eWxlPSJkaXNwbGF5Om5vbmUiIGlkPSJpcGFkZHJlc3MiPkZhbHNlPC9kaXY+DQo8ZGl2IGNsYXNzPSJxdF93YWlkdiIgc3R5bGU9ImRpc3BsYXk6IG5vbmUiPg0KCTxkaXYgY2xhc3M9InF0X2xvZ2luIj4NCgk8L2Rpdj4NCgk8ZGl2IGNsYXNzPSJxdF9sb2diZyI+DQoJCTxkaXYgY2xhc3M9InF0X2xvZ3RiZyI+DQoJCQk8c3Bhbj7ln47luILliIfmjaI8L3NwYW4+PGlucHV0IHR5cGU9ImJ1dHRvbiIgdmFsdWU9IiIgdGl0bGU9IuWFs+mXrSIgY2xhc3M9IiIgb25jbGljaz0iY2xvc2VkaXYoKSIgLz4NCgkJPC9kaXY+DQoJCTxkaXYgY2xhc3M9ImJsYW5rMTAiPg0KCQk8L2Rpdj4NCgkJPGZvcm0+DQoJCQk8dGFibGU+DQoJCQkJPHRyPg0KCQkJCQk8dGQgd2lkdGg9IjMzMCIgaGVpZ2h0PSI0OSIgYWxpZ249ImNlbnRlciI+DQoJCQkJCQk8YnIgLz48YnIgLz48YnIgLz48aDM+5Lmf6K645oKo6ZyA6KaB5YiH5o2i5YiwJm5ic3A7PGEgaHJlZj0iaHR0cDovL2hhbmRhbi5tZWl6aGFpLmNuIiB0YXJnZXQ9Il9zZWxmIj7pgq/pg7jnq5k8L2E+PzwvaDM+PGJyIC8+DQoJCQkJCTwvdGQ+DQoJCQkJPC90cj4NCgkJCQk8dHI+DQoJCQkJCTx0ZCB3aWR0aD0iMzMwIiBoZWlnaHQ9IjQ5IiBhbGlnbj0iY2VudGVyIj4NCgkJCQkJCTxiciAvPjxoMz7mgqjov5jlj6/ku6UmbmJzcDs8YSBocmVmPSJqYXZhc2NyaXB0Oi8vIiBvbmNsaWNrPSJjbG9zZWRpdigpIiB0YXJnZXQ9Il9zZWxmIj7nlZnlnKjljJfkuqznq5k8L2E+PC9oMz48YnIgLz4NCgkJCQkJPC90ZD4NCgkJCQk8L3RyPg0KCQkJPC90YWJsZT4NCgkJPC9mb3JtPg0KCQk8ZGl2IGNsYXNzPSJxdF9idG1fIj4NCgkJPC9kaXY+DQoJPC9kaXY+DQo8L2Rpdj4NCjxzY3JpcHQgdHlwZT0idGV4dC9qYXZhc2NyaXB0Ij4NCg0KCXZhciBsb2dpblN1Y2Nlc3MgPSAnJzsNCglpZiAobG9naW5TdWNjZXNzLmxlbmd0aCA+IDApIHsNCgkJdG9wLmxvY2F0aW9uLmhyZWYgPSAnL2FnZW50L3VzZXIvaW5kZXgnOw0KCX0NCgl2YXIgcmVhc29uID0gJyc7DQoJaWYgKHJlYXNvbiAhPSBudWxsICYmIHJlYXNvbi5sZW5ndGggPiAwKSB7DQoJCWFsZXJ0KHJlYXNvbik7DQoJfQ0KCXZhciBsb2dpbkZsYWc7DQoJJChkb2N1bWVudCkucmVhZHkoZnVuY3Rpb24gKCkgew0KCQkkKCIjZnJvbUxvZ2luQWdlbnQiKS5BZ2VudExvZ2luKCk7DQoJCWpRdWVyeSgiLnNsaWRlQm94Iikuc2xpZGUoeyBtYWluQ2VsbDogIi5iZCB1bCIsIGVmZmVjdDogImxlZnRMb29wIiwgYXV0b1BsYXk6IHRydWUgfSk7DQoJCXZhciBhbm90aGVyPWZhbHNlOw0KCQlpZighYW5vdGhlcikNCgkJew0KCQkJYmluZEZhdigpDQoJCX0NCgl9KTsNCglmdW5jdGlvbiBhZGRDb29raWUob2JqTmFtZSwgb2JqVmFsdWUsIG9iakhvdXJzKSB7Ly/mt7vliqBjb29raWUNCg0KCQl2YXIgc3RyID0gb2JqTmFtZSArICI9IiArIGVzY2FwZShvYmpWYWx1ZSk7DQoJCWlmIChvYmpIb3VycyA+IDApIHsvL+S4ujDml7bkuI3orr7lrprov4fmnJ/ml7bpl7TvvIzmtY/op4jlmajlhbPpl63ml7Zjb29raWXoh6rliqjmtojlpLENCgkJCXZhciBkYXRlID0gbmV3IERhdGUoKTsNCgkJCXZhciBtcyA9IDMwICogMTAwMDsNCgkJCWRhdGUuc2V0VGltZShkYXRlLmdldFRpbWUoKSArIG1zKTsNCgkJCXN0ciArPSAiOyBleHBpcmVzPSIgKyBkYXRlLnRvR01UU3RyaW5nKCk7DQoJCX0NCgkJZG9jdW1lbnQuY29va2llID0gc3RyOw0KCX0NCg0KPC9zY3JpcHQ+DQoNCjxzY3JpcHQgc3JjPSIvU2NyaXB0cy9qcXVlcnkuYmxvY2tVSS5qcz92PTIwMTUwNzE3IiB0eXBlPSJ0ZXh0L2phdmFzY3JpcHQiPjwvc2NyaXB0Pg0KDQoNCgk8IS0t5bqV6YOo54mI5p2DLS0+DQoJPGRpdiBjbGFzcz0iZm9vdGVyIj4NCgkJPHA+DQoJCTxhIGhyZWY9Ii9BZ2VudC9BYm91dE91ci9BYm91dE1laXpoYWkiIHRhcmdldD0iX2JsYW5rIj7lhbPkuo7nvo7lroU8L2E+DQoJCSZuYnNwOyZuYnNwO3wmbmJzcDsmbmJzcDsNCgkJPGEgaHJlZj0iL0FnZW50L0Fib3V0T3VyL0pvaW5NZWl6aGFpIiB0YXJnZXQ9Il9ibGFuayI+5Yqg5YWl576O5a6FPC9hPg0KCQkmbmJzcDsmbmJzcDt8Jm5ic3A7Jm5ic3A7DQoJCTxhIGhyZWY9Ii9BZ2VudC9BYm91dE91ci9Db250YWN0TWVpemhhaSIgdGFyZ2V0PSJfYmxhbmsiPuiBlOezu+aIkeS7rDwvYT4NCgkJJm5ic3A7Jm5ic3A7fCZuYnNwOyZuYnNwOw0KCQk8YSBocmVmPSIvQWdlbnQvQWJvdXRPdXIvRmVlZGJhY2siIHRhcmdldD0iX2JsYW5rIj7mhI/op4Hlj43ppog8L2E+DQoJCSZuYnNwOyZuYnNwO3wmbmJzcDsmbmJzcDsNCgkJPGEgaHJlZj0iL3NpdGVtYXAiIHRhcmdldD0iX2JsYW5rIj7nvZHnq5nlnLDlm748L2E+DQoJCSZuYnNwOyZuYnNwO3wmbmJzcDsmbmJzcDsNCgkJCTxhIGhyZWY9Ii9QYXJ0bmVyIiB0YXJnZXQ9Il9ibGFuayI+5ZCI5L2c5LyZ5Ly0PC9hPjwvcD4NCgkJPHA+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9QSIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7QSZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1CIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtCJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PUMiIHRhcmdldD0iX3NlbGYiPiZuYnNwO0MmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9RCIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7RCZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1FIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtFJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PUYiIHRhcmdldD0iX3NlbGYiPiZuYnNwO0YmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9RyIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7RyZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1IIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtIJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PUkiIHRhcmdldD0iX3NlbGYiPiZuYnNwO0kmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9SiIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7SiZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1LIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtLJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PUwiIHRhcmdldD0iX3NlbGYiPiZuYnNwO0wmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9TSIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7TSZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1OIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtOJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PU8iIHRhcmdldD0iX3NlbGYiPiZuYnNwO08mbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9UCIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7UCZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1RIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtRJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PVIiIHRhcmdldD0iX3NlbGYiPiZuYnNwO1ImbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9UyIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7UyZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1UIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtUJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PVUiIHRhcmdldD0iX3NlbGYiPiZuYnNwO1UmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9ViIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7ViZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1XIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtXJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PVgiIHRhcmdldD0iX3NlbGYiPiZuYnNwO1gmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9WSIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7WSZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT1aIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDtaJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PTAiIHRhcmdldD0iX3NlbGYiPiZuYnNwOzAmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9MSIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7MSZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT0yIiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDsyJm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PTMiIHRhcmdldD0iX3NlbGYiPiZuYnNwOzMmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9NCIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7NCZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT01IiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDs1Jm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PTYiIHRhcmdldD0iX3NlbGYiPiZuYnNwOzYmbmJzcDs8L2E+DQoJCTxhIGhyZWY9Ii9ob21lL3NpdGVpbmRleD9rZXk9NyIgdGFyZ2V0PSJfc2VsZiI+Jm5ic3A7NyZuYnNwOzwvYT4NCgkJPGEgaHJlZj0iL2hvbWUvc2l0ZWluZGV4P2tleT04IiB0YXJnZXQ9Il9zZWxmIj4mbmJzcDs4Jm5ic3A7PC9hPg0KCQk8YSBocmVmPSIvaG9tZS9zaXRlaW5kZXg/a2V5PTkiIHRhcmdldD0iX3NlbGYiPiZuYnNwOzkmbmJzcDs8L2E+DQoJCTwvcD4NCgkJPHA+Q29weXJpZ2h0wqkyMDE1IOe+juWuhee9kSBBbGwgcmlnaHRzIHJlc2V2ZXJ2ZWQu5LqsSUNQ5aSHMTEwMTYxNDDlj7ctM+WPtyDkuqzlhaznvZHlronlpIcgMTEwMTA1MDA5Njk4PC9wPg0KCTwvZGl2Pg0KDQoJDQoJDQo8L2JvZHk+DQo8L2h0bWw+DQo=");
            //File.WriteAllBytes("d:\\s.jpg", data);

            var dt = DateTime.Now;
            Stopwatch.StartNew();
            var wh = new List<ManualResetEvent>();
            for (var i = 1; i < 10; i++)
            {
                wh.Add(new ManualResetEvent(false));
                new Thread(o => { Thread.Sleep(1000); }).Start();
            }
            WaitHandle.WaitAll(wh.ToArray());
            Console.WriteLine("st->" + (DateTime.Now - dt).TotalMilliseconds);

        }
        private void CutImage(string tel, int x, int y, int w, int h)
        {
            var img = Image.FromFile(tel);
            var newimg = new Bitmap(120, 20);
            var g = Graphics.FromImage(newimg);
            g.DrawImage(img, new Rectangle(0, 0, 120, 20), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
            img.Dispose();
            g.Dispose();
            newimg.Save(tel);
            newimg.Dispose();
        }
        /// <summary>
        /// 支付信息
        /// </summary>
        [XmlType("xml")]
        public class payinfo
        {
            /// <summary>
            /// 公众号id
            /// </summary>
            public string appid { get; set; }
            /// <summary>
            /// 商户id
            /// </summary>
            public string mchid { get; set; }
            /// <summary>
            /// 随机字符串
            /// </summary>
            public string nonce_str { get; set; }
            /// <summary>
            /// 签名
            /// </summary>
            public string sign { get; set; }
            /// <summary>
            /// 商户订单号
            /// 我们使用卡号
            /// </summary>
            public string partner_trade_no { get; set; }
            /// <summary>
            /// 用户openid
            /// </summary>
            public string openid { get; set; }
            /// <summary>
            /// 是否验证姓名
            /// </summary>
            public string check_name { get { return "NO_CHECK"; } }
            /// <summary>
            /// 用户姓名
            /// </summary>
            public string re_user_name { get; set; }
            /// <summary>
            /// 付款金额（分）
            /// </summary>
            public int amount { get; set; }
            /// <summary>
            /// 付款描述
            /// 我们使用模板
            /// 返现卡：卡号 返现金额：金额
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// Ip地址
            /// </summary>
            public string spbill_create_ip { get; set; }
        }

        [TestMethod]
        public void Test1()
        {
            //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //Console.WriteLine(Convert.ToInt64(ts.TotalSeconds).ToString());
            //Console.WriteLine(22222.ToString("0000"));
            Console.WriteLine(Serialize.ToXml(new payinfo() { amount = 60000, appid = "appid", desc = "desc", mchid = "mchid", nonce_str = "nonce_str", openid = "openid", partner_trade_no = "no", re_user_name = "name", sign = "sign", spbill_create_ip = "ip" }));
        }

        [Serializable]
        public class Msg<T>
        {
            public Dictionary<string, T> dict { get; set; }
            public Msg()
            {
                dict = new Dictionary<string, T>();
            }
            public string ToJson()
            {
                return Serialize.ToJson(dict);
            }
            public string ToXml()
            {

                return Serialize.ToXml(dict);
            }
        }

        public class Push
        {
            public string AppId { get { return GetValue("AppId"); } }
            public string CreateTime { get { return GetValue("CreateTime"); } }
            public string InfoType { get { return GetValue("InfoType"); } }

            private Dictionary<string, string> ps = new Dictionary<string, string>();

            /// <summary>
            /// Initializes a new instance of the Push class.
            /// </summary>
            public Push(string xml)
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                var root = doc.FirstChild;
                foreach (XmlNode n in root.ChildNodes)
                {
                    var v = "";
                    if (n.NodeType == XmlNodeType.CDATA) v = n.FirstChild.InnerText;
                    else v = n.InnerText;
                    ps.Add(n.Name, v);
                }
            }

            public string GetValue(string name)
            {
                if (ps.ContainsKey(name)) return ps[name];
                return "";
            }
        }


    }
}
