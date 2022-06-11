using FluentValidation;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(l => l.UserName)
                .NotEmpty().WithMessage("User name is required");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}