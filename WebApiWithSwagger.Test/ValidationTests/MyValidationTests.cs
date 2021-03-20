using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiWithSwagger.Models;
using WebApiWithSwagger.Validation;
using Xunit;

namespace WebApiWithSwagger.Test.ValidationTests
{
    public class MyValidationTests
    {
        private readonly MyValidation _testee;

        public MyValidationTests()
        {
            _testee = new MyValidation();
        }

        [Theory]
        [InlineData("A01", true)]
        [InlineData("a02", true)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        public void StringValidation_Should_ReturnCorrectResult(string inputString, bool output)
        {
            //arrange & act
            var result = _testee.ValidateStringParameter(inputString);

            //assert
            result.Should().Be(output);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(15, true)]
        [InlineData(-1, false)]
        [InlineData(-100, false)]
        public void IntValidation_Should_ReturnCorrectResult(int inputNumber, bool output)
        {
            //arrange & act
            var result = _testee.ValidateIntParameter(inputNumber);

            //assert
            result.Should().Be(output);
        }

        [Fact]
        public void CarRequestValidation_Should_ReturnTrue_On_CorrectInput()
        {
            //arrange
            var carRequestObject = new CarRequest() { DealerCode = "B02", Make = "Audi", Model = "A4", Year = 2020 };
            //act
            var result = _testee.ValidateCarRequest(carRequestObject);

            //assert
            result.Should().Be(true);
        }

        [Fact]
        public void CarRequestValidation_Should_ReturnFalse_On_IncorrectInput()
        {
            //arrange
            var carRequestObject = new CarRequest() { DealerCode = "B02", Make = "  ", Model = "A4", Year = 2020 };
            //act
            var result = _testee.ValidateCarRequest(carRequestObject);

            //assert
            result.Should().Be(false);
        }
    }
}
