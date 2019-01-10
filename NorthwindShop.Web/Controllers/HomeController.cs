using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.ViewModels;

namespace NorthwindShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(IProductService productService,
                              ILogger<HomeController> logger,
                              IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var productDtos = (await _productService.GetWithInclude(c => c.Category, s => s.Supplier)).Take(3);
            var productsViewModel = _mapper.Map<List<ProductViewModel>>(productDtos);

            return View(productsViewModel);
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