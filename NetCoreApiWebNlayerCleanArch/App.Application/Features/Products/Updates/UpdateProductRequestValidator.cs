using FluentValidation;

namespace App.Application.Features.Products.Updates;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .Length(3, 20).WithMessage("Product name should be between 3 and 20 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");

        RuleFor(x => x.Stock)
            .InclusiveBetween(-1, 100).WithMessage("Product stock should be between -1 and 100.");

        RuleFor(x => x.categoryId)
            .GreaterThan(0).WithMessage("Category value must be greater than 0.");
    }
}

