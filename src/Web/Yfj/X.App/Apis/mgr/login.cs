﻿using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Core.Plugin;
using X.Core.Utility;
using X.Web;
using X.Web.Apis;
using X.Web.Com;

namespace X.App.Apis.mgr
{
    public class login : xapi
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }

        protected override XResp Execute()
        {
            var c = CacheHelper.Get<string>("img.code." + uid);
            CacheHelper.Remove("img.code." + uid);
            if (c == null || c != code) throw new XExcep("0x0022");
            var ad = DB.x_mgr.SingleOrDefault(o => o.uid == uid);

            if (ad == null || ad.pwd != Secret.MD5(pwd)) throw new XExcep("0x0023");
            ad.ukey = Guid.NewGuid().ToString();

            CacheHelper.Save("mgr." + ad.ukey, ad, 60 * 20);

            SubmitDBChanges();
            return new XResp() { msg = ad.ukey };
        }
    }
}
