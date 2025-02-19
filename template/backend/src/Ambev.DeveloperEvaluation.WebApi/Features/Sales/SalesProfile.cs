using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.Messages;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
            CreateMap<GetSaleByIdResult, SaleResponse>();
            CreateMap<GetSaleByIdItemResult, SaleItemResponse>();
            CreateMap<CreateSaleCommand, CreateSaleMessage>();
            CreateMap<CreateSaleItemCommand, CreateSaleItemMessage>();
        }
    }
} 