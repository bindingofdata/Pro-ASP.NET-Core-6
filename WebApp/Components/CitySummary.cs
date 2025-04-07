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

        public IViewComponentResult Invoke(string themeName)
        {
            ViewBag.Theme = themeName;
            return View(new CityViewModel
            {
                Cities = _data.Cities.Count,
                Population = _data.Cities.Sum(city => city.Population),
            });
        }
    }
}
