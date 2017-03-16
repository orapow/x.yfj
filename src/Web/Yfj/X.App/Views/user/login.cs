using System;
using System.Linq;

namespace X.App.Views.user
{
    public class login : _u
    {
        protected override bool needuser
        {
            get
            {
                return false;
            }
        }
    }
}
