using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleDTO
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
        public List<SaleItemDTO> Items { get; set; }
    }
} 