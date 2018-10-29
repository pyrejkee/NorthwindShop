using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.Automapper;
using NorthwindShop.Web.Controllers;
using System.Collections.Generic;
using Xunit;

namespace ControllerTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Index_WhenProductsExist_ShouldReturnViewResultWithListOfProductViewModel()
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

            productSevice.Setup(x => x.Get()).Returns(GetProductDtos());

            var productController = new ProductController(productSevice.Object, categoryService.Object, supplierService.Object, autoMapper, configuration.Object, logger.Object);

            // Act
            //var result = productController.Index();

            //Assert.IsType<ViewResult>(result);
        }



        private List<ProductDTO> GetProductDtos()
        {
            var productDtos = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, Name = "Product 1", UnitPrice = 10 },
                new ProductDTO { Id = 2, Name = "Product 2", UnitPrice = 20 },
                new ProductDTO { Id = 3, Name = "Product 3", UnitPrice = 30 }
            };

            return productDtos;
        }
    }
}
