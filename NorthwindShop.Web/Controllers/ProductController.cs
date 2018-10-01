using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.ViewModels;

namespace NorthwindShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 ISupplierService supplierService,
                                 IMapper mapper,
                                 IConfiguration configuration)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var maximumProductsCountToDisplay = _configuration.GetValue<int>("ProductPageSettings:MaximumProductsCount", 0);
            var productsDtos = maximumProductsCountToDisplay > 0 ?
                _productService.GetWithInclude(c => c.Category, s => s.Supplier).Take(maximumProductsCountToDisplay) :
                _productService.GetWithInclude(c => c.Category, s => s.Supplier);
            var products = _mapper.Map<List<ProductViewModel>>(productsDtos);

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categoriesDtos = _categoryService.Get();
            var categories = _mapper.Map<List<CategoryForProductViewModel>>(categoriesDtos);
            var suppliersDtos = _supplierService.Get();
            var suppliers = _mapper.Map<List<SupplierForProductViewModel>>(suppliersDtos);
            var createProductViewModel = new CreateProductViewModel();
            createProductViewModel.Categories = new SelectList(categories, "Id", "Name");
            createProductViewModel.Suppliers = new SelectList(suppliers, "Id", "Name");

            return View(createProductViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var product = _mapper.Map<ProductDTO>(model);
                _productService.Add(product);
                return RedirectToAction("Index", "Product");
            }

            return RedirectToAction("Create", "Product");
        }
    }
}