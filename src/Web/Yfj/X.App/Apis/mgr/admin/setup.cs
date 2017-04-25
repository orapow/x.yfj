using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.Core.Utility;
using X.App;
using X.App.Com;
using X.Web.Com;

namespace X.App.Apis.mgr.admin {
    /// <summary>
    /// 常规配置
    /// </summary>
    public class setup : xmg {
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

        public decimal upstair { get; set; }//上楼
        public decimal free_ship { get; set; }//包邮

        public string sms_cfg { get; set; }

        public string wx_appid { get; set; }//
        public string wx_scr { get; set; }//
        public string wx_mch_id { get; set; }//
        /// <summary>
        /// 微信证书路径
        /// </summary>
        public string wx_certpath { get; set; }//
        public string wx_paykey { get; set; }//


        private static Config cfg = null;
   

        protected override Web.Com.XResp Execute() {
            cfg = Config.LoadConfig();
            cfg.domain = domain;
            cfg.name = name;
            cfg.pay_cod = pay_cod;
            cfg.wx_appid = wx_appid;
            cfg.wx_certpath = wx_certpath;
            cfg.wx_mch_id = wx_mch_id;
            cfg.wx_paykey = wx_paykey;
            cfg.wx_scr = wx_scr;
            Config.SaveConfig(cfg);
            return new XResp();
        }
    }
}
