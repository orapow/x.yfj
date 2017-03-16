using System;
using System.IO;
using System.Text;
using X.Core.Utility;
using X.Web.Com;

namespace X.App.Apis.com
{
    public class uploadbd : upload
    {
        public override byte[] GetResponse()
        {
            type = "img";
            InitApi();
            var callback = Context.Request["callback"];
            var editorId = Context.Request["editorid"];
            uploadFile = Context.Request.Files[0];

            var rspbd = new Resp_UploadBd()
            {
                originalName = uploadFile.FileName,
                size = uploadFile.ContentLength,
                type = Path.GetExtension(uploadFile.FileName)
            };
            var rsp = upFile();
            if (rsp.issucc)
            {
                rspbd.name = Path.GetFileName(rsp.url);
                rspbd.state = "SUCCESS";
                rspbd.url = rsp.url;
            }
            else
            {
                rspbd.state = "";
            }

            var json = Serialize.ToJson(rspbd);

            if (callback != null)
            {
                return Encoding.UTF8.GetBytes(String.Format("<script>{0}(JSON.parse(\"{1}\"));</script>", callback, json));
            }
            else
            {
                return Encoding.UTF8.GetBytes(json);
            }
        }

        public class Resp_UploadBd : XResp
        {
            public string state { get; set; }
            public string url { get; set; }
            public string originalName { get; set; }
            public string name { get; set; }
            public int size { get; set; }
            public string type { get; set; }
        }
    }
}
