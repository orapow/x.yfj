namespace X.App.Views.wx
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
                return "cate";
            }
        }

    }
}
