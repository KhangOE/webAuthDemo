using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using web_authentication.entities;

namespace web_authentication.filter
{
    public class AuthorizationNeedAllFilterAttribute : Attribute, IAuthorizationFilter
    {
        private string _role;
        public AuthorizationNeedAllFilterAttribute(string role)
        {
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //throw new NotImplementedException();
            
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }

            var roleNeeded = _role.Split(',');

            var userRole = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select( x => x.Value);
          /*  foreach( var claim in userRole ) {
                Console.WriteLine( "user role : " + claim );
            }
            foreach (var claim in roleNeeded)
            {
                Console.WriteLine("need role : " + claim);
            }
            Console.WriteLine("check  " +  !roleNeeded.Except(userRole).Any());*/
            if (roleNeeded.Except(userRole).Any()){
                context.Result = new ForbidResult();
                return;
            }   
        }
    }
}
