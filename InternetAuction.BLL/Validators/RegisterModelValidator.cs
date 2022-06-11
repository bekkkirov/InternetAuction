using FluentValidation;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30);
            
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30);

            RuleFor(r => r.Email)
                .EmailAddress();
        }
    }
}