using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using X.Core.Utility;

namespace X.Wx.Mp
{
    /// <summary>
    /// 支付
    /// </summary>
    public class Pay
    {
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="body">商品介绍</param>
        /// <param name="order_no">订单号</param>
        /// <param name="total">总金额</param>
        /// <param name="notify_url">回调url</param>
        /// <returns></returns>
        public static Oxml CreateOrder(string body, string order_no, string total, string notify_url, string openid)
        {
            var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            var ps = new Dictionary<string, string>();
            ps.Add("appid", "");
            ps.Add("mch_id", "");
            ps.Add("nonce_str", Tools.GetRandRom(24, 3));
            ps.Add("body", body);
            ps.Add("out_trade_no", order_no);
            ps.Add("total_fee", total);
            ps.Add("spbill_create_ip", Tools.GetClientIP());
            ps.Add("notify_url", notify_url);
            ps.Add("trade_type", "JSAPI");
            ps.Add("openid", openid);
            var topostxml = "";//ToXml(ps);

            var odxml = Tools.PostHttpData(url, topostxml);
            return Serialize.FormXml<Oxml>(odxml);
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="order_no">用户订单号</param>
        /// <returns></returns>
        public static Clxml CloseOrder(string order_no)
        {
            var url = "https://api.mch.weixin.qq.com/pay/closeorder";

            var ps = new Dictionary<string, string>();
            ps.Add("out_trade_no", order_no);
            ps.Add("appid", "");
            ps.Add("mch_id", "");
            ps.Add("nonce_str", Tools.GetRandRom(24, 3));
            var toclxml = "";//ToXml(ps);

            var clxml = Tools.PostHttpData(url, toclxml);
            return Serialize.FormXml<Clxml>(clxml);
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="order_no">订单号</param>
        /// <param name="refund_no">退款单号</param>
        /// <param name="total_fee">总金额（分）</param>
        /// <param name="refund_fee">退款金额（分）</param>
        /// <returns></returns>
        public static Ruxml Refund(string order_no, string refund_no, string total_fee, string refund_fee)
        {
            var url = "https://api.mch.weixin.qq.com/secapi/pay/refund";

            var ps = new Dictionary<string, string>();
            ps.Add("appid", "");
            ps.Add("mch_id", "");
            ps.Add("nonce_str", Tools.GetRandRom(24, 3));
            ps.Add("out_trade_no", order_no);
            ps.Add("out_refund_no", refund_no);
            ps.Add("total_fee", total_fee);
            ps.Add("refund_fee", refund_fee);
            ps.Add("op_user_id", "");
            var xml = "";//ToXml(ps);

            var rfxml = Tools.PostHttpData(url, xml, "POST", "", "");
            return Serialize.FormXml<Ruxml>(rfxml);
        }

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="mchid"></param>
        /// <param name="desc"></param>
        /// <param name="openid"></param>
        /// <param name="no"></param>
        /// <param name="amount"></param>
        /// <param name="cert_path"></param>
        /// <param name="signkey"></param>
        /// <returns></returns>
        public static Payxml PayToOpenid(string appid, string mchid, string desc, string openid, string no, int amount, string cert_path, string signkey)
        {
            var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
            var dict = new Dictionary<string, string>();
            dict.Add("mch_appid", appid);
            dict.Add("mchid", mchid);
            dict.Add("nonce_str", Tools.GetRandRom(32, 3));
            dict.Add("partner_trade_no", no + "");
            dict.Add("openid", openid);
            dict.Add("check_name", "NO_CHECK");
            dict.Add("amount", amount * 100 + "");
            dict.Add("desc", desc);
            dict.Add("spbill_create_ip", Tools.GetClientIP());
            var to_md5 = "";
            var xml_data = "<xml>";
            foreach (var d in dict.OrderBy(o => o.Key))
            {
                if (!string.IsNullOrEmpty(d.Value)) to_md5 += d.Key + "=" + d.Value + "&";
                xml_data += "<" + d.Key + ">" + d.Value + "</" + d.Key + ">";
            }
            xml_data += "<sign>" + Secret.MD5(to_md5 + "key=" + signkey, 0).ToUpper() + "</sign>";
            xml_data += "</xml>";
            var xml = Tools.PostHttpData(url, xml_data, "POST", cert_path + "apiclient_cert.p12", mchid);
            return Serialize.FormXml<Payxml>(xml);
        }

        /// <summary>
        /// 验证支付回调
        /// </summary>
        /// <param name="nt">
        /// 反回参数实体
        /// 验证失败为null
        /// </param>
        /// <returns></returns>
        public static Ntxml ValidNotify(string xml)
        {
            var nt = Serialize.FormXml<Ntxml>(xml);
            if (nt == null || string.IsNullOrEmpty(nt.sign)) return null;
            //if (nt.mch_id != wxcfg.mchid || nt.appid != wxcfg.appid) return null;
            var ps = new Dictionary<string, string>();
            ps.Add("appid", nt.appid);
            ps.Add("bank_type", nt.bank_type);
            ps.Add("cash_fee", nt.cash_fee);
            ps.Add("fee_type", nt.fee_type);
            ps.Add("is_subscribe", nt.is_subscribe);
            ps.Add("mch_id", nt.mch_id);
            ps.Add("nonce_str", nt.nonce_str);
            ps.Add("openid", nt.openid);
            ps.Add("out_trade_no", nt.out_trade_no);
            ps.Add("result_code", nt.result_code);
            ps.Add("return_code", nt.return_code);
            ps.Add("time_end", nt.time_end);
            ps.Add("total_fee", nt.total_fee);
            ps.Add("trade_type", nt.trade_type);
            ps.Add("transaction_id", nt.transaction_id);
            return (nt.sign.ToLower() == "") ? nt : null;
        }

        /// <summary>
        /// 微信Xml
        /// </summary>
        public class xml
        {
            /// <summary>
            /// SUCCESS/FAIL
            /// 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
            /// </summary>
            public string return_code { get; set; }
            /// <summary>
            /// 返回信息，如非空，为错误原因
            /// 签名失败
            /// 参数格式校验错误
            /// </summary>
            public string return_msg { get; set; }
        }

        /// <summary>
        /// 下单XML
        /// </summary>
        [XmlType("xml")]
        public class Oxml : xml
        {
            /// <summary>
            /// 调用接口提交的公众账号ID
            /// </summary>
            public string appid { get; set; }
            /// <summary>
            /// 调用接口提交的商户号
            /// </summary>
            public string mch_id { get; set; }
            /// <summary>
            /// 调用接口提交的终端设备号，
            /// </summary>
            public string device_info { get; set; }
            /// <summary>
            /// 微信返回的随机字符串
            /// </summary>
            public string nonce_str { get; set; }
            /// <summary>
            /// 微信返回的签名，详见签名算法
            /// </summary>
            public string sign { get; set; }
            /// <summary>
            /// SUCCESS	SUCCESS/FAIL
            /// </summary>
            public string result_code { get; set; }
            /// <summary>
            /// 详细参见第6节错误列表
            /// </summary>
            public string err_code { get; set; }
            /// <summary>
            /// 错误返回的信息描述
            /// </summary>
            public string err_code_des { get; set; }
            /// <summary>
            /// 调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，详细说明见参数规定
            /// </summary>
            public string trade_type { get; set; }
            /// <summary>
            /// 微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
            /// </summary>
            public string prepay_id { get; set; }
            /// <summary>
            /// trade_type为NATIVE是有返回，可将该参数值生成二维码展示出来进行扫码支付
            /// </summary>
            public string code_url { get; set; }
        }

        /// <summary>
        /// 退款XML
        /// </summary>
        [XmlType("xml")]
        public class Ruxml : xml
        {
            /// <summary>
            /// SUCCESS退款申请接收成功，结果通过退款查询接口查询
            /// FAIL 提交业务失败
            /// </summary>
            public string result_code { get; set; }
            /// <summary>
            /// 错误代码
            /// </summary>
            public string err_code { get; set; }
            /// <summary>
            /// 结果信息描述
            /// </summary>
            public string err_code_des { get; set; }
            /// <summary>
            /// 微信分配的公众账号ID
            /// </summary>
            public string appid { get; set; }
            /// <summary>
            /// 微信支付分配的商户号
            /// </summary>
            public string mch_id { get; set; }
            /// <summary>
            /// 微信支付分配的终端设备号，与下单一致
            /// </summary>
            public string device_info { get; set; }
            /// <summary>
            /// 随机字符串，不长于32位
            /// </summary>
            public string nonce_str { get; set; }
            /// <summary>
            /// 签名，详见签名算法
            /// </summary>
            public string sign { get; set; }
            /// <summary>
            /// 微信订单号
            /// </summary>
            public string transaction_id { get; set; }
            /// <summary>
            /// 商户系统内部的订单号
            /// </summary>
            public string out_trade_no { get; set; }
            /// <summary>
            /// 商户退款单号
            /// </summary>
            public string out_refund_no { get; set; }
            /// <summary>
            /// 微信退款单号
            /// </summary>
            public string refund_id { get; set; }
            /// <summary>
            /// ORIGINAL—原路退款
            /// BALANCE—退回到余额
            /// </summary>
            public string refund_channel { get; set; }
            /// <summary>
            /// 退款总金额,单位为分,可以做部分退款
            /// </summary>
            public string refund_fee { get; set; }
            /// <summary>
            /// 订单总金额，单位为分，只能为整数，详见支付金额
            /// </summary>
            public string total_fee { get; set; }
            /// <summary>
            /// CNY	订单金额货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
            /// </summary>
            public string fee_type { get; set; }
            /// <summary>
            /// 现金支付金额，单位为分，只能为整数，详见支付金额
            /// </summary>
            public string cash_fee { get; set; }
            /// <summary>
            /// 现金退款金额，单位为分，只能为整数，详见支付金额
            /// </summary>
            public string cash_refund_fee { get; set; }
            /// <summary>
            /// 代金券或立减优惠退款金额=订单金额-现金退款金额，注意：立减优惠金额不会退回
            /// </summary>
            public string coupon_refund_fee { get; set; }
            /// <summary>
            /// 代金券或立减优惠使用数量
            /// </summary>
            public string coupon_refund_count { get; set; }
            /// <summary>
            /// SUCCESS退款申请接收成功，结果通过退款查询接口查询
            /// FAIL 提交业务失败
            /// </summary>
            public string coupon_refund_id { get; set; }
        }

        /// <summary>
        /// 闭关订单XML
        /// </summary>
        [XmlType("xml")]
        public class Clxml : xml
        {
            /// <summary>
            /// 微信分配的公众账号ID
            /// </summary>
            public string appid { get; set; }
            /// <summary>
            /// 微信支付分配的商户号
            /// </summary>
            public string mch_id { get; set; }
            /// <summary>
            /// 随机字符串，不长于32位
            /// </summary>
            public string nonce_str { get; set; }
            /// <summary>
            /// 签名，验证签名算
            /// </summary>
            public string sign { get; set; }
            /// <summary>
            /// 详细参见第6节错误列表
            /// </summary>
            public string err_code { get; set; }
            /// <summary>
            /// 结果信息描述
            /// </summary>
            public string err_code_des { get; set; }
        }

        /// <summary>
        /// 支付回调XML
        /// </summary>
        [XmlType("xml")]
        public class Ntxml : xml
        {
            public string appid { get; set; }
            public string bank_type { get; set; }
            public string cash_fee { get; set; }
            public string fee_type { get; set; }
            public string is_subscribe { get; set; }
            public string mch_id { get; set; }
            public string nonce_str { get; set; }
            public string openid { get; set; }
            public string out_trade_no { get; set; }
            public string result_code { get; set; }
            public string sign { get; set; }
            public string time_end { get; set; }
            public string total_fee { get; set; }
            public string trade_type { get; set; }
            public string transaction_id { get; set; }
        }

        [XmlType("xml")]
        public class Payxml : xml
        {
            /// <summary>
            /// 微信分配的公众账号ID（企业号corpid即为此appId）
            /// </summary>
            public string mch_appid { get; set; }
            /// <summary>
            /// 微信支付分配的商户号
            /// </summary>
            public string mchid { get; set; }
            /// <summary>
            /// 微信支付分配的终端设备号
            /// </summary>
            public string device_info { get; set; }
            /// <summary>
            /// 随机字符串，不长于32位
            /// </summary>
            public string nonce_str { get; set; }
            /// <summary>
            /// SUCCESS/FAIL
            /// </summary>
            public string result_code { get; set; }
            /// <summary>
            /// 错误码信息
            /// </summary>
            public string err_code { get; set; }
            /// <summary>
            /// 结果信息描述
            /// </summary>
            public string err_code_des { get; set; }
            /// <summary>
            /// 商户订单号
            /// 我们使用返现卡卡号
            /// </summary>
            public string partner_trade_no { get; set; }
            /// <summary>
            /// 微信订单号
            /// </summary>
            public string payment_no { get; set; }
            /// <summary>
            /// 微信支付成功时间
            /// </summary>
            public string payment_time { get; set; }
        }
    }
}
