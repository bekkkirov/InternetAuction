using FluentValidation;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(l => l.UserName)
                .NotEmpty();

            RuleFor(l => l.Password)
                .NotEmpty();
        }
    }
}