using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using X.App.Com;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;

namespace X.App.Views.sder
{
    public class _sd : xview
    {
        protected string opid { get; set; }
        protected bool isWx { get; set; }
        public x_dict sd { get; set; }
        private void initUser()
        {
            if (sd != null) return;

            var code = GetReqParms("code");
            if (string.IsNullOrEmpty(code)) toWxUrl("snsapi_base");

            if (!string.IsNullOrEmpty(code))
            {
                var tk = Wx.GetWebToken(cfg.wx_appid, cfg.wx_scr, code);
                if (!string.IsNullOrEmpty(tk.errcode)) toWxUrl("snsapi_base");
                opid = tk.openid;
            }

        }
        private void toWxUrl(string scope)
        {
            var url = Context.Request.RawUrl.Split('?')[0];
            Context.Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + cfg.wx_appid + "&redirect_uri=" + Context.Server.UrlEncode("http://" + cfg.domain + url) + "&response_type=code&scope=" + scope + "&state=" + Tools.GetRandRom(6, 3) + "#wechat_redirect");
            Context.Response.End();
        }
        private void initWx()
        {
            var ts = Tools.GetGreenTime("");
            var no = Tools.GetRandRom(6);

            dict.Add("wx_appid", cfg.wx_appid);
            dict.Add("wx_ts", ts);
            dict.Add("wx_no", no);

            var tk = Wx.GetToken(cfg.wx_appid, cfg.wx_scr);
            var tick = Wx.GetJsTicket(tk);
            if (string.IsNullOrEmpty(tk)) tick = Wx.GetJsTicket(Wx.GetToken(cfg.wx_appid, cfg.wx_scr, true), true);

            var dt = new List<string>();
            dt.Add("noncestr=" + no);
            dt.Add("jsapi_ticket=" + tick);
            dt.Add("timestamp=" + ts);
            dt.Add("url=http://" + cfg.domain + Context.Request.RawUrl);
            dt.Sort();

            var sign = Secret.SHA1(string.Join("&", dt));

            dict.Add("wx_sign", sign.ToLower());
        }

        protected override void InitView()
        {
            base.InitView();

            opid = GetReqParms("sd_key");
            if (string.IsNullOrEmpty(opid)) initUser();

            sd = DB.x_dict.FirstOrDefault(o => o.code == "user.sender" && o.jp == opid);
            if (sd == null && !Context.Request.RawUrl.Contains("/sder/bind.html")) Context.Response.Redirect("/sder/bind.html");

            Context.Response.SetCookie(new HttpCookie("sd_key", opid));

            isWx = Context.Request.UserAgent.Contains("MicroMessenger");

            if (isWx) initWx();
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("iswx", isWx);
            if (sd != null) dict.Add("sd", sd);
        }
    }
}
