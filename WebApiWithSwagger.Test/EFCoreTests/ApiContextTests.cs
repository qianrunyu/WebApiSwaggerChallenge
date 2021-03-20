using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiWithSwagger.EFCore;
using WebApiWithSwagger.Models;
using Xunit;

namespace WebApiWithSwagger.Test.EFCoreTests
{
    public class ApiContextTests : IDisposable
    {
        private DbContextOptions<ApiContext> dbContextOptions = new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
        private ApiContext _testee;

        public ApiContextTests()
        {
            var context = new ApiContext(dbContextOptions);
            _testee = context;
            SeedDB();
        }
        public void Dispose()
        {
            _testee.Database.EnsureDeleted();
        }
        private void SeedDB()
        {
            _testee.Cars.AddRange(
             new CarStock
             {
                 Make = "AUDI",
                 Model = "RS5",
                 Year = 2021,
                 DealerCode = "A01",
                 StockLevel = 1
             },
             new CarStock
             {
                 Make = "AUDI",
                 Model = "RS7",
                 Year = 2021,
                 DealerCode = "A01",
                 StockLevel = 2
             },
             new CarStock
             {
                 Make = "BMW",
                 Model = "M3",
                 Year = 2020,
                 DealerCode = "A01",
                 StockLevel = 5
             },
             new CarStock
             {
                 Make = "BMW",
                 Model = "M3",
                 Year = 2020,
                 DealerCode = "B02",
                 StockLevel = 2
             },
             new CarStock
             {
                 Make = "BMW",
                 Model = "330i",
                 Year = 2015,
                 DealerCode = "B02",
                 StockLevel = 1
             },
             new CarStock
             {
                 Make = "BMW",
                 Model = "330i",
                 Year = 2018,
                 DealerCode = "B02",
                 StockLevel = 2
             },
              new CarStock
              {
                  Make = "AUDI",
                  Model = "A7",
                  Year = 1999,
                  DealerCode = "B02",
                  StockLevel = 10
              });
            _testee.SaveChanges();
        }

        [Fact]
        public void GetCars_Should_Return_AllCarStocks()
        {
            //arrange & act
            var result = _testee.GetCars();

            //assert
            result.Should().NotBeNull();
            result.Count().Should().Be(7);
            Dispose();
        }

        [Fact]
        public void AddCar_Should_Add_Car()
        {
            //arrange 
            var newCar = new NewCarRequest() { DealerCode = "A01", Make = "AUDI", Model = "R8", Year = 2018 };

            //act
            var result = _testee.AddCar(newCar);

            //assert
            result.Should().BeTrue();
            _testee.Cars.Count().Should().Be(8);
            Dispose();
        }
        [Theory]
        [InlineData("A01", "AUDI", "RS7", 2021, 3)]
        [InlineData("B02", "AUDI", "A7", 1999, 11)]
        public void AddCar_Should_IncreaseStockLevel_When_SameCarIsFound(string dealerCode, string make, string model, int year, int expectedStockLevel)
        {
            //arrange 
            var newCar = new NewCarRequest() { DealerCode = dealerCode, Make = make, Model = model, Year = year };

            //act
            var result = _testee.AddCar(newCar);

            //assert
            result.Should().BeTrue();
            _testee.Cars.Count().Should().Be(7);
            _testee.Cars.Where(x => x.DealerCode == dealerCode && x.Make == make && x.Model == model && x.Year == year).FirstOrDefault().StockLevel.Should().Be(expectedStockLevel);
            Dispose();
        }

        [Theory]
        [InlineData("A01", "AUDI", "RS7", 2021, 1)]
        [InlineData("B02", "AUDI", "A7", 1999, 9)]
        public void RemoveCar_Should_DecreaseStockLevel_When_SameCarIsFound(string dealerCode, string make, string model, int year, int expectedStockLevel)
        {
            //arrange 
            var newCar = new RemoveCarRequest() { DealerCode = dealerCode, Make = make, Model = model, Year = year };

            //act
            var result = _testee.RemoveCar(newCar);

            //assert
            result.Should().BeTrue();
            _testee.Cars.Count().Should().Be(7);
            _testee.Cars.Where(x => x.DealerCode == dealerCode && x.Make == make && x.Model == model && x.Year == year).FirstOrDefault().StockLevel.Should().Be(expectedStockLevel);
            Dispose();
        }

        [Fact]
        public void UpdateStock_Should_UpdateStockLevel_When_SameCarIsFound()
        {
            //arrange 
            var car = _testee.Cars.FirstOrDefault();
            var initialStockLevel = car.StockLevel;
            int newStockLevel = initialStockLevel + 10;
            var expectedStockLevel = newStockLevel;
            var updateStockRequest = new UpdateStockRequest() { StockId = car.Id, DealerCode = car.DealerCode, StockLevel = newStockLevel };

            //act
            var result = _testee.UpdateStock(updateStockRequest);

            //assert
            result.Should().NotBeNull();
            result.StockLevel.Should().Be(expectedStockLevel);
            Dispose();
        }

        [Fact]
        public void UpdateStock_Should_ReturnNull_When_CarIsNotFound()
        {
            //arrange 
            var car = _testee.Cars.FirstOrDefault();
            var initialStockLevel = car.StockLevel;
            int newStockLevel = initialStockLevel + 10;
            var updateStockRequest = new UpdateStockRequest() { StockId = car.Id, DealerCode = "Other Dealer", StockLevel = newStockLevel };

            //act
            var result = _testee.UpdateStock(updateStockRequest);

            //assert
            result.Should().BeNull();
            Dispose();
        }

    }
}
