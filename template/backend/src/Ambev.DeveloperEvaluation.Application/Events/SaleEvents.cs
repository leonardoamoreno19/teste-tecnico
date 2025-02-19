using System;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    public record SaleCreatedEvent(Guid SaleId) : INotification;
    public record SaleModifiedEvent(Guid SaleId) : INotification;
    public record SaleCancelledEvent(Guid SaleId) : INotification;
    public record SaleDeletedEvent(Guid SaleId) : INotification;
    public record ItemCancelledEvent(Guid SaleId, Guid ItemId) : INotification;
} 