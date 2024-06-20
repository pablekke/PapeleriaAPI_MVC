using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace MVC.Controllers
{
    public class TokenAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext contexto)
        {
            var session = contexto.HttpContext.Session;
            var token = session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                contexto.Result = new RedirectToActionResult("Login", "Login", null);
            }
        }
    }
}