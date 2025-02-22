using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleByIdHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleByIdHandler _handler;

    public GetSaleByIdHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleByIdHandler(_saleRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ExistingSale_ShouldReturnSaleDetails()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var query = new GetSaleByIdQuery();
        query.Id = saleId;
        
        var sale = new Sale("SALE001", Guid.NewGuid(), "Test Customer", Guid.NewGuid(), "Test Branch");
        var expectedResult = new GetSaleByIdResult 
        { 
            Id = saleId,
            SaleNumber = "SALE001",
            CustomerName = "Test Customer",
            BranchName = "Test Branch"
        };

        _saleRepository.GetByIdAsync(saleId).Returns(sale);
        _mapper.Map<GetSaleByIdResult>(sale).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(saleId);
        result.SaleNumber.Should().Be("SALE001");
        await _saleRepository.Received(1).GetByIdAsync(saleId);
    }

    [Fact]
    public async Task Handle_NonExistentSale_ShouldReturnNoItems()
    {
        // Arrange
        var query = new GetSaleByIdQuery();
        query.Id = Guid.NewGuid();
        _saleRepository.GetByIdAsync(query.Id).Returns((Sale?)null);

        // Act
        var act = await _handler.Handle(query, CancellationToken.None);

        // Assert that the operation throws when sale is not found
        act.Should().BeNull();
        await _saleRepository.Received(1).GetByIdAsync(query.Id);
    }
}
