using BuisnesLogic.Data_Transfer_Object;
using FluentValidation;

namespace Buisnes_Logic.Validation
{
    public class ResourceValidator : AbstractValidator<ResourceDTO>
    {
        public ResourceValidator()
        {
            RuleFor(resource => resource.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(resource => resource.Description)
                 .NotEmpty()
                 .WithMessage("Description is required");

            RuleFor(resource => resource.UnitCost)
                .IsInEnum()
                .WithMessage("Value input to entry UnitCost not correct");

            RuleFor(resource => resource.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity is less 0");
        }
    }
}
