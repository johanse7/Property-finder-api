using FluentValidation;

public class PropertiesQueryValidator : AbstractValidator<PropertiesRequest>
{
    public PropertiesQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue);

        RuleFor(x => x.MaxPrice)
            .GreaterThan(0).When(x => x.MaxPrice.HasValue)
            .GreaterThanOrEqualTo(x => x.MinPrice ?? 0).WithMessage("Maximum price cannot be less than minimum price");
    }
}
