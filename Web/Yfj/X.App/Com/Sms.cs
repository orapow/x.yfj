using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using X.Core.Utility;

namespace X.App.Com
{
    public class Sms
    {
        public static string SendSms(string cfgstr, string to, string content)
        {
            cfgstr = cfgstr.Replace("\\,", "<->");
            var cfg = cfgstr.Split(',');
            if (cfg[0] == "0") return string.Empty;
            try
            {
                var url = cfg[1].Replace("<->", ",").Replace("{tel}", to).Replace("{txt}", content);
                var result = Tools.GetHttpData(url);
                if (!new Regex(cfg[2].Replace("<->", ",")).IsMatch(result)) throw new Exception(result);
                return string.Empty;
            }
            catch (Exception ex)
            {
                X.Core.Plugin.Loger.Error(String.Format("{0:yyyy-MM-dd hh:mm:ss}，{1}", DateTime.Now, ex.Message));
                return ex.Message;
            }
        }
    }
}
