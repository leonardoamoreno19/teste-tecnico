using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById
{
    public class GetSaleByIdResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public List<GetSaleByIdItemResult> Items { get; set; }
    }

    public class GetSaleByIdItemResult
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
} 