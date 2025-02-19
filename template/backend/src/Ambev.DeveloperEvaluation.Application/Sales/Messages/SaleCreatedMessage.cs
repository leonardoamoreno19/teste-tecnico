using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.Messages
{
    public class SaleCreatedMessage
    {
        public Guid SaleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public static SaleCreatedMessage For(Guid saleId)
        {
            return new SaleCreatedMessage
            {
                SaleId = saleId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
} 