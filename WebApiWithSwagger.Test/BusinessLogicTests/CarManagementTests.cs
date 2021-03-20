using FluentAssertions;
using WebApiWithSwagger.BusinessLogic;
using Xunit;

namespace WebApiWithSwagger.Test.BusinessLogicTests
{
    public class CarManagementTests
    {
        private readonly CarManagement _testee;

        public CarManagementTests()
        {
            _testee = new CarManagement();
        }

        [Fact]
        public void GetAllStock_With_WrongDealerCode_Should_ReturnEmptyArray()
        {
            //arrange
            var cars = TestingData.GetCarStock();

            //act
            var result = _testee.GetAllCarsStock(cars, "A55");

            //assert
            result.Should().NotBeNull();
            result.CarStocks.Length.Should().Be(0);
        }

        [Theory]
        [InlineData("A01", 3)]
        [InlineData("a01", 3)]
        [InlineData("B02", 4)]
        [InlineData("b02", 4)]
        public void GetAllStock_Should_Be_CaseInsensitive(string dealerCode, int CarStockCount)
        {
            //arrange
            var cars = TestingData.GetCarStock();

            //act
            var result = _testee.GetAllCarsStock(cars, dealerCode);

            //assert
            result.Should().NotBeNull();
            result.CarStocks.Length.Should().Be(CarStockCount);
        }

        [Theory]
        [InlineData("A01", "Audi", "RS7", 1)]
        [InlineData("B02", "BMW", "330i", 2)]
        public void SearchStock_Should_Return_ValidResults(string dealerCode, string make, string model, int carStockCount)
        {
            //arrange
            var cars = TestingData.GetCarStock();

            //act
            var result = _testee.SearchStock(cars, dealerCode, make, model);

            //assert
            result.Should().NotBeNull();
            result.CarStocks.Length.Should().Be(carStockCount);
        }

        [Theory]
        [InlineData("A01", "Audi", "RS7", 1)]
        [InlineData("A01", "AUDI", "RS7", 1)]
        [InlineData("A01", "AUDI", "rs7", 1)]
        public void SearchStock_Should_Be_CaseInsensitive(string dealerCode, string make, string model, int carStockCount)
        {
            //arrange
            var cars = TestingData.GetCarStock();

            //act
            var result = _testee.SearchStock(cars, dealerCode, make, model);

            //assert
            result.Should().NotBeNull();
            result.CarStocks.Length.Should().Be(carStockCount);
        }


        [Theory]
        [InlineData("A01", "Audi", "R8", 0)]
        public void SearchStock_Should_ReturnEmptyArray_WhenNoMatch(string dealerCode, string make, string model, int carStockCount)
        {
            //arrange
            var cars = TestingData.GetCarStock();

            //act
            var result = _testee.SearchStock(cars, dealerCode, make, model);

            //assert
            result.Should().NotBeNull();
            result.CarStocks.Length.Should().Be(carStockCount);
        }
    }
}
