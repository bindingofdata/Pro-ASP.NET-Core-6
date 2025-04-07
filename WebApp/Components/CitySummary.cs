using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

using WebApp.Models;

namespace WebApp.Components
{
    public sealed class CitySummary : ViewComponent
    {
        private readonly CitiesData _data;

        public CitySummary(CitiesData data)
        {
            _data = data;
        }

        public string Invoke()
        {
            if (RouteData.Values["controller"] != null)
            {
                return "Controller Request";
            }
            else
            {
                return "Razor Page Request";
            }
        }
    }
}
