using FluentValidation;
using InternetAuction.BLL.Models.Bid;

namespace InternetAuction.BLL.Validators.Bid
{
    public class BidCreateModelValidator : AbstractValidator<BidCreateModel>
    {
        public BidCreateModelValidator()
        {
            RuleFor(b => b.BidValue)
                .GreaterThan(0).WithMessage("Bid value must be greater than zero");
        }
    }
}