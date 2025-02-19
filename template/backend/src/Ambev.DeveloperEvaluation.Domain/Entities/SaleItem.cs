using System;
using Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } // Denormalized
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public Sale Sale { get; private set; }

    protected SaleItem() { }

    public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CalculateTotalAmount();
    }

    public void ApplyDiscount(decimal discountPercentage)
    {
        DiscountPercentage = discountPercentage;
        CalculateTotalAmount();
    }

    public void Cancel()
    {
        IsCancelled = true;
        UpdatedAt = DateTime.UtcNow;
    }

    private void CalculateTotalAmount()
    {
        var subtotal = Quantity * UnitPrice;
        var discount = subtotal * DiscountPercentage;
        TotalAmount = subtotal - discount;
    }
} 