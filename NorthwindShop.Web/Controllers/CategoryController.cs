using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.ViewModels;

namespace NorthwindShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,
                                  IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var categoriesDtos = _categoryService.Get();
            var categoriesViewModels = _mapper.Map<List<CategoryViewModel>>(categoriesDtos);

            return View(categoriesViewModels);
        }
    }
}