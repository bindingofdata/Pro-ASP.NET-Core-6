using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using WebApp.Filters;

namespace WebApp.Controllers
{
    [HttpsOnly]
    [ResultDiagnostics]
    public sealed class HomeController : Controller
    {
        //[RequireHttps]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller.");
        }

        //[RequireHttps]
        public IActionResult Secure()
        {
            return View("Message", "This is the Secure action on the Home controller");
        }

        //[ChangeArg]
        public IActionResult Messages(string message1, string message2 = "None")
        {
            return View("Message", $"{message1}, {message2}");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("message1"))
            {
                context.ActionArguments["message1"] = "New message";
            }
        }

        [RangeException]
        public ViewResult GenerateException(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else if (id.Value > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            else
            {
                return View("Message", $"The value is {id}");
            }
        }
    }
}
