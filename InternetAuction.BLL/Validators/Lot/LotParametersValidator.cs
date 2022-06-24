using FluentValidation;
using InternetAuction.BLL.Models.Lot;

namespace InternetAuction.BLL.Validators.Lot
{
    public class LotParametersValidator : AbstractValidator<LotParameters>
    {
        public LotParametersValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than zero");

            RuleFor(p => p.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Min price must be non negative");

            RuleFor(p => p.MaxPrice)
                .GreaterThan(0).When(p => p.MaxPrice.HasValue)
                .WithMessage("Max price must be greater than 0");
        }
    }
}