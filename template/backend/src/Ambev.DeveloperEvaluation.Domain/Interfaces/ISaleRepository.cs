using Ambev.DeveloperEvaluation.Domain.Entities;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<Sale> GetByNumberAsync(string saleNumber);
    Task<IEnumerable<Sale>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<Sale>> GetByBranchIdAsync(Guid branchId);
    Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
} 