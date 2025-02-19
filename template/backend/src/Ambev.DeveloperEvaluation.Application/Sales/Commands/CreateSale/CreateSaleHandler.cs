using AutoMapper;
using MediatR;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.Messages;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreateSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper,
            IBus bus)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Sale>(request);

            foreach (var itemCommand in request.Items)
            {
                var item = _mapper.Map<SaleItem>(itemCommand);
                sale.AddItem(item);
            }

            var result = await _saleRepository.AddAsync(sale);

            var saleCreatedMessage = SaleCreatedMessage.For(result.Id);

            await _bus.Subscribe<SaleCreatedMessage>(); 
            await _bus.Publish(saleCreatedMessage);

            return _mapper.Map<CreateSaleResult>(result);
        }
    }
}