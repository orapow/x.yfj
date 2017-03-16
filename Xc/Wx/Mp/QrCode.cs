using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.Wx.Mp
{
    /// <summary>
    ///  二维码
    /// </summary>
    public class Qrcode
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="info">
        /// 内容
        /// </param>
        /// <param name="id">
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
        /// </param>
        /// <param name="sec">
        /// 该二维码有效时间，以秒为单位。 最大不超过604800（即7天），为0时为永久二维码
        /// </param>
        /// <param name="src">
        /// 场景值ID（字符串形式的ID），字符串类型，长度限制为1到64，仅永久二维码支持此字段
        /// </param>
        /// <returns></returns>
        public static string Create(string info, int id, int sec, int src)
        {
            return "";
        }
        /// <summary>
        /// 获取二维码地址
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static string Load(string ticket)
        {
            return api_url += "showqrcode?ticket=" + ticket;
        }
    }
}
