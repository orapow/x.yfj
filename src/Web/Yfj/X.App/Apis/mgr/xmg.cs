using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Data;
using X.Web;

namespace X.App.Apis.mgr
{
    public class xmg : xapi
    {
        protected x_mgr mg = null;
        protected long cityid = 0;
        /// <summary>
        /// 功能权限码
        /// #是默认码
        /// 为空说明不需要验证
        /// </summary>
        protected virtual int powercode
        {
            get
            {
                return 3;
            }
        }
        protected override void InitApi()
        {
            base.InitApi();

            var id = GetReqParms("mgr_ad");
            if (string.IsNullOrEmpty("ad")) throw new XExcep("0x0006");

            //mg = DB.x_mgr.FirstOrDefault(o => o.mgr_id == 1);
            mg = CacheHelper.Get<x_mgr>("mgr." + id);
            if (mg == null) throw new XExcep("0x0004");

            if (mg.city == null || mg.city == 0) throw new XExcep("0x0060");
            if (mg.city == 62 && mg.role_id == 3) long.TryParse(GetReqParms("mgr_ct"), out cityid);
            if (cityid == 0) cityid = mg.city.Value;

            ValidPower();
        }

        /// <summary>
        /// 是否有权限
        /// </summary>
        private bool HasPower()
        {
            return mg.role_id < 3 ? mg.role_id == powercode || powercode == 0 : true;
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        private void ValidPower()
        {
            if (!HasPower())
            {
                throw new XExcep("0x0040");
            }
        }
    }
}
