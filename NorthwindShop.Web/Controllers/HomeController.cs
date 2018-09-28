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
            // For test purposes, will be deleted soon
            var allProducts = _productService.Get();
            var allProductsByFunc = _productService.Get(x => x.ProductName.StartsWith("Kirya"));
            var productsWithInclude = _productService.GetWithInclude(s => s.Supplier, od => od.OrderDetails, c => c.Category);
            var productsWithIncludeByFunc =
                _productService.GetWithInclude(x => x.ProductName.StartsWith("Kirya"), od => od.OrderDetails);

            return View(productsWithInclude);
        }
    }
}