using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using X.Core.Cache;
using X.Core.Utility;
using X.Web;

namespace X.App.Apis.wx {
    public class logout : _wx {
        
        
        protected override Web.Com.XResp Execute() {
            cu = null;
            deleteCookeis();
            ClearCache();//保存cookie状态
            return new Web.Com.XResp();
        }

        ///<summary>
        /// 删除cookies
        ///</summary>
        public void deleteCookeis() {
            foreach (string cookiename in HttpContext.Current.Request.Cookies.AllKeys) {
                HttpCookie cookies = HttpContext.Current.Request.Cookies[cookiename];
                if (cookies != null) {
                    cookies.Expires = DateTime.Today.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookies);
                    HttpContext.Current.Request.Cookies.Remove(cookiename);
                }
            }

        }


        /// <summary>
        /// 清空所有的Cache
        /// </summary>
        private static void ClearCache() {
            List<string> cacheKeys = new List<string>();
            IDictionaryEnumerator cacheEnum = HttpContext.Current.Cache.GetEnumerator();
            while (cacheEnum.MoveNext()) {
                cacheKeys.Add(cacheEnum.Key.ToString());
            }
            foreach (string cacheKey in cacheKeys) {
                HttpContext.Current.Cache.Remove(cacheKey);
            }
        }
    }
}
