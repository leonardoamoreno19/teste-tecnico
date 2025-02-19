using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with id {request.Id} not found");

            // Atualiza as propriedades básicas
            _mapper.Map(request, sale);

            // Remove itens que não estão mais presentes
            sale.Items.Clear();

            // Adiciona os novos itens
            foreach (var itemCommand in request.Items)
            {
                var item = _mapper.Map<SaleItem>(itemCommand);
                sale.AddItem(item);
            }

            await _saleRepository.UpdateAsync(sale);
            await _mediator.Publish(new SaleModifiedEvent(sale.Id), cancellationToken);

            return _mapper.Map<UpdateSaleResult>(sale);
        }
    }
} 