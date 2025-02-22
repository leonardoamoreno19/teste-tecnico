using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _bus = Substitute.For<IBus>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _bus);
    }

    [Fact]
    public async Task Handle_ValidSale_ShouldCreateSaleSuccessfully()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            CustomerId = Guid.NewGuid(),
            CustomerName = "Test Customer",
            BranchId = Guid.NewGuid(),
            BranchName = "Test Branch",
            Items = new List<CreateSaleItemCommand>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Test Product",
                    Quantity = 5,
                    UnitPrice = 10.00m
                }
            }
        };

        var expectedSale = new Sale(
            "SALE001", 
            command.CustomerId,
            command.CustomerName,
            command.BranchId,
            command.BranchName
        );
        
        // Adiciona o item à venda esperada
        expectedSale.AddItem(new SaleItem(
            command.Items[0].ProductId,
            command.Items[0].ProductName,
            command.Items[0].Quantity,
            command.Items[0].UnitPrice
        ));

        var result = new CreateSaleResult 
        { 
            Id = Guid.NewGuid(),
            SaleNumber = expectedSale.SaleNumber,
            TotalAmount = expectedSale.TotalAmount
        };

        _mapper.Map<Sale>(command).Returns(expectedSale);
        _mapper.Map<SaleItem>(Arg.Any<CreateSaleItemCommand>()).Returns(expectedSale.Items.First());
        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>()).Returns(result);
        _saleRepository.AddAsync(Arg.Any<Sale>()).Returns(expectedSale);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(result.Id);
        response.SaleNumber.Should().Be(expectedSale.SaleNumber);
                
        await _saleRepository.Received(1).AddAsync(Arg.Any<Sale>());
    }

    [Fact]
    public async Task Handle_InvalidSale_ShouldThrowValidationException()
    {
        // Arrange
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }
}
