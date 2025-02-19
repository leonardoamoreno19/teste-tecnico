using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    public class SaleEventHandler : 
        INotificationHandler<SaleCreatedEvent>,
        INotificationHandler<SaleModifiedEvent>,
        INotificationHandler<SaleCancelledEvent>,
        INotificationHandler<SaleDeletedEvent>,
        INotificationHandler<ItemCancelledEvent>
    {
        private readonly ILogger<SaleEventHandler> _logger;

        public SaleEventHandler(ILogger<SaleEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sale created: {SaleId}", notification.SaleId);
            return Task.CompletedTask;
        }

        public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sale modified: {SaleId}", notification.SaleId);
            return Task.CompletedTask;
        }

        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sale cancelled: {SaleId}", notification.SaleId);
            return Task.CompletedTask;
        }

        public Task Handle(SaleDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sale deleted: {SaleId}", notification.SaleId);
            return Task.CompletedTask;
        }

        public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Item cancelled: {ItemId} from sale {SaleId}", 
                notification.ItemId, notification.SaleId);
            return Task.CompletedTask;
        }
    }
} 