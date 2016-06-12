using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2;
using WebApplication2.Models;

namespace System.Web.Mvc
{
    public class AuthorizeUserAccessLevel :AuthorizeAttribute
    {
        public string UserRole { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();

            var isAutorized = base.AuthorizeCore(httpContext);
            if (!isAutorized)
            {
                return false;
            }
            string CurrentUser = HttpContext.Current.User.Identity.Name.ToString();
            var res = dc.Users.Where(a => a.Username.Equals(CurrentUser)).FirstOrDefault();
            string CurrentUserRole = res.Area;
            if (this.UserRole.Contains(CurrentUserRole))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}