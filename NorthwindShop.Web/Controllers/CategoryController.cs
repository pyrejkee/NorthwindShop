using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
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

        public IActionResult Image(int id)
        {
            var category = _categoryService.GetById(id);

            if(category == null)
            {
                return NotFound();
            }

            byte[] correctImageStream = new byte[category.Picture.Length - 78];
            Buffer.BlockCopy(category.Picture, 78, correctImageStream, 0, category.Picture.Length - 78);

            return File(correctImageStream, "image/bmp");
        }
    }
}