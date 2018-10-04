using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindShop.BLL.Services.Interfaces;

namespace NorthwindShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductService productService,
                              ILogger<HomeController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Error/500")]
        public IActionResult Error500()
        {
            var contextFeature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                _logger.LogError($"Something went wrong {contextFeature.Error}");
                return View("500");
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }

            return View("500");
        }
    }
}