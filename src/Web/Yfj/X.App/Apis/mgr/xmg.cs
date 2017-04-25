using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Data;
using X.Web;

namespace X.App.Apis.mgr {
    public class xmg : xapi {
        protected x_mgr mg = null;
        /// <summary>
        /// 功能权限码
        /// #是默认码
        /// 为空说明不需要验证
        /// </summary>
        protected virtual int powercode {
            get {
                return 3;
            }
        }
        protected override void InitApi() {
            base.InitApi();

            //var key = GetReqParms("mg_keys");
            //if (string.IsNullOrEmpty(key)) throw new XExcep("0x0006");

            mg = DB.x_mgr.FirstOrDefault(o => o.mgr_id == 1);// CacheHelper.Get<x_mgr>("mgr." + key);
            if (mg == null) throw new XExcep("0x0006");

            //CacheHelper.Save("mgr." + id, mg, 60 * 60);

            ValidPower();
        }

        /// <summary>
        /// 是否有权限
        /// </summary>
        private bool HasPower() {
            return mg.role_id < 3 ? mg.role_id == powercode : true;
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        private void ValidPower() {
            if (!HasPower()) {
                throw new XExcep("T当前用户没有权限");
            }
        }
    }
}
