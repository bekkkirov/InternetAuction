using FluentValidation;
using InternetAuction.BLL.Models.Bid;

namespace InternetAuction.BLL.Validators
{
    public class BidCreateModelValidator : AbstractValidator<BidCreateModel>
    {
        public BidCreateModelValidator()
        {
            RuleFor(b => b.BidValue)
                .GreaterThan(0).WithMessage("Bid value must be greater than zero");

            RuleFor(b => b.BidderUserName)
                .NotEmpty().WithMessage("Bidder user name is required");

            RuleFor(b => b.LotId)
                .NotEmpty().WithMessage("Lot id is required");
        }
    }
}