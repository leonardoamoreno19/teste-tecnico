using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;

        public CancelSaleHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with id {request.Id} not found");

            sale.Cancel();
            await _saleRepository.UpdateAsync(sale);
            await _mediator.Publish(new SaleCancelledEvent(request.Id), cancellationToken);

            return Unit.Value;
        }
    }
} 