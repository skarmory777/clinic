using System;
using Domain.Entities;
using Xunit;

namespace UnitTests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_ValidData_ShouldCreateSuccessfully()
        {
            // Arrange
            var sku = "SKU-001";
            var name = "Laptop Dell XPS 15";
            var unitPrice = 1999.99m;
            var stockQuantity = 10;

            // Act
            var product = new Product(sku, name, unitPrice, stockQuantity);

            // Assert
            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.Equal(sku, product.Sku);
            Assert.Equal(name, product.Name);
            Assert.Equal(unitPrice, product.UnitPrice);
            Assert.Equal(stockQuantity, product.StockQuantity);
            Assert.Equal(0.00m, product.TaxRate);
            Assert.Equal("unit", product.UnitOfMeasure);
            Assert.Equal(0, product.MinStockLevel);
            Assert.True(product.IsActive);
            Assert.True(product.CreatedAt > DateTime.UtcNow.AddSeconds(-5));
        }

        [Fact]
        public void CreateProduct_NegativePrice_ShouldThrowException()
        {
            // Arrange
            var sku = "SKU-001";
            var name = "Product";
            var unitPrice = -10m;
            var stockQuantity = 5;

            // Act & Assert
            Assert.Throws<DomainException>(() => 
                new Product(sku, name, unitPrice, stockQuantity));
        }

        [Fact]
        public void IncreaseStock_ValidQuantity_ShouldIncreaseStock()
        {
            // Arrange
            var product = new Product("SKU-001", "Product", 100m, 10);
            var initialStock = product.StockQuantity;

            // Act
            product.IncreaseStock(5);

            // Assert
            Assert.Equal(initialStock + 5, product.StockQuantity);
            Assert.NotNull(product.UpdatedAt);
        }

        [Fact]
        public void DecreaseStock_ValidQuantity_ShouldDecreaseStock()
        {
            // Arrange
            var product = new Product("SKU-001", "Product", 100m, 10);
            var initialStock = product.StockQuantity;

            // Act
            product.DecreaseStock(3);

            // Assert
            Assert.Equal(initialStock - 3, product.StockQuantity);
        }

        [Fact]
        public void DecreaseStock_InsufficientStock_ShouldThrowException()
        {
            // Arrange
            var product = new Product("SKU-001", "Product", 100m, 5);

            // Act & Assert
            Assert.Throws<DomainException>(() => product.DecreaseStock(10));
        }

        [Fact]
        public void CalculatePriceWithTax_ShouldReturnCorrectValue()
        {
            // Arrange
            var product = new Product("SKU-001", "Product", 100m, 10, taxRate: 10m);

            // Act
            var priceWithTax = product.CalculatePriceWithTax();

            // Assert
            Assert.Equal(110m, priceWithTax);
        }

        [Fact]
        public void IsLowStock_WhenBelowMinLevel_ShouldReturnTrue()
        {
            // Arrange
            var product = new Product("SKU-001", "Product", 100m, 5, minStockLevel: 10);

            // Act & Assert
            Assert.True(product.IsLowStock());
        }

        [Fact]
        public void CanSell_WhenInactive_ShouldReturnFalse()
        {
            // Arrange
            var product = new Product("SKU-001", "Product", 100m, 10);
            product.Deactivate();

            // Act & Assert
            Assert.False(product.CanSell(5));
        }
    }
}