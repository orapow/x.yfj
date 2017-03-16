using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using X.Core.Plugin;
using X.Core.Utility;
using X.Wx.Com;
using X.Wx.Mp.Com;

namespace X.Wx.Mp
{
    /// <summary>
    /// 素材
    /// </summary>
    public class Media
    {
        #region RegionName
        ///// <summary>
        ///// 下载图片
        ///// </summary>
        ///// <param name="mid"></param>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static string DownImage(string mid, string path, string access_token)
        //{
        //    var wxurl = String.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", access_token, mid);

        //    var hr = HttpWebRequest.Create(wxurl) as HttpWebRequest;
        //    hr.Method = "GET";
        //    Image img = null;
        //    try
        //    {
        //        var rs = hr.GetResponse() as HttpWebResponse;
        //        img = Image.FromStream(rs.GetResponseStream());
        //        rs.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Loger.Error(new Exception("微信图片下载失败", ex));
        //        return string.Empty;
        //    }

        //    try
        //    {
        //        var pix = getPix(img.RawFormat);
        //        var url = String.Format("{0}/{1:yyyy-MM-dd}/{2}{3}", path.TrimEnd('/'), DateTime.Now, Guid.NewGuid(), pix);
        //        var file = new FileInfo(HttpContext.Current.Server.MapPath(url));
        //        if (!file.Directory.Exists) file.Directory.Create();
        //        img.Save(file.FullName);
        //        return url;
        //    }
        //    catch (Exception ex)
        //    {
        //        Loger.Error(new Exception("微信图片保存失败", ex));
        //        return string.Empty;
        //    }
        //    finally
        //    {
        //        img.Dispose();
        //    }
        //}
        ///// <summary>
        ///// 上传文件
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //public static string UpFile(byte[] data, string type, string filename, string access_token)
        //{
        //    return Tools.PostHttpFile("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + access_token + "&type=" + type, data, filename);
        //}

        //public static string DownFile(string mid, string path)
        //{
        //    return "";
        //}
        ///// <summary>
        ///// 上传图片
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns>
        ///// 返回图片地址
        ///// </returns>
        //public static string UpLoadImage(string file)
        //{
        //    var wc = new WebClient();
        //    try
        //    {
        //        string boundary = "----" + DateTime.Now.Ticks.ToString("x");
        //        var header = Encoding.UTF8.GetBytes("----" + boundary + "\r\nContent-Disposition: form-data; filename=\"" + filename + "\"\r\n\r\n");
        //        var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

        //        var post = new List<byte>();
        //        post.AddRange(header);
        //        post.AddRange(data);
        //        post.AddRange(footer);

        //        wc.Headers.Add("Content-Type", string.Format("multipart/form-data;boundary={0}", boundary));
        //        var back = wc.UploadData(url, "POST", post.ToArray());
        //        return Encoding.UTF8.GetString(back);
        //    }
        //    catch (Exception ex)
        //    {
        //        Loger.Error(ex);
        //        throw;
        //    }
        //    finally { wc.Dispose(); }
        //}
        #endregion

        #region 永久素材
        /// <summary>
        /// 上传素材
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static upload UploadFile(string file, string type, string access_token)
        {
            return UploadFile(file, type, "", "", access_token);
        }
        public static upload UploadFile(string file, string type, string title, string desc, string access_token)
        {
            return PostFile("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token=" + access_token, file, type, title, desc);
        }
        /// <summary>
        /// 上传图文素材
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string UploadNews(string access_token)
        {
            return "";
        }
        /// <summary>
        /// 获取素材
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string GetFile(string media_id, string access_token)
        {
            return Tools.PostHttpData("https://api.weixin.qq.com/cgi-bin/material/get_material?access_token=" + access_token, "{\"media_id\":\"" + media_id + "\"}");
        }
        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="type"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static list<item> LoadItemList(int page, int limit, string type, string access_token)
        {
            var json = LoadList(page, limit, type, access_token);
            return Serialize.FromJson<list<item>>(json);
        }
        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="type"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static list<news> LoadNewsList(int page, int limit, string access_token)
        {
            var json = LoadList(page, limit, "news", access_token);
            return Serialize.FromJson<list<news>>(json);
        }
        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static bool DeleteFile(string media_id, string access_token)
        {
            var json = Api.PostData("https://api.weixin.qq.com/cgi-bin/material/del_material?access_token=" + access_token, "{\"media_id\":\"" + media_id + "\"}");
            var back = Serialize.FromJson<msgbase>(json);
            if (back.errcode != "0") throw new WxExcep(json);
            return true;
        }
        #endregion

        #region 临时素材
        public static upload UploadTmpTile(string file, string type, string access_token)
        {
            return PostFile("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + access_token + "&type=" + type, file, type, "", "");
        }
        public static byte[] GetTmpFile(string media_id, string access_token)
        {
            var wc = new WebClient();
            try
            {
                var data = wc.DownloadData("https://api.weixin.qq.com/cgi-bin/media/get?access_token=" + access_token + "&media_id=" + media_id);
                var json = Encoding.UTF8.GetString(data);
                if (json.IndexOf("errcode") > 0) throw new WxExcep(json);
                return data;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                throw;
            }
            finally
            {
                wc.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// 获取列表json
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="type"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        private static string LoadList(int page, int limit, string type, string access_token)
        {
            var dict = new Dictionary<string, object>();
            dict.Add("type", type);
            dict.Add("offset", (page - 1) * limit);
            dict.Add("count", limit);

            return Api.PostData("material/batchget_material?access_token=" + access_token, Serialize.ToJson(dict));
        }
        private static upload PostFile(string url, string file, string type, string title, string desc)
        {
            var wc = new WebClient();
            try
            {
                string boundary = "----" + DateTime.Now.Ticks.ToString("x");
                var header = "----" + boundary + "\r\nContent-Disposition: form-data;name=\"{0}\" filename=\"{1}\"\r\n\r\n";
                var footer = "\r\n--" + boundary + "--\r\n";

                var post = new List<byte>();
                post.AddRange(Encoding.UTF8.GetBytes(string.Format(header, "media", file)));
                post.AddRange(File.ReadAllBytes(HttpContext.Current.Server.MapPath(file)));
                post.AddRange(Encoding.UTF8.GetBytes(footer));

                if (type == "video" && !string.IsNullOrEmpty(title + desc))
                {
                    post.AddRange(Encoding.UTF8.GetBytes(string.Format(header, "description", "")));
                    post.AddRange(Encoding.UTF8.GetBytes("{\"title\":\"" + title + "\",\"introduction\":\"" + desc + "\"}"));
                    post.AddRange(Encoding.UTF8.GetBytes(footer));
                }

                wc.Headers.Add("Content-Type", string.Format("multipart/form-data;boundary={0}", boundary));
                var data = Encoding.UTF8.GetString(wc.UploadData(url, "POST", post.ToArray()));
                var back = Serialize.FromJson<upload>(data);
                if (!string.IsNullOrEmpty(back.errcode)) throw new WxExcep(data);

                return back;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                throw;
            }
            finally { wc.Dispose(); }
        }

        public class list<T> : msgbase
        {
            public int total_count { get; set; }
            public int item_count { get; set; }
            public List<T> item { get; set; }
        }
        public class media
        {
            public string media_id { get; set; }
            public string update_time { get; set; }
        }
        public class item : media
        {
            public string url { get; set; }
            public string name { get; set; }
        }
        public class news : media
        {
            public content content { get; set; }
        }
        public class content
        {
            public List<news_item> news_item { get; set; }
        }
        public class news_item
        {
            public string title { get; set; }
            public string thumb_media_id { get; set; }
            public string thumb_url { get; set; }
            public int show_cover_pic { get; set; }
            public string author { get; set; }
            public string digest { get; set; }
            public string content { get; set; }
            public string url { get; set; }
            public string content_source_url { get; set; }
        }

        public class upload : msgbase
        {
            public string media_id { get; set; }
            public string type { get; set; }
            public long created_at { get; set; }
            public string url { get; set; }
        }

    }

}
