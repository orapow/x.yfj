using System;
using System.Collections.Generic;
using System.Linq;
using X.App.Com;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;
using X.Web.Views;

namespace X.App.Views.mgr
{
    public class xmg : xview
    {
        protected x_mgr mg = null;

        /// <summary>
        /// 0、无权限
        /// 1、客服
        /// 2、财务
        /// 3、管理员
        /// </summary>
        protected virtual int powercode
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool HasPower()
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

        protected override void InitDict()
        {
            base.InitDict();

            var id = GetReqParms("mgr_ad");// Context.Request.Cookies["ad"];
            if (string.IsNullOrEmpty("ad")) throw new XExcep("0x0006");

            //mg = DB.x_mgr.FirstOrDefault(o => o.mgr_id == 1);
            mg = CacheHelper.Get<x_mgr>("mgr." + id); //CacheHelper.Get<x_mgr>("mgr." + id);
            if (mg == null) throw new XExcep("0x0004");
            var dt = DB.x_dict.FirstOrDefault(o => o.value == mg.city + "" && o.code == "sys.city");

            dict.Add("isbase", dt.f1);
            dict.Add("cname", dt.name);

            ValidPower();

            CacheHelper.Save("mgr." + id, mg, 60 * 60);
            dict.Add("mg", mg);
        }
    }
}
