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
        public string cate { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "cate";
            }
        }
    }
}
