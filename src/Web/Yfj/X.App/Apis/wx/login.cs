using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;

namespace X.App.Apis.wx {
    public class login : _wx {
        public string tel { get; set; }
        public string code { get; set; }
        protected override bool nd_user {
            get {
                return false;
            }
        }
        protected override Web.Com.XResp Execute() {
            var cachecode = CacheHelper.Get<string>("sms.code." + tel);
            if (code.Equals(cachecode)) { }

            return new Web.Com.XResp();
        }
    }
}
