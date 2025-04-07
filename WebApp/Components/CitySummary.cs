using Microsoft.AspNetCore.Mvc;

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
            return View(new CityViewModel
            {
                Cities = _data.Cities.Count,
                Population = _data.Cities.Sum(c => c.Population),
            });
        }
    }
}
