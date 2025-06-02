using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesRep.API.Controllers;
using SalesRep.Core.DTO;
using SalesRep.Core.Interfaces;
using System.Text.Json;

namespace SalesRep.API.Tests
{
    public class SalesRepApiControllerTests
    {
        [Fact]
        public async Task GetById_ReturnsOk_WithSalesRep()
        {
            // Arrange
            var mockService = new Mock<ISalesRepService>();
            var rep = new SalesRepDetailDto { Id = 1, Name = "Test Rep" };
            mockService.Setup(s => s.GetById(1)).ReturnsAsync(rep);

            var controller = new SalesRepApiController(mockService.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRep = Assert.IsType<SalesRepDetailDto>(okResult.Value);
            Assert.Equal("Test Rep", returnedRep.Name);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            // Arrange
            var repId = 999;
            var mockService = new Mock<ISalesRepService>();
            mockService.Setup(s => s.GetById(repId)).ReturnsAsync((SalesRepDetailDto?)null);

            var controller = new SalesRepApiController(mockService.Object);

            // Act
            var result = await controller.GetById(repId);


            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var value = Assert.IsAssignableFrom<object>(notFoundResult.Value);
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSerializer.Serialize(value));

            Assert.Equal($"Resource with ID {repId} not found.", dict["message"]);

        }
    }
}
