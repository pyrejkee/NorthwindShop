using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.ViewModels;

namespace NorthwindShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService,
                                 IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var productsDtos = _productService.GetWithInclude(c => c.Category, s => s.Supplier);
            var products = _mapper.Map<List<ProductViewModel>>(productsDtos);

            return View(products);
        }
    }
}