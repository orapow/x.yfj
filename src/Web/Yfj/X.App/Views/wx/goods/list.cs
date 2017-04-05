using System.Linq;
namespace X.App.Views.wx.goods
{
    public class list : _wx
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
                return "cid";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("cts", GetDictList("goods.cate", "0").Take(5));
        }
    }
}
