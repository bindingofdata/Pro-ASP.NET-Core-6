using Microsoft.AspNetCore.Mvc;

using SportsStore.Models;

namespace SportsStore.Components
{
    public sealed class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreRepository _repository;

        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke() => View(_repository.Products
            .Select(product => product.Category)
            .Distinct()
            .OrderBy(category => category));
    }
}
