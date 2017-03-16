using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.Wx.Mp
{
    /// <summary>
    /// 门店管理
    /// </summary>
    public class Store
    {
        public static void Add()
        {

        }
        public static Poi Get(int poi_id)
        {
            return null;
        }
        public static List<Poi> List()
        {
            return null;
        }
        public static void Update(Poi o)
        {

        }
        public static void Del(int poi_id)
        {

        }
        public static string Category()
        {
            return "";
        }
        /// <summary>
        /// 门店管理
        /// </summary>
        public class Poi
        {
            /// <summary>
            ///  门店名称（仅为商户名，如：国美、麦当劳，不应包含地区、地址、分店名等信息，错误示例：北京国美）	 是
            /// </summary>
            public string business_name { get; set; }
            /// <summary>
            /// 分店名称（不应包含地区信息，不应与门店名有重复，错误示例：北京王府井店）	 是
            /// </summary>
            public string branch_name { get; set; }
            /// <summary>
            /// 	 门店所在的省份（直辖市填城市名,如：北京市）	 是
            /// </summary>
            public string province { get; set; }
            /// <summary>
            /// 	 门店所在的城市	 是
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 		 门店所在地区	 是
            /// </summary>
            public string district { get; set; }
            /// <summary>
            /// 门店所在的详细街道地址（不要填写省市信息）	 是
            /// </summary>
            public string address { get; set; }
            /// <summary>
            ///  门店的电话（纯数字，区号、分机号均由“-”隔开）	 是
            /// </summary>
            public string telephone { get; set; }
            /// <summary>
            ///  门店的类型（不同级分类用“,”隔开，如：美食，川菜，火锅。详细分类参见附件：微信门店类目表）	 是
            /// </summary>
            public string categories { get; set; }
            /// <summary>
            /// 	 坐标类型，1 为火星坐标（目前只能选1）	 是
            /// </summary>
            public string offset_type { get; set; }
            /// <summary>
            ///  门店所在地理位置的经度	 是
            /// </summary>
            public string longitude { get; set; }
            /// <summary>
            /// 门店所在地理位置的纬度（经纬度均为火星坐标，最好选用腾讯地图标记的坐标）	 是
            /// </summary>
            public string latitude { get; set; }
            /// <summary>
            /// 	 图片列表，url 形式，可以有多张图片，尺寸为   640*340px	。必须为上一接口生成的url。图片内容不允许与门店不相关，不允许为二维码、员工合照（或模特肖像）、营业执照、无门店正门的街景、地图截图、公交地铁站牌、菜单截图等   是
            /// </summary>
            public string photo_list { get; set; }
            /// <summary>
            ///  特色服务，如免费wifi，免费停车，送货上门等商户能提供的特色功能或服务	 是
            /// </summary>
            public string special { get; set; }
            /// <summary>
            /// 		 营业时间，24 小时制表示，用“-”连接，如 8:00-20:00	 是
            /// </summary>
            public string open_time { get; set; }
            /// <summary>
            ///  人均价格，大于0 的整数	 是
            /// </summary>
            public string avg_price { get; set; }
            /// <summary>
            /// 	 商户自己的id，用于后续审核通过收到poi_id 的通知时，做对应关系。请商户自己保证唯一识别性	 否
            /// </summary>
            public string sid { get; set; }
            /// <summary>
            /// 		 商户简介，主要介绍商户信息等	 否
            /// </summary>
            public string introduction { get; set; }
            /// <summary>
            /// 	 推荐品，餐厅可为推荐菜；酒店为推荐套房；景点为推荐游玩景点等，针对自己行业的推荐内容	 否
            /// </summary>
            public string recommend { get; set; }
        }
    }
}
