using Xunit;
using ProductosMVC.BusinessLayer;

namespace ProductosMVC.Tests
{
    public class ProductBusinessLogicTests
    {
        [Fact]
        public void CalculateTax_ShouldReturnCorrectTax()
        {
            // Arrange
            var businessLogic = new ProductBusinessLogic();
            decimal price = 100m;
            decimal expectedTax = 19m; // 19% of 100

            // Act
            decimal actualTax = businessLogic.CalculateTax(price);

            // Assert
            Assert.Equal(expectedTax, actualTax);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 9.5)]
        [InlineData(200, 38)]
        public void CalculateTax_ShouldReturnCorrectTax_ForVariousPrices(decimal price, decimal expectedTax)
        {
            // Arrange
            var businessLogic = new ProductBusinessLogic();

            // Act
            decimal actualTax = businessLogic.CalculateTax(price);

            // Assert
            Assert.Equal(expectedTax, actualTax);
        }
    }
}
