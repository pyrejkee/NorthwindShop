using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.BLL.Automapper;
using NorthwindShop.DAL.Entities;
using NorthwindShop.DAL.Interfaces;
using NorthwindShop.Web.Automapper;
using NorthwindShop.Web.Controllers;
using NorthwindShop.Web.ViewModels;
using Xunit;
using NorthwindShop.BLL.Services.Implementations;

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
            categoryService.Setup(x => x.Get()).Returns(GetCategoryDTOs());
            var controller = new CategoryController(categoryService.Object, autoMapper, distributedCache.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<CategoryViewModel>>(((ViewResult) result).ViewData.Model);
            IEnumerable<CategoryViewModel> expectedCategoryViewModels = new List<CategoryViewModel>
            {
                new CategoryViewModel { Id = 1, Name = "Category 1", Description = "Description 1"},
                new CategoryViewModel { Id = 2, Name = "Category 2", Description = "Description 2"}
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
            categoryService.Setup(x => x.Get()).Returns(GetCategoryDTOs());
            var controller = new CategoryController(categoryService.Object, autoMapper, distributedCache.Object);
            var categoryId = 3;

            // Act
            var result = controller.EditCategory(categoryId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void EditCategory_GET_WhenSpecifiedCategoryExists_ShouldReturnViewWithViewModel()
        {
            // Arrange
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();
            var categoryService = new Mock<ICategoryService>();
            var distributedCache = new Mock<IDistributedCache>();

            var categoryId = 2;
            categoryService.Setup(x => x.GetById(categoryId)).Returns(GetCategoryDTOs().FirstOrDefault(x => x.Id == categoryId));
            var controller = new CategoryController(categoryService.Object, autoMapper, distributedCache.Object);
            

            // Act
            var result = controller.EditCategory(categoryId);

            // Assert
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditCategoryViewModel>(((ViewResult)result).ViewData.Model);
            Assert.Equal(new EditCategoryViewModel
            {
                Id = 2,
                Name = "Category 2",
                Description = "Description 2"
            }, model, new ValueComparer<EditCategoryViewModel>());
        }

        [Fact]
        public void EditCategory_POST_WhenModelStateIsInvalidValid_ShouldReturnBadRequest()
        {
            // Arrange
            var autoMapper = new Mock<IMapper>();
            var categoryService = new Mock<ICategoryService>();
            var distributedCache = new Mock<IDistributedCache>();
            var controller = new CategoryController(categoryService.Object, autoMapper.Object, distributedCache.Object);
            controller.ModelState.AddModelError("SomeKey", "SomeError");

            // Act
            var result = controller.EditCategory(It.IsAny<EditCategoryViewModel>());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void EditCategory_POST_WhenModelStateIsValid_ShouldUpdateCategory()
        {
            // Arrange
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
                cfg.AddProfile(new BusinessLogicProfile());
            });
            var autoMapper = configMapper.CreateMapper();
            var baseRepository = new Mock<IRepository<Category>>();
            var categoryService = new CategoryService(baseRepository.Object, autoMapper);
            var distributedCache = new Mock<IDistributedCache>();

            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.img";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);


            var newEditCategoryViewModel = new EditCategoryViewModel
            {
                Id = 1,
                Name = "Category 1 updated",
                Description = "Description 1 updated",
                Image = fileMock.Object
            };
            var newEditCategoryViewModelDto = autoMapper.Map<CategoryDTO>(newEditCategoryViewModel);
            
            var controller = new CategoryController(categoryService, autoMapper, distributedCache.Object);

            // Act
            var result = controller.EditCategory(newEditCategoryViewModel);

            // Assert
            baseRepository.Verify(x => x.Update(It.IsAny<Category>()), Times.Once());
        }

        private List<CategoryDTO> GetCategoryDTOs()
        {
            var categoryDtos = new List<CategoryDTO>
            {
                new CategoryDTO {Id = 1, Name = "Category 1", Description = "Description 1"},
                new CategoryDTO {Id = 2, Name = "Category 2", Description = "Description 2"}
            };

            return categoryDtos;
        }
    }
}
