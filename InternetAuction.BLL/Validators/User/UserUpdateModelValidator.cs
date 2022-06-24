using FluentValidation;
using InternetAuction.BLL.Models.User;

namespace InternetAuction.BLL.Validators.User
{
    public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateModelValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long")
                .MaximumLength(30).WithMessage("First name can't exceed 30 characters");

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("Last name is required")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long")
                .MaximumLength(30).WithMessage("Last name can't exceed 30 characters");

            RuleFor(u => u.Balance)
                .GreaterThan(0).WithMessage("Balance must be greater than 0");
        }
    }
}