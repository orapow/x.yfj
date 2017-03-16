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
    }
}
