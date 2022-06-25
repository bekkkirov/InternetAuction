﻿using FluentValidation;
using InternetAuction.BLL.Models.User;

namespace InternetAuction.BLL.Validators.User
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