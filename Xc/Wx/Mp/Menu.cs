using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Wx.Mp.Com;

namespace X.Wx.Mp
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Memu
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="btns"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static void Create(string json, string access_token)
        {
            Api.PostData("menu/create?access_token=" + access_token, json);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="access_token"></param>
        public static void Delete(string access_token)
        {
            Api.GetData("menu/delete?access_token=" + access_token);
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string Get(string access_token)
        {
            return Api.GetData("menu/get?access_token=" + access_token);
        }
        /// <summary>
        /// 按钮对象
        /// </summary>
        public class Button
        {
            public string type { get; set; }
            public string name { get; set; }
            public string key { get; set; }
            public string url { get; set; }
            public List<Button> sub_button { get; set; }
        }
    }
}
