using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Ambev.DeveloperEvaluation.Application.Sales.Messages;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class CreateSaleMessageHandler : IHandleMessages<CreateSaleMessage>
    {
        private readonly ILogger<CreateSaleMessageHandler> _logger;

        public CreateSaleMessageHandler(ILogger<CreateSaleMessageHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(CreateSaleMessage message)
        {
            _logger.LogInformation(
                "Processing sale creation: {SaleId}, Customer: {CustomerName}, Items: {ItemCount}", 
                message.Id, 
                message.CustomerName,
                message.Items.Count);

            // Aqui você pode adicionar lógicas adicionais como:
            // - Notificar sistemas externos
            // - Gerar relatórios
            // - Atualizar cache
            // - Enviar emails
            // etc.

            await Task.CompletedTask;
        }
    }
} 