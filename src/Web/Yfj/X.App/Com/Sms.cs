using System;
using System.Text.RegularExpressions;
using X.Core.Plugin;
using X.Core.Utility;

namespace X.App.Com
{
    public class Sms
    {
        public static string SendSms(string to, string content)
        {
            var url = "https://sms.253.com/msg/send?un=N2626427&pw=XumTEGLPrh81e2&phone={tel}&msg={txt}&rd=1";
            try
            {
                url = url.Replace("{tel}", to).Replace("{txt}", content);
                return Tools.GetHttpData(url);
            }
            catch (Exception ex)
            {
                Loger.Error(String.Format("{0:yyyy-MM-dd hh:mm:ss}，{1}", DateTime.Now, ex.Message));
                return ex.Message;
            }
        }
    }
}
