using Microsoft.AspNetCore.Mvc;
using NorthwindShop.BLL.Services.Interfaces;

namespace NorthwindShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}