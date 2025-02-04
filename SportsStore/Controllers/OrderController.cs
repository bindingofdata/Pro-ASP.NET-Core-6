using Microsoft.AspNetCore.Mvc;

using SportsStore.Models;

namespace SportsStore.Controllers
{
    public sealed class OrderController : Controller
    {
        public ViewResult Checkout() => View(new Order());
    }
}
