using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.Automapper;
using NorthwindShop.Web.Controllers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NorthwindShop.DAL.Entities;
using NorthwindShop.Web.ViewModels;
using Xunit;

namespace ControllerTests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Index_WhenProductsExist_ShouldReturnViewResultWithListOfProductViewModel()
        {
            var productSevice = new Mock<IProductService>();
            var categoryService = new Mock<ICategoryService>();
            var supplierService = new Mock<ISupplierService>();
            var configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            var logger = new Mock<ILogger<ProductController>>();
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();

            productSevice.Setup(x => x.Get()).ReturnsAsync(GetProductDtos);

            var productController = new ProductController(productSevice.Object, categoryService.Object, supplierService.Object, autoMapper, configuration.Object, logger.Object);

            // Act
            var result = await productController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_GET_WhenThereAreExistProducts_ShouldReturnThem()
        {
            var productSevice = new Mock<IProductService>();
            var categoryService = new Mock<ICategoryService>();
            var supplierService = new Mock<ISupplierService>();
            var configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            var logger = new Mock<ILogger<ProductController>>();
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();

            productSevice.Setup(x => x.Get()).ReturnsAsync(GetProductDtos());
            categoryService.Setup(x => x.Get()).ReturnsAsync(GetCategoryDtos());
            supplierService.Setup(x => x.Get()).ReturnsAsync(GetSupplierDtos());

            var productController = new ProductController(productSevice.Object, categoryService.Object, supplierService.Object, autoMapper, configuration.Object, logger.Object);

            // Act
            var result = await productController.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreateProductViewModel>(((ViewResult)result).ViewData.Model);
        }

        [Fact]
        public async Task Create_POST_WhenModelStateIsValid_ShouldRedirectToDetailsAction()
        {
            var productSevice = new Mock<IProductService>();
            var categoryService = new Mock<ICategoryService>();
            var supplierService = new Mock<ISupplierService>();
            var configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            var logger = new Mock<ILogger<ProductController>>();
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();

            productSevice.Setup(x => x.Add(It.IsAny<ProductDTO>())).ReturnsAsync(new ProductDTO());

            var productController = new ProductController(productSevice.Object, categoryService.Object, supplierService.Object, autoMapper, configuration.Object, logger.Object);

            // Act
            var result = await productController.Create(new CreateProductViewModel());

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Details_WhenSuchProductExists_ShouldReturnViewResultWithModel()
        {
            var productSevice = new Mock<IProductService>();
            var categoryService = new Mock<ICategoryService>();
            var supplierService = new Mock<ISupplierService>();
            var configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            var logger = new Mock<ILogger<ProductController>>();
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();

            productSevice.Setup(x => x.GetWithInclude(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<Expression<Func<Product, object>>[]>()))
                .ReturnsAsync(new List<ProductDTO>{ new ProductDTO { Id = 1, Name = "Name 1" } });

            var productController = new ProductController(productSevice.Object, categoryService.Object, supplierService.Object, autoMapper, configuration.Object, logger.Object);

            // Act
            var result = await productController.Details(1);

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ProductViewModel>(((ViewResult)result).ViewData.Model);
        }

        [Fact]
        public async Task Edit_GET_WhenProductExists_ShouldReturnViewResultWithEditProductViewModel()
        {
            var productSevice = new Mock<IProductService>();
            var categoryService = new Mock<ICategoryService>();
            var supplierService = new Mock<ISupplierService>();
            var configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            var logger = new Mock<ILogger<ProductController>>();
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();

            productSevice.Setup(x => x.GetWithInclude(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<Expression<Func<Product, object>>[]>()))
                .ReturnsAsync(new List<ProductDTO> { new ProductDTO { Id = 1, Name = "Name 1" } });
            categoryService.Setup(x => x.Get()).ReturnsAsync(GetCategoryDtos());
            supplierService.Setup(x => x.Get()).ReturnsAsync(GetSupplierDtos());

            var productController = new ProductController(productSevice.Object, categoryService.Object, supplierService.Object, autoMapper, configuration.Object, logger.Object);

            // Act
            var result = await productController.Edit(1);

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<EditProductViewModel>(((ViewResult)result).ViewData.Model);
        }

        [Fact]
        public async Task Edit_POST_WhenModelStateIsValid_ShouldRedirectToDetailsAction()
        {
            var productSevice = new Mock<IProductService>();
            var categoryService = new Mock<ICategoryService>();
            var supplierService = new Mock<ISupplierService>();
            var configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            var logger = new Mock<ILogger<ProductController>>();
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebProfile());
            });
            var autoMapper = configMapper.CreateMapper();

            productSevice.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<ProductDTO> { new ProductDTO { Id = 1, Name = "Name 1" } });

            var productController = new ProductController(productSevice.Object, categoryService.Object, supplierService.Object, autoMapper, configuration.Object, logger.Object);

            // Act
            var result = await productController.Edit(new EditProductViewModel());

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }


        private List<ProductDTO> GetProductDtos()
        {
            var productDtos = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, Name = "Product 1", UnitPrice = 10, CategoryId = 1, SupplierId = 1},
                new ProductDTO { Id = 2, Name = "Product 2", UnitPrice = 20, CategoryId = 1, SupplierId = 1},
                new ProductDTO { Id = 3, Name = "Product 3", UnitPrice = 30, CategoryId = 2, SupplierId = 1}
            };

            return productDtos;
        }

        private List<CategoryDTO> GetCategoryDtos()
        {
            var categoryDtos = new List<CategoryDTO>
            {
                new CategoryDTO {Id = 1, Name = "Category 1", Description = "Description 1"},
                new CategoryDTO {Id = 2, Name = "Category 2", Description = "Description 2"}
            };

            return categoryDtos;
        }

        private List<SupplierDTO> GetSupplierDtos()
        {
            var supplierDtos = new List<SupplierDTO>
            {
                new SupplierDTO {Id = 1, CompanyName = "Name 1"},
                new SupplierDTO {Id = 2, CompanyName = "Name 2"}
            };

            return supplierDtos;
        }
    }
}
