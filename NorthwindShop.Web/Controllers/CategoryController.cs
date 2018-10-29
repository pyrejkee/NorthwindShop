using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.ViewModels;

namespace NorthwindShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public CategoryController(ICategoryService categoryService,
                                  IMapper mapper,
                                  IDistributedCache cache)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _cache = cache;
        }

        public IActionResult Index()
        {
            var categoriesDtos = _categoryService.Get();
            var categoriesViewModels = _mapper.Map<List<CategoryViewModel>>(categoriesDtos);

            return View(categoriesViewModels);
        }

        public IActionResult Image(int id)
        {
            var cachedImage = _cache.Get($"categoryId-{id}");
            if (cachedImage != null)
            {
                return File(cachedImage, "image/jpg");
            }

            var category = _categoryService.GetById(id);

            if(category?.Picture.Length == 0)
            {
                return NotFound();
            }
            
            return File(category.Picture, "image/jpg");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var categoryViewModel = _mapper.Map<EditCategoryViewModel>(category);

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDto = _mapper.Map<CategoryDTO>(model);

            var updatedCategory = _categoryService.Update(categoryDto);

            return RedirectToAction(nameof(Index));
        }
    }
}