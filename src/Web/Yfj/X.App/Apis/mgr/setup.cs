using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.Core.Utility;
using X.App;
using X.App.Com;
using X.Web.Com;
using X.Web;

namespace X.App.Apis.mgr
{
    /// <summary>
    /// 常规配置
    /// </summary>
    public class setup : xmg
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string domain { get; set; }//


        /// <summary>
        /// 系统名称
        /// </summary>
        public string name { get; set; }//网站名
        public string tel { get; set; }
        /// <summary>
        /// 缓存设置
        /// 1、memcached
        /// 2、WebCached
        /// </summary>
        public int cache { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string pay_ways { get; set; }//
        public int chg_audit { get; set; }

        public int credit { get; set; }//会员设置积分价值
        public int max_deposit { get; set; }//最大充值金额

        public int min_deposit { get; set; }//最少充值金额
        public int shipfee { get; set; }//每单邮费
        public int free_ship { get; set; }//包邮

        public string wx_appid { get; set; }//
        public string wx_scr { get; set; }//
        public string wx_mch_id { get; set; }//
        /// <summary>
        /// 微信证书路径
        /// </summary>
        public string wx_certpath { get; set; }//
        public string wx_paykey { get; set; }//


        protected override Web.Com.XResp Execute()
        {
            if (max_deposit < min_deposit) throw new XExcep("0x0036");
            cfg = Config.LoadConfig();
            cfg.domain = domain;
            cfg.name = name;

            cfg.chg_audit = chg_audit;
            cfg.svr_tel = tel;
            cfg.pay_ways = pay_ways;
            cfg.credit = credit;
            cfg.min_deposit = min_deposit;
            cfg.max_deposit = max_deposit;


            cfg.wx_appid = wx_appid;
            cfg.wx_certpath = wx_certpath;
            cfg.wx_mch_id = wx_mch_id;
            cfg.wx_paykey = wx_paykey;
            cfg.wx_scr = wx_scr;

            cfg.shipfee = shipfee;
            cfg.free_ship = free_ship;
            Config.SaveConfig(cfg);
            return new XResp();
        }
    }
}
