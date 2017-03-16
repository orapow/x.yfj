using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;

namespace X.App.Views.user
{
    public class _u : xview
    {
        protected virtual bool needuser { get { return true; } }

        protected override void InitView()
        {
            base.InitView();

            if (cu == null && needuser) throw new XExcep("T用户未登陆或登陆超时");
            CacheHelper.Save("u.cu", cu);

        }
    }
}
