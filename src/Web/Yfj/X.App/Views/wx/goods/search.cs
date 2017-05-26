using System.Linq;
namespace X.App.Views.wx.goods
{
    public class search : _wx
    {
        public string key { get; set; }
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

    }
}
