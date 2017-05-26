using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.user
{
    /// <summary>
    /// 用户管理列表
    /// </summary>
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public int city { get; set; }
        protected override int powercode
        {
            get
            {
                return 1;
            }
        }

        protected override XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from u in DB.x_user
                    where u.city == cityid
                    select u;

            //if (city > 0) q = q.Where(o => o.city == (mg.x_role.power == "###" ? city : mg.city));
            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key) || o.tel.Contains(key) || o.nickname.Contains(key));

            var list = q.OrderByDescending(o => o.ctime).Skip((page - 1) * limit).Take(limit).ToList();

            r.items = list.Select(u => new
            {
                u.id,
                name = string.IsNullOrEmpty(u.name) ? u.name : u.name + "(" + u.sex + ")",
                u.tel,
                u.nickname,
                city = GetDictName("sys.city", u.city),
                u.headimg,
                level = GetDictName("user.level", u.@group),
                exp = u.exp + (u.used_exp > 0 ? "(" + u.used_exp + ")" : ""),
                u.balance,
                ctime = u.ctime.Value.ToString("yyyy-MM-dd HH:mm"),
                etime = u.etime.Value.ToString("yyyy-MM-dd HH:mm")
            }).ToList();
            r.count = q.Count();

            return r;
        }

    }
}
