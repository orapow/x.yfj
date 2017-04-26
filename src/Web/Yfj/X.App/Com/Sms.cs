using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Sms.Model.V20160927;
using Aliyun.MNS;
using Aliyun.MNS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using X.Core.Utility;

namespace X.App.Com {
    public class Sms {
        public static bool SendCode(string to, string code, String key, String sign, string tpid) {
            var dc = new Dictionary<string, string>();
            dc.Add("Format", "JSON");
            dc.Add("Version", "2016-09-27");
            dc.Add("SignatureMethod", "HMAC-SHA1");
            dc.Add("SignatureNonce", Guid.NewGuid().ToString());
            dc.Add("SignatureVersion", "1.0");

            dc.Add("AccessKeyId", key);
            //dc.Add("Signature", sign);

            dc.Add("Timestamp", DateTime.Now.AddHours(-8).ToString("yyyy-MM-ddTHH:mm:ssZ"));
            //新增AccessKeyId
            //dc.Add("AccessKeyId", key);
            dc.Add("Action", "SingleSendSms");
            dc.Add("SignName", sign);
            dc.Add("TemplateCode", tpid);
            dc.Add("RecNum", to);
            dc.Add("ParamString", "{\"code\":\"" + code + "\"}");
            dc.Add("Signature", getSign(dc));

            var url = new StringBuilder("https://sms.aliyuncs.com/?");
            foreach (var d in dc)
                url.Append(d.Key + "=" + d.Value + "&");
            var rsp = Tools.GetHttpData(url.ToString());

            return true;
        }

        public static Boolean sendSmsViaSDK(String signName, String accessKey, String accessSecret, String smsTemplate, String recNum, String param) {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKey, accessSecret);
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendSmsRequest request = new SingleSendSmsRequest();

            request.SignName = signName;//"管理控制台中配置的短信签名（状态必须是验证通过）";
            request.TemplateCode = smsTemplate;//"管理控制台中配置的审核通过的短信模板的模板CODE（状态必须是验证通过）";
            request.RecNum = recNum; //"接收号码，多个号码可以逗号分隔";
            request.ParamString = "{\"code\":\"" + param + "\"}";// "短信模板中的变量；数字需要转换为字符串；个人用户每个变量长度必须小于15个字符。";
            SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
            return true;
        }

        public static Boolean sendSmsViaMnsSDK(String signName, String accessKey, String accessSecret, String smsTemplate, String recNum, String code,String endpoint,String topicName) {
            try {

                IMNS client = new Aliyun.MNS.MNSClient(accessKey, accessSecret, endpoint);
                /**
                 * Step 2. 获取主题引用
                 */
                Topic topic = client.GetNativeTopic(topicName);
                /**
                 * Step 3. 生成SMS消息属性
                 */
                MessageAttributes messageAttributes = new MessageAttributes();
                BatchSmsAttributes batchSmsAttributes = new BatchSmsAttributes();
                // 3.1 设置发送短信的签名：SMSSignName
                batchSmsAttributes.FreeSignName = signName;
                // 3.2 设置发送短信的模板SMSTemplateCode
                batchSmsAttributes.TemplateCode = smsTemplate;
                Dictionary<string, string> param = new Dictionary<string, string>();
                // 3.3 （如果短信模板中定义了参数）设置短信模板中的参数，发送短信时，会进行替换
                param.Add("code", code);
                // 3.4 设置短信接收者手机号码
                batchSmsAttributes.AddReceiver(recNum, param);
                //batchSmsAttributes.AddReceiver("$YourReceiverPhoneNumber2", param);
                messageAttributes.BatchSmsAttributes = batchSmsAttributes;
                PublishMessageRequest request = new PublishMessageRequest();
                request.MessageAttributes = messageAttributes;
                /**
                 * Step 4. 设置SMS消息体（必须）
                 *
                 * 注：目前暂时不支持消息内容为空，需要指定消息内容，不为空即可。
                 */
                request.MessageBody = "smsmessage";
                PublishMessageResponse resp = topic.PublishMessage(request);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        static string getSign(Dictionary<string, string> ps) {
            var parms = ps.OrderBy(o => o.Key);
            var sb_str = new StringBuilder("GET&%2F&");
            foreach (var p in parms) {
                if (p.Key != "AccessKeyId")
                    sb_str.Append("%26");
                sb_str.Append(HttpUtility.UrlEncode(p.Key) + "%3D" + UrlEncode(UrlEncode(p.Value)));
            }
            return sha1(sb_str.ToString());
        }
        static string UrlEncode(string value) {
            StringBuilder result = new StringBuilder();
            string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            foreach (char symbol in value) {
                if (unreservedChars.IndexOf(symbol) != -1)
                    result.Append(symbol);
                else
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
            }
            return result.ToString();
        }
        static string sha1(string str) {
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes("S0DiD9VFx5gC4fhnCKWy3wMONy3J7z&");
            byte[] dataBuffer = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }

    }
}
