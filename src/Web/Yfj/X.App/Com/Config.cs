using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.Core.Utility;

namespace X.App.Com
{
    /// <summary>
    /// 常规配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string domain { get; set; }//


        /// <summary>
        /// 系统名称
        /// </summary>
        public string name { get; set; }//网站名


        /// <summary>
        /// 百度key
        /// </summary>
        public string bdkey { get; set; }
        /// <summary>
        /// 缓存设置
        /// 1、memcached
        /// 2、WebCached
        /// </summary>
        public int cache { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 货到付款
        /// </summary>
        public int pay_cod { get; set; }//

        public string sms_cfg { get; set; }

        public string wx_appid { get; set; }//
        public string wx_scr { get; set; }//
        public string wx_mch_id { get; set; }//
        /// <summary>
        /// 微信证书路径
        /// </summary>
        public string wx_certpath { get; set; }//
        public string wx_paykey { get; set; }//

        private static string file = HttpContext.Current.Server.MapPath("/dat/cfg.x");//来自服务器的文件
        private static Config cfg = null;
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public static Config LoadConfig()
        {
            if (cfg == null)
            {
                var json = Tools.ReadFile(file);
                if (string.IsNullOrEmpty(json)) return new Config();
                cfg = Serialize.FromJson<Config>(json);
            }
            return cfg;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="cfg"></param>
        public static void SaveConfig(Config cfg)
        {
            Tools.SaveFile(HttpContext.Current.Server.MapPath("/dat/cfg.x"), Serialize.ToJson(cfg));
        }

        public int max_deposit { get; set; }

        public int min_deposit { get; set; }

        public int credit { get; set; }

        public decimal free_ship { get; set; }

        public decimal shipfee { get; set; }
    }
}
