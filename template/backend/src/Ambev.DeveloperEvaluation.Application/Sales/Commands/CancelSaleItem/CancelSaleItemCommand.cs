using System;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem
{
    public class CancelSaleItemCommand : IRequest<Unit>
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
    }
} 