using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.Events;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SaleService(ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<SaleDTO> GetByIdAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task<SaleDTO> GetByNumberAsync(string saleNumber)
        {
            var sale = await _saleRepository.GetByNumberAsync(saleNumber);
            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task<IEnumerable<SaleDTO>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }

        public async Task<IEnumerable<SaleDTO>> GetByCustomerIdAsync(Guid customerId)
        {
            var sales = await _saleRepository.GetByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }

        public async Task<IEnumerable<SaleDTO>> GetByBranchIdAsync(Guid branchId)
        {
            var sales = await _saleRepository.GetByBranchIdAsync(branchId);
            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }

        public async Task<IEnumerable<SaleDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }

        public async Task<SaleDTO> CreateAsync(SaleDTO saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            var result = await _saleRepository.AddAsync(sale);
            
            // Publicar evento de criação
            await _mediator.Publish(new SaleCreatedEvent(result.Id));
            
            return _mapper.Map<SaleDTO>(result);
        }

        public async Task UpdateAsync(SaleDTO saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            await _saleRepository.UpdateAsync(sale);
            
            // Publicar evento de modificação
            await _mediator.Publish(new SaleModifiedEvent(sale.Id));
        }

        public async Task CancelAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            sale.Cancel();
            await _saleRepository.UpdateAsync(sale);
            
            // Publicar evento de cancelamento
            await _mediator.Publish(new SaleCancelledEvent(id));
        }

        public async Task CancelItemAsync(Guid saleId, Guid itemId)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
            var item = sale.Items.FirstOrDefault(i => i.Id == itemId);
            
            if (item != null)
            {
                item.Cancel();
                await _saleRepository.UpdateAsync(sale);
                
                // Publicar evento de cancelamento de item
                await _mediator.Publish(new ItemCancelledEvent(saleId, itemId));
            }
        }
    }
} 