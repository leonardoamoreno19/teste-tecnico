using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void AddItem_ValidQuantity_ShouldAddItemSuccessfully()
    {
        // Arrange
        var sale = new Sale(
            "SALE001",
            Guid.NewGuid(),
            "Test Customer",
            Guid.NewGuid(),
            "Test Branch"
        );
        var item = new SaleItem(
            Guid.NewGuid(),
            "Test Product",
            5,
            10.00m
        );

        // Act
        sale.AddItem(item);

        // Assert
        sale.Items.Should().Contain(item);
        sale.TotalAmount.Should().Be(45.00m);
    }

    [Fact]
    public void AddItem_QuantityGreaterThan20_ShouldThrowException()
    {
        // Arrange
        var sale = new Sale(
            "SALE001",
            Guid.NewGuid(),
            "Test Customer",
            Guid.NewGuid(),
            "Test Branch"
        );
        var item = new SaleItem(
            Guid.NewGuid(),
            "Test Product",
            21,
            10.00m
        );

        // Act
        var act = () => sale.AddItem(item);

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Maximum quantity per product is 20 units.");
    }

    [Theory]
    [InlineData(4, 0.10)] // 10% discount for 4-9 items
    [InlineData(10, 0.20)] // 20% discount for 10-20 items
    public void AddItem_QuantityWithDiscount_ShouldApplyCorrectDiscount(int quantity, decimal expectedDiscount)
    {
        // Arrange
        var sale = new Sale(
            "SALE001",
            Guid.NewGuid(),
            "Test Customer",
            Guid.NewGuid(),
            "Test Branch"
        );
        var item = new SaleItem(
            Guid.NewGuid(),
            "Test Product",
            quantity,
            10.00m
        );

        // Act
        sale.AddItem(item);

        // Assert
        item.DiscountPercentage.Should().Be(expectedDiscount);
    }

    [Fact]
    public void Cancel_ActiveSale_ShouldMarkAsCancelled()
    {
        // Arrange
        var sale = new Sale(
            "SALE001",
            Guid.NewGuid(),
            "Test Customer",
            Guid.NewGuid(),
            "Test Branch"
        );

        // Act
        sale.Cancel();

        // Assert
        sale.IsCancelled.Should().BeTrue();
    }
}
