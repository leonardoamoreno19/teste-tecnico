using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Models;
using Ambev.DeveloperEvaluation.WebApi.Controllers;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a sale by id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SaleResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetSaleByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return HandleResponse(result);
        }

        /// <summary>
        /// Create a new sale
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SaleResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
        {
            var command = new CreateSaleCommand
            {
                SaleNumber = request.SaleNumber,
                CustomerId = request.CustomerId,
                CustomerName = request.CustomerName,
                BranchId = request.BranchId,
                BranchName = request.BranchName,
                Items = request.Items.ConvertAll(i => new CreateSaleItemCommand
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                })
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Update an existing sale
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(SaleResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSaleRequest request)
        {
            var command = new UpdateSaleCommand
            {
                Id = id,
                SaleNumber = request.SaleNumber,
                CustomerId = request.CustomerId,
                CustomerName = request.CustomerName,
                BranchId = request.BranchId,
                BranchName = request.BranchName,
                Items = request.Items.ConvertAll(i => new UpdateSaleItemCommand
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                })
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete a sale
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteSaleCommand { Id = id });
            return NoContent();
        }

        /// <summary>
        /// Cancel a sale
        /// </summary>
        [HttpPost("{id:guid}/cancel")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _mediator.Send(new CancelSaleCommand { Id = id });
            return NoContent();
        }

        /// <summary>
        /// Cancel a sale item
        /// </summary>
        [HttpPost("{saleId:guid}/items/{itemId:guid}/cancel")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CancelItem(Guid saleId, Guid itemId)
        {
            await _mediator.Send(new CancelSaleItemCommand { SaleId = saleId, ItemId = itemId });
            return NoContent();
        }
    }
} 