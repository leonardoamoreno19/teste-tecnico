using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Ambev.DeveloperEvaluation.Application.Sales.Messages;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class SaleCreatedHandler : IHandleMessages<SaleCreatedMessage>
    {
        private readonly ILogger<SaleCreatedHandler> _logger;

        public SaleCreatedHandler(ILogger<SaleCreatedHandler> logger)
        {
            _logger = logger;            
        }

        public async Task Handle(SaleCreatedMessage message)
        {
            try
            {
                _logger.LogInformation(
                    "Sale Event created successfully: {SaleId} at {CreatedAt}", 
                    message.SaleId,
                    message.CreatedAt);

                await Task.CompletedTask;                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing SaleCreatedMessage");
                throw;
            }
        }
    }
} 