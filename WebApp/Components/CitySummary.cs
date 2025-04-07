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

        public string Invoke()
        {
            return $"{_data.Cities.Count} cities, "
                + $"{_data.Cities.Sum(city => city.Population)}";
        }
    }
}
