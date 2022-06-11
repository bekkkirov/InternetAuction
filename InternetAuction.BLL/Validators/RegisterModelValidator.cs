using FluentValidation;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("User name is required")
                .MinimumLength(5).WithMessage("User name must be at least 5 characters long")
                .MaximumLength(20).WithMessage("User name can't exceed 20 characters");

            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long")
                .MaximumLength(30).WithMessage("First name can't exceed 30 characters");
            
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Last name is required")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long")
                .MaximumLength(30).WithMessage("Last name can't exceed 30 characters");

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(5).WithMessage("Password must be at least 5 characters long")
                .MaximumLength(20).WithMessage("Password can't exceed 20 characters");
        }
    }
}