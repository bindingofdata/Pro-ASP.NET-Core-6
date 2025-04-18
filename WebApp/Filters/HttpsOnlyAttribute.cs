using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
    public sealed class HttpsOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsHttps)
            {
                filterContext.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
