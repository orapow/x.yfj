using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using X.Core.Utility;

namespace X.Wx.Com
{
    class AES
    {
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Input">密文</param>
        /// <param name="EncodingAESKey"></param>
        /// <returns></returns>
        public static string Decrypt(String Input, string EncodingAESKey, ref string appid)
        {
            byte[] Key;
            Key = Convert.FromBase64String(EncodingAESKey + "=");
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            byte[] btmpMsg = AES_decrypt(Input, Iv, Key);

            int len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);


            byte[] bMsg = new byte[len];
            byte[] bAppid = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            Array.Copy(btmpMsg, 20 + len, bAppid, 0, btmpMsg.Length - 20 - len);
            string oriMsg = Encoding.UTF8.GetString(bMsg);
            appid = Encoding.UTF8.GetString(bAppid);

            return oriMsg;
        }
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="EncodingAESKey"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static String Encrypt(String Input, string EncodingAESKey, string appid)
        {
            byte[] Key;
            Key = Convert.FromBase64String(EncodingAESKey + "=");
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            string Randcode = CreateRandCode(16);
            byte[] bRand = Encoding.UTF8.GetBytes(Randcode);
            byte[] bAppid = Encoding.UTF8.GetBytes(appid);
            byte[] btmpMsg = Encoding.UTF8.GetBytes(Input);
            byte[] bMsgLen = BitConverter.GetBytes(HostToNetworkOrder(btmpMsg.Length));
            byte[] bMsg = new byte[bRand.Length + bMsgLen.Length + bAppid.Length + btmpMsg.Length];

            Array.Copy(bRand, bMsg, bRand.Length);
            Array.Copy(bMsgLen, 0, bMsg, bRand.Length, bMsgLen.Length);
            Array.Copy(btmpMsg, 0, bMsg, bRand.Length + bMsgLen.Length, btmpMsg.Length);
            Array.Copy(bAppid, 0, bMsg, bRand.Length + bMsgLen.Length + btmpMsg.Length, bAppid.Length);

            return AES_encrypt(bMsg, Iv, Key);

        }

        #region 私有方法
        static UInt32 HostToNetworkOrder(UInt32 inval)
        {
            UInt32 outval = 0;
            for (int i = 0; i < 4; i++)
                outval = (outval << 8) + ((inval >> (i * 8)) & 255);
            return outval;
        }
        static Int32 HostToNetworkOrder(Int32 inval)
        {
            Int32 outval = 0;
            for (int i = 0; i < 4; i++)
                outval = (outval << 8) + ((inval >> (i * 8)) & 255);
            return outval;
        }
        static string CreateRandCode(int codeLen)
        {
            string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }
            string[] arr = codeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }
        static String AES_encrypt(String Input, byte[] Iv, byte[] Key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = Key;
            aes.IV = Iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }
            String Output = Convert.ToBase64String(xBuff);
            return Output;
        }
        static String AES_encrypt(byte[] Input, byte[] Iv, byte[] Key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            //aes.Padding = PaddingMode.PKCS7;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            aes.Key = Key;
            aes.IV = Iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            #region 自己进行PKCS7补位，用系统自己带的不行
            byte[] msg = new byte[Input.Length + 32 - Input.Length % 32];
            Array.Copy(Input, msg, Input.Length);
            byte[] pad = KCS7Encoder(Input.Length);
            Array.Copy(pad, 0, msg, Input.Length, pad.Length);
            #endregion

            #region 注释的也是一种方法，效果一样
            //ICryptoTransform transform = aes.CreateEncryptor();
            //byte[] xBuff = transform.TransformFinalBlock(msg, 0, msg.Length);
            #endregion

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    cs.Write(msg, 0, msg.Length);
                }
                xBuff = ms.ToArray();
            }

            String Output = Convert.ToBase64String(xBuff);
            return Output;
        }
        static byte[] KCS7Encoder(int text_length)
        {
            int block_size = 32;
            // 计算需要填充的位数
            int amount_to_pad = block_size - (text_length % block_size);
            if (amount_to_pad == 0)
            {
                amount_to_pad = block_size;
            }
            // 获得补位所用的字符
            char pad_chr = chr(amount_to_pad);
            string tmp = "";
            for (int index = 0; index < amount_to_pad; index++)
            {
                tmp += pad_chr;
            }
            return Encoding.UTF8.GetBytes(tmp);
        }
        static char chr(int a)
        {

            byte target = (byte)(a & 0xFF);
            return (char)target;
        }
        static byte[] AES_decrypt(String Input, byte[] Iv, byte[] Key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = Key;
            aes.IV = Iv;
            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(Input);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = decode2(ms.ToArray());
            }
            return xBuff;
        }
        static byte[] decode2(byte[] decrypted)
        {
            int pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
        #endregion
    }

    public class Crypt
    {
        static string otoken = "ub0OXFdDCgldbjT5U2UISsN2yHH7lcD435QBoQYmvC";
        static string msgencrypt = "C2MyJaTTcowZ50JQveT3z8lDXzsdqptphBjhGIomgbu";

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sTimeStamp"></param>
        /// <param name="sNonce"></param>
        /// <param name="sMsgBody"></param>
        /// <param name="sSigture"></param>
        /// <returns></returns>
        static void VerifySignature(string sTimeStamp, string sNonce, string sMsgBody, string sSigture)
        {
            var hash = GenarateSinature(sTimeStamp, sNonce, sMsgBody);
            Debug.WriteLine("->" + sTimeStamp);
            Debug.WriteLine("->" + sNonce);
            Debug.WriteLine("->" + sMsgBody);
            Debug.WriteLine(hash + "<->" + sSigture);
            if (hash != sSigture)
            {
                throw new WxExcep("签名验证错误。");
            }
        }
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="msgencrypt"></param>
        /// <param name="msgsignature"></param>
        /// <returns></returns>
        static string GenarateSinature(string timestamp, string nonce, string msgencrypt)
        {
            var ps = new List<string>();
            ps.Add(otoken);
            ps.Add(timestamp);
            ps.Add(nonce);
            ps.Add(msgencrypt);

            ps.Sort((x, y) => { return x.CompareTo(y); });

            string raw = "";
            for (int i = 0; i < ps.Count; ++i) { raw += ps[i]; }
            //Debug.WriteLine("->" + raw);

            //SHA1 sha;
            //ASCIIEncoding enc;
            //string hash = "";
            //try
            //{
            //    sha = new SHA1CryptoServiceProvider();
            //    enc = new ASCIIEncoding();
            //    byte[] dataToHash = enc.GetBytes(raw);
            //    byte[] dataHashed = sha.ComputeHash(dataToHash);
            //    hash = BitConverter.ToString(dataHashed).Replace("-", "");
            //    hash = hash.ToLower();
            //}
            //catch
            //{
            //    throw new WxExcep("SHA加密生成签名失败。");
            //}

            return Secret.SHA1(raw).ToLower();
        }

        /// <summary>
        /// 检验消息的真实性，并且获取解密后的明文
        /// </summary>
        /// <param name="sMsgSignature">签名串，对应URL参数的msg_signature</param>
        /// <param name="sTimeStamp">时间戳，对应URL参数的timestamp</param>
        /// <param name="sNonce">随机串，对应URL参数的nonce</param>
        /// <param name="sPostData">密文，对应POST请求的数据</param>
        /// <param name="sMsg">解密后的原文，当return返回0时有效</param>
        /// <returns>成功0，失败返回对应的错误码</returns>
        public static string DecryptMsg(string appid, string sMsgSignature, string sTimeStamp, string sNonce, string sPostData)
        {
            if (msgencrypt.Length != 43) throw new WxExcep("AESKey不正确。");

            XmlDocument doc = new XmlDocument();
            XmlNode root;
            string sEncryptMsg;
            try
            {
                doc.LoadXml(sPostData);
                root = doc.FirstChild;
                sEncryptMsg = root["Encrypt"].InnerText;
            }
            catch (Exception)
            {
                throw new WxExcep("XML解析失败");
            }

            VerifySignature(sTimeStamp, sNonce, sEncryptMsg, sMsgSignature);

            string cpid = "";
            var sMsg = "";
            try
            {
                sMsg = AES.Decrypt(sEncryptMsg, msgencrypt, ref cpid);
            }
            catch (FormatException)
            {
                throw new WxExcep("BASE64解密异常");
            }
            catch (Exception)
            {
                throw new WxExcep("AES 解密失败");
            }

            if (cpid != appid) throw new WxExcep("APPID 校验错误");

            return sMsg;
        }

        /// <summary>
        /// 将企业号回复用户的消息加密打包
        /// </summary>
        /// <param name="sReplyMsg">企业号待回复用户的消息，xml格式的字符串</param>
        /// <param name="sTimeStamp">时间戳，可以自己生成，也可以用URL参数的timestamp</param>
        /// <param name="sNonce">随机串，可以自己生成，也可以用URL参数的nonce</param>
        /// <param name="sEncryptMsg">加密后的可以直接回复用户的密文，包括msg_signature, timestamp, nonce, encrypt的xml格式的字符串</param>
        /// <returns>
        /// 成功0，失败返回对应的错误码
        /// </returns>
        public static string EncryptMsg(string appid, string sReplyMsg, string sTimeStamp, string sNonce)
        {
            if (msgencrypt.Length != 43) throw new WxExcep("AESKey不正确。");

            string raw = "";
            try
            {
                raw = AES.Encrypt(sReplyMsg, msgencrypt, appid);
            }
            catch (Exception)
            {
                throw new WxExcep("AES 加密失败");
            }

            string MsgSigature = GenarateSinature(sTimeStamp, sNonce, raw);

            var sEncryptMsg = "";
            string EncryptLabelHead = "<Encrypt><![CDATA[";
            string EncryptLabelTail = "]]></Encrypt>";
            string MsgSigLabelHead = "<MsgSignature><![CDATA[";
            string MsgSigLabelTail = "]]></MsgSignature>";
            string TimeStampLabelHead = "<TimeStamp><![CDATA[";
            string TimeStampLabelTail = "]]></TimeStamp>";
            string NonceLabelHead = "<Nonce><![CDATA[";
            string NonceLabelTail = "]]></Nonce>";

            sEncryptMsg = sEncryptMsg + "<xml>" + EncryptLabelHead + raw + EncryptLabelTail;
            sEncryptMsg = sEncryptMsg + MsgSigLabelHead + MsgSigature + MsgSigLabelTail;
            sEncryptMsg = sEncryptMsg + TimeStampLabelHead + sTimeStamp + TimeStampLabelTail;
            sEncryptMsg = sEncryptMsg + NonceLabelHead + sNonce + NonceLabelTail;
            sEncryptMsg += "</xml>";

            return sEncryptMsg;

        }
    }
}
