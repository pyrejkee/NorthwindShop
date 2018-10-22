using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindShop.BLL.EntitiesDTO;
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

        public IActionResult Image(int id)
        {
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
                RedirectToAction(nameof(Index));
            }

            var categoryViewModel = _mapper.Map<EditCategoryViewModel>(category);

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categoryDto = _mapper.Map<CategoryDTO>(model);

                _categoryService.Update(categoryDto);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}