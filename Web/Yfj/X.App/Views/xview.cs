using X.App.Com;
using X.Web.Views;

namespace X.App.Views
{
    public class xview : View
    {
        /// <summary>
        /// 系统配置文件
        /// </summary>
        protected Config cfg = null;

        protected override void InitView()
        {
            base.InitView();
            cfg = Config.LoadConfig();
            dict.Add("cfg", cfg);
        }
    }
}
