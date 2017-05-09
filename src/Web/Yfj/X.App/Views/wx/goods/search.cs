using System.Linq;
namespace X.App.Views.wx.goods
{
    public class search : _wx
    {
        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }

        protected override string GetParmNames
        {
            get
            {
                return "key";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict["key"] = Context.Server.UrlDecode(dict["key"] + "");
        }
    }
}
