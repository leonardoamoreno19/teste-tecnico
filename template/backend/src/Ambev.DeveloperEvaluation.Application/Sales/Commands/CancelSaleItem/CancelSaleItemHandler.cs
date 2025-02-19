using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;

        public CancelSaleItemHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with id {request.SaleId} not found");

            var item = sale.Items.FirstOrDefault(i => i.Id == request.ItemId);
            if (item == null)
                throw new KeyNotFoundException($"Item with id {request.ItemId} not found in sale {request.SaleId}");

            item.Cancel();
            await _saleRepository.UpdateAsync(sale);
            await _mediator.Publish(new ItemCancelledEvent(request.SaleId, request.ItemId), cancellationToken);

            return Unit.Value;
        }
    }
} 