﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.Controllers;
using Xunit;

namespace ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ShouldReturnViewResult()
        {
            // Arrange
            var productService = new Mock<IProductService>();
            var logger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(productService.Object, logger.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
