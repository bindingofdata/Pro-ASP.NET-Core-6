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

        public IViewComponentResult Invoke()
        {
            // Return HtmlContentViewComponentResult example
            return new HtmlContentViewComponentResult(
                new HtmlString("This is a <h3><i>string</i></h3>"));

            // Return ContentViewComponentResult example
            //return Content("This is a <h3><i>string</i></h3>");

            // Return partial view example
            //return View(new CityViewModel
            //{
            //    Cities = _data.Cities.Count,
            //    Population = _data.Cities.Sum(c => c.Population),
            //});
        }
    }
}
