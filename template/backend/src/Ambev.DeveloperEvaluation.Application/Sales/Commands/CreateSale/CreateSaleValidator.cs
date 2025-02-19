using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.BranchId)
                .NotEmpty();

            RuleFor(x => x.BranchName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("At least one item is required");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId)
                    .NotEmpty();

                item.RuleFor(x => x.ProductName)
                    .NotEmpty()
                    .MaximumLength(200);

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(20)
                    .WithMessage("Quantity must be between 1 and 20");

                item.RuleFor(x => x.UnitPrice)
                    .GreaterThan(0);
            });
        }
    }
} 