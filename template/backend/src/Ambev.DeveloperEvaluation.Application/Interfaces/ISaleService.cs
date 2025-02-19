using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDTO> GetByIdAsync(Guid id);
        Task<SaleDTO> GetByNumberAsync(string saleNumber);
        Task<IEnumerable<SaleDTO>> GetAllAsync();
        Task<IEnumerable<SaleDTO>> GetByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<SaleDTO>> GetByBranchIdAsync(Guid branchId);
        Task<IEnumerable<SaleDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<SaleDTO> CreateAsync(SaleDTO saleDto);
        Task UpdateAsync(SaleDTO saleDto);
        Task CancelAsync(Guid id);
        Task CancelItemAsync(Guid saleId, Guid itemId);
    }
} 