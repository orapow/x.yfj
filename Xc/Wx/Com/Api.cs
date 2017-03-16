using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Wx.Com;

namespace X.Wx.Mp.Com
{
    public class Api
    {
        private static string api_url = "https://api.weixin.qq.com/cgi-bin/";

        public static string PostData(string api, string data)
        {
            var json = Tools.PostHttpData(api_url + api, data);
            if (json.IndexOf("errcode") > 0) throw new WxExcep(json);
            return json;
        }

        public static string GetData(string api)
        {
            var json = Tools.GetHttpData(api_url + api);
            if (json.IndexOf("errcode") > 0) throw new WxExcep(json);
            return json;
        }
    }

    /// <summary>
    /// 消息基类
    /// </summary>
    public class msgbase
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}
