using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.Automapper;
using NorthwindShop.Web.Controllers;
using NorthwindShop.Web.ViewModels;
using Xunit;

namespace ControllerTests
{
    public class CategoryControllerTests
    {
        [Fact]
        public void Index_WhenThereAreExistCategories_ShouldReturnViewWithCategories()
        {
            // Arrange
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();
            var categoryService = new Mock<ICategoryService>();
            var distributedCache = new Mock<IDistributedCache>();
            categoryService.Setup(x => x.Get()).Returns(new List<CategoryDTO>
            {
                new CategoryDTO {Name = "Category 1", Description = "Description 1"},
                new CategoryDTO {Name = "Category 2", Description = "Description 2"}
            });
            var controller = new CategoryController(categoryService.Object, autoMapper, distributedCache.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<CategoryViewModel>>(((ViewResult) result).ViewData.Model);
            IEnumerable<CategoryViewModel> expectedCategoryViewModels = new List<CategoryViewModel>
            {
                new CategoryViewModel { Name = "Category 1", Description = "Description 1"},
                new CategoryViewModel { Name = "Category 2", Description = "Description 2"}
            };
            Assert.Equal(expectedCategoryViewModels, model.AsEnumerable(), new ValueComparer<CategoryViewModel>());
        }

        [Fact]
        public void Image_ShouldReturnFileContentResult()
        {
            // Arrange
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();
            var categoryService = new Mock<ICategoryService>();
            var distributedCache = new Mock<IDistributedCache>();
            var controller = new CategoryController(categoryService.Object, autoMapper, distributedCache.Object);

            // Act
            var result = controller.Image(1);

            // Assert
            Assert.IsType<FileContentResult>(result);
        }

        [Fact]
        public void EditCategory_GET_WhenSpecifiedCategoryWasNotFound_ShouldReturnRedirectToIndex()
        {
            // Arrange
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();
            var categoryService = new Mock<ICategoryService>();
            var distributedCache = new Mock<IDistributedCache>();
            categoryService.Setup(x => x.Get()).Returns(new List<CategoryDTO>
            {
                new CategoryDTO {Name = "Category 1", Description = "Description 1"},
                new CategoryDTO {Name = "Category 2", Description = "Description 2"}
            });
            var controller = new CategoryController(categoryService.Object, autoMapper, distributedCache.Object);
            var categoryId = 3;

            // Act
            var result = controller.EditCategory(categoryId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
