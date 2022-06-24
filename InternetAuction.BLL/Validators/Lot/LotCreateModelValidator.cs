using System;
using FluentValidation;
using InternetAuction.BLL.Models.Lot;

namespace InternetAuction.BLL.Validators.Lot
{
    public class LotCreateModelValidator : AbstractValidator<LotCreateModel>
    {
        public LotCreateModelValidator()
        {
            RuleFor(l => l.Name)
                .NotEmpty().WithMessage("Lot name is required")
                .MinimumLength(5).WithMessage("Lot name must be at least 5 characters long")
                .MaximumLength(30).WithMessage("Lot name can't exceed 30 characters");

            RuleFor(l => l.Description)
                .NotEmpty().WithMessage("Lot description is required")
                .MinimumLength(10).WithMessage("Lot description must be at least 10 characters long")
                .MaximumLength(250).WithMessage("Lot description can't exceed 30 characters");

            RuleFor(l => l.InitialPrice)
                .GreaterThan(0).WithMessage("Lot price must be greater than zero");

            RuleFor(l => l.SaleEndTime)
                .GreaterThan(DateTime.Now).WithMessage("Sale end time is already passed");

            RuleFor(l => l.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");
            
            RuleFor(l => l.CategoryId)
                .GreaterThan(0).WithMessage("Category id must be greater than zero");
        }
    }
}