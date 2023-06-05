using AggregationApp.Controllers;
using AggregationApp.Data;
using AggregationApp.Models;
using AggregationApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AggregationApp.tests
{
    public class ElectricityControllerTests
    {
        [Fact]
        public async Task GetAggregatedData_ReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ElectricityController>>();
            var electricityDataServiceMock = new Mock<IElectricityDataService>();
            var contextMock = new Mock<ElectricityDbContext>();

            var controller = new ElectricityController(loggerMock.Object, electricityDataServiceMock.Object, contextMock.Object);

            // Act
            var result = await controller.GetAggregatedData();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAggregatedData_ReturnsOkResult_WithEmptyData()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ElectricityController>>();
            var electricityDataServiceMock = new Mock<IElectricityDataService>();
            electricityDataServiceMock.Setup(service => service.GetAggregatedDataAsync()).ReturnsAsync(new List<APIResponseModel>());
            var contextMock = new Mock<ElectricityDbContext>();

            var controller = new ElectricityController(loggerMock.Object, electricityDataServiceMock.Object, contextMock.Object);

            // Act
            var result = await controller.GetAggregatedData();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<List<APIResponseModel>>(okResult.Value);
            Assert.Empty(data);
        }

        [Fact]
        public async Task GetAggregatedData_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ElectricityController>>();
            var electricityDataServiceMock = new Mock<IElectricityDataService>();
            electricityDataServiceMock.Setup(service => service.GetAggregatedDataAsync()).ThrowsAsync(new Exception());
            var contextMock = new Mock<ElectricityDbContext>();

            var controller = new ElectricityController(loggerMock.Object, electricityDataServiceMock.Object, contextMock.Object);

            // Act
            var result = await controller.GetAggregatedData();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public async Task Download_ReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ElectricityController>>();
            var electricityDataServiceMock = new Mock<IElectricityDataService>();
            electricityDataServiceMock.Setup(service => service.DownloadCsvFiles()).ReturnsAsync(true);
            var contextMock = new Mock<ElectricityDbContext>();

            var controller = new ElectricityController(loggerMock.Object, electricityDataServiceMock.Object, contextMock.Object);

            // Act
            var result = await controller.Download();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Download_ReturnsBadRequestResult()
        {
            
            var loggerMock = new Mock<ILogger<ElectricityController>>();
            var electricityDataServiceMock = new Mock<IElectricityDataService>();
            electricityDataServiceMock.Setup(service => service.DownloadCsvFiles()).ReturnsAsync(false);
            var contextMock = new Mock<ElectricityDbContext>();

            var controller = new ElectricityController(loggerMock.Object, electricityDataServiceMock.Object, contextMock.Object);

            
            var result = await controller.Download();

            
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Download_ReturnsStatusCode500OnException()
        {
            
            var loggerMock = new Mock<ILogger<ElectricityController>>();
            var electricityDataServiceMock = new Mock<IElectricityDataService>();
            electricityDataServiceMock.Setup(service => service.DownloadCsvFiles()).ThrowsAsync(new Exception());
            var contextMock = new Mock<ElectricityDbContext>();

            var controller = new ElectricityController(loggerMock.Object, electricityDataServiceMock.Object, contextMock.Object);

            
            var result = await controller.Download();

            
            Assert.IsType<ObjectResult>(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

    }
}
