using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Validation;


namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; } // Denormalized
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; } // Denormalized
        public decimal TotalAmount { get; private set; }
        public bool IsCancelled { get; private set; }
        public ICollection<SaleItem> Items { get; private set; }

        protected Sale() { }

        public Sale(string saleNumber, Guid customerId, string customerName, 
                    Guid branchId, string branchName)
        {
            SaleNumber = saleNumber;
            SaleDate = DateTime.UtcNow;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
            Items = new List<SaleItem>();
            IsCancelled = false;
            CalculateTotalAmount();
        }

        public void AddItem(SaleItem item)
        {
            ValidateItemQuantity(item);
            ApplyDiscountRules(item);
            Items.Add(item);
            CalculateTotalAmount();
        }

        public void Cancel()
        {
            IsCancelled = true;
            UpdatedAt = DateTime.UtcNow;
        }

        private void ValidateItemQuantity(SaleItem item)
        {
            if (item.Quantity > 20)
                throw new DomainException("Maximum quantity per product is 20 units.");
        }

        private void ApplyDiscountRules(SaleItem item)
        {
            if (item.Quantity >= 10 && item.Quantity <= 20)
                item.ApplyDiscount(0.20m); // 20% discount
            else if (item.Quantity >= 4)
                item.ApplyDiscount(0.10m); // 10% discount
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = Items.Sum(item => item.TotalAmount);
        }
    }
} 