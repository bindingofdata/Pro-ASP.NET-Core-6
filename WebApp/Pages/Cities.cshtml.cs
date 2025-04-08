using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewComponents;

using WebApp.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApp.Pages
{
    [ViewComponent(Name = "CitiesPageHybrid")]
    public sealed class CitiesModel : PageModel
    {
        public CitiesData? Data { get; set; }

        public CitiesModel(CitiesData data)
        {
            Data = data;
        }

        [ViewComponentContext]
        public ViewComponentContext Context { get; set; } = null!;

        public IViewComponentResult Invoke()
        {
            return new ViewViewComponentResult()
            {
                ViewData = new ViewDataDictionary<CityViewModel>(
                    Context.ViewData,
                    new CityViewModel()
                    {
                        Cities = Data?.Cities.Count ?? 0,
                        Population = Data?.Cities.Sum(city => city.Population) ?? 0
                    })
            };
        }
    }
}
