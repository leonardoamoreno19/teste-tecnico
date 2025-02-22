using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteSaleHandler _handler;
    private readonly IMediator _mediator;
    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mediator = Substitute.For<IMediator>();

        _handler = new DeleteSaleHandler(_saleRepository, _mediator);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldDeleteSaleSuccessfully()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new DeleteSaleCommand();
        command.Id = saleId;

        var sale = new Sale("SALE001", Guid.NewGuid(), "Test Customer", Guid.NewGuid(), "Test Branch");
        
        _saleRepository.GetByIdAsync(saleId).Returns(sale);
        _saleRepository.DeleteAsync(command.Id).Returns(Task.FromResult(true));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).GetByIdAsync(saleId);
        await _saleRepository.Received(1).DeleteAsync(command.Id);
    }

    [Fact]
    public async Task Handle_NonExistentSale_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new DeleteSaleCommand();
        command.Id = Guid.NewGuid();
        _saleRepository.GetByIdAsync(command.Id).Returns((Sale?)null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await _saleRepository.Received(1).GetByIdAsync(command.Id);
        await _saleRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
    }

    [Fact]
    public async Task Handle_InvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new DeleteSaleCommand();
        command.Id = Guid.NewGuid();
        _saleRepository.GetByIdAsync(command.Id).Returns((Sale?)null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await _saleRepository.Received(1).GetByIdAsync(command.Id);
        await _saleRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
    }
}