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
        protected long cityid = 0;
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

            var key = GetReqParms("mgr_ad");// Context.Request.Cookies["ad"];
            if (string.IsNullOrEmpty(key)) throw new XExcep("0x0004");

            //mg = DB.x_mgr.FirstOrDefault(o => o.mgr_id == 1);
            mg = CacheHelper.Get<x_mgr>("mgr." + key); //CacheHelper.Get<x_mgr>("mgr." + id);
            if (mg == null) throw new XExcep("0x0004");

            if (mg.city == null || mg.city == 0) throw new XExcep("0x0060");
            if (mg.city == 62 && mg.role_id == 3) long.TryParse(GetReqParms("mgr_ct"), out cityid);
            if (cityid == 0) cityid = mg.city.Value;

            dict.Add("cityid", cityid);
            var dt = DB.x_dict.FirstOrDefault(o => o.value == cityid + "" && o.code == "sys.city");
            dict.Add("cityname", dt.name);

            ValidPower();

            Context.Response.SetCookie(new System.Web.HttpCookie("mgr_ct", cityid + ""));

            CacheHelper.Save("mgr." + key, mg, 60 * 60);
            dict.Add("mg", mg);
        }
    }
}
