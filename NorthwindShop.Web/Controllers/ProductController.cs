using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 ISupplierService supplierService,
                                 IMapper mapper,
                                 IConfiguration configuration,
                                 ILogger<ProductController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace("Reading > MaximumProductsCount < from configuration");
            var maximumProductsCountToDisplay = _configuration.GetValue<int>("ProductPageSettings:MaximumProductsCount", 0);
            _logger.LogTrace($"> MaximumProductsCount < has been read. Value: { maximumProductsCountToDisplay }");

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
                var addedProduct = _productService.Add(product);
                var productViewModel = _mapper.Map<ProductViewModel>(addedProduct);

                return RedirectToAction(nameof(Details), new { id = productViewModel.Id });
            }

            return RedirectToAction(nameof(Create));
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetWithInclude(x => x.ProductId == id, c => c.Category,
                s => s.Supplier).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var productViewModel = _mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetWithInclude(x => x.ProductId == id, c => c.Category, s => s.Supplier)
                .FirstOrDefault();
            if (product == null)
            {
                RedirectToAction(nameof(Index));
            }

            var categoriesDtos = _categoryService.Get();
            var categories = _mapper.Map<List<CategoryForProductViewModel>>(categoriesDtos);
            var suppliersDtos = _supplierService.Get();
            var suppliers = _mapper.Map<List<SupplierForProductViewModel>>(suppliersDtos);
            var productViewModel = _mapper.Map<EditProductViewModel>(product);
            productViewModel.Categories = new SelectList(categories, "Id", "Name");
            productViewModel.Suppliers = new SelectList(suppliers, "Id", "Name");

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.Get(x => x.ProductId == model.Id).FirstOrDefault();
                if (product == null)
                {
                    return BadRequest();
                }

                product.Name = model.Name;
                product.UnitPrice = model.UnitPrice;
                product.CategoryId = model.CategoryId;
                product.SupplierId = model.SupplierId;

                _productService.Update(product);

                return RedirectToAction(nameof(Details), new { id = product.Id });
            }

            return View();
        }
    }
}