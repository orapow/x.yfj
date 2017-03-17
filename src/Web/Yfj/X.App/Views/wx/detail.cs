using System.Linq;
using X.Web;

namespace X.App.Views.wx
{
    public class detail : _wx
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override bool nd_user
        {
            get
            {
                return false;
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var gc = 0;
            if (cu != null)
            {
                var g = cu.x_cart.FirstOrDefault(o => o.goods_id == id);
                if (g != null) gc = g.count.Value;
                dict.Add("tc", cu.x_cart.Sum(o => o.count.Value));
            }
            dict.Add("gc", gc);
        }
    }
}
