using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.Messages
{
    public class CreateSaleMessage
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public List<CreateSaleItemMessage> Items { get; set; }
    }

    public class CreateSaleItemMessage
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 