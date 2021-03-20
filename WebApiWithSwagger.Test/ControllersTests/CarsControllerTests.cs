using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using WebApiWithSwagger.BusinessLogic;
using WebApiWithSwagger.Controllers;
using WebApiWithSwagger.EFCore;
using WebApiWithSwagger.Models;
using WebApiWithSwagger.Validation;
using Xunit;

namespace WebApiWithSwagger.Test.ControllersTests
{
    public class CarsControllerTests
    {
        private DbContextOptions<ApiContext> dbContextOptions = new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;

        [Fact]
        public void GetAllStock_Should_Return_CarStocks()
        {
            //arrange 
            var mockILogger = new Mock<ILogger<CarsController>>();
            var mockCarManagement = new Mock<ICarManagement>();
            var mockValidation = new Mock<IValidation>();
            var mockApiContext = new Mock<ApiContext>(dbContextOptions);

            mockValidation.Setup(_ => _.ValidateStringParameter(It.IsAny<string>()))
               .Returns(true);
            mockApiContext.Setup(_ => _.GetCars())
              .Returns(new List<CarStock>());
            mockCarManagement.Setup(_ => _.GetAllCarsStock(It.IsAny<List<CarStock>>(), It.IsAny<string>()))
              .Returns(It.IsAny<CarStockLevelResponse>());

            var controller = new CarsController(mockApiContext.Object, mockCarManagement.Object, mockValidation.Object, mockILogger.Object);

            //act
            var result = controller.GetAllStock(It.IsAny<string>());

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void SearchStock_Should_Return_BadResponse_When_ValidationFailed()
        {
            //arrange 
            var mockILogger = new Mock<ILogger<CarsController>>();
            var mockCarManagement = new Mock<ICarManagement>();
            var mockValidation = new Mock<IValidation>();
            var mockApiContext = new Mock<ApiContext>(dbContextOptions);

            mockValidation.Setup(_ => _.ValidateStringParameter(It.IsAny<string>()))
               .Returns(false);
            mockApiContext.Setup(_ => _.GetCars())
              .Returns(new List<CarStock>());

            var controller = new CarsController(mockApiContext.Object, mockCarManagement.Object, mockValidation.Object, mockILogger.Object);

            //act
            var result = controller.SearchStock(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }
        [Fact]
        public void SearchStock_Should_Return_ValidStock()
        {
            //arrange 
            var mockILogger = new Mock<ILogger<CarsController>>();
            var mockCarManagement = new Mock<ICarManagement>();
            var mockValidation = new Mock<IValidation>();
            var mockApiContext = new Mock<ApiContext>(dbContextOptions);

            mockValidation.Setup(_ => _.ValidateStringParameter(It.IsAny<string>()))
               .Returns(true);
            mockApiContext.Setup(_ => _.GetCars())
              .Returns(new List<CarStock>());
            mockCarManagement.Setup(_ => _.SearchStock(It.IsAny<List<CarStock>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
              .Returns(It.IsAny<SearchCarResponse>());

            var controller = new CarsController(mockApiContext.Object, mockCarManagement.Object, mockValidation.Object, mockILogger.Object);

            //act
            var result = controller.SearchStock(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void AddCar_Should_Return_ValidResult()
        {
            //arrange 
            var mockILogger = new Mock<ILogger<CarsController>>();
            var mockCarManagement = new Mock<ICarManagement>();
            var mockValidation = new Mock<IValidation>();
            var mockApiContext = new Mock<ApiContext>(dbContextOptions);

            mockValidation.Setup(_ => _.ValidateCarRequest(It.IsAny<NewCarRequest>()))
               .Returns(true);
            mockApiContext.Setup(_ => _.AddCar(It.IsAny<NewCarRequest>()))
              .Returns(true);

            var controller = new CarsController(mockApiContext.Object, mockCarManagement.Object, mockValidation.Object, mockILogger.Object);

            //act
            var result = controller.AddCar(It.IsAny<NewCarRequest>());

            //assert
            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public void RemoveCar_Should_Return_ValidResult()
        {
            //arrange 
            var mockILogger = new Mock<ILogger<CarsController>>();
            var mockCarManagement = new Mock<ICarManagement>();
            var mockValidation = new Mock<IValidation>();
            var mockApiContext = new Mock<ApiContext>(dbContextOptions);

            mockValidation.Setup(_ => _.ValidateCarRequest(It.IsAny<RemoveCarRequest>()))
               .Returns(true);
            mockApiContext.Setup(_ => _.RemoveCar(It.IsAny<RemoveCarRequest>()))
              .Returns(true);

            var controller = new CarsController(mockApiContext.Object, mockCarManagement.Object, mockValidation.Object, mockILogger.Object);

            //act
            var result = controller.RemoveCar(It.IsAny<RemoveCarRequest>());

            //assert
            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public void UpdateStock_Should_Return_BadResult_WhenNoStockFound()
        {
            //arrange 
            var mockILogger = new Mock<ILogger<CarsController>>();
            var mockCarManagement = new Mock<ICarManagement>();
            var mockValidation = new Mock<IValidation>();
            var mockApiContext = new Mock<ApiContext>(dbContextOptions);

            mockValidation.Setup(_ => _.ValidateIntParameter(It.IsAny<int>()))
               .Returns(true);
            mockValidation.Setup(_ => _.ValidateStringParameter(It.IsAny<string>()))
              .Returns(true);
            mockApiContext.Setup(_ => _.UpdateStock(It.IsAny<UpdateStockRequest>()))
              .Returns((CarStock)null);

            var controller = new CarsController(mockApiContext.Object, mockCarManagement.Object, mockValidation.Object, mockILogger.Object);

            //act
            var result = controller.UpdateStock(new UpdateStockRequest());

            //assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
            var badResult = result as BadRequestObjectResult;
            badResult.Value.ToString().Should().Contain("Stock not Found");
        }
        [Fact]
        public void UpdateStock_Should_Return_ValidResult()
        {
            //arrange 
            var mockILogger = new Mock<ILogger<CarsController>>();
            var mockCarManagement = new Mock<ICarManagement>();
            var mockValidation = new Mock<IValidation>();
            var mockApiContext = new Mock<ApiContext>(dbContextOptions);

            mockValidation.Setup(_ => _.ValidateIntParameter(It.IsAny<int>()))
               .Returns(true);
            mockValidation.Setup(_ => _.ValidateStringParameter(It.IsAny<string>()))
              .Returns(true);
            mockApiContext.Setup(_ => _.UpdateStock(It.IsAny<UpdateStockRequest>()))
              .Returns(new CarStock());

            var controller = new CarsController(mockApiContext.Object, mockCarManagement.Object, mockValidation.Object, mockILogger.Object);

            //act
            var result = controller.UpdateStock(new UpdateStockRequest());

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
