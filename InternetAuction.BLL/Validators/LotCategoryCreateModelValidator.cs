using FluentValidation;
using InternetAuction.BLL.Models.Lot;

namespace InternetAuction.BLL.Validators
{
    public class LotCategoryCreateModelValidator : AbstractValidator<LotCategoryCreateModel>
    {
        public LotCategoryCreateModelValidator()
        {
            RuleFor(c => c.Name)
                .MinimumLength(3).WithMessage("Category name must be at least 2 characters long")
                .MaximumLength(30).WithMessage("Category name can't exceed 30 characters");
        }
    }
}