using BuisnesLogic.Data_Transfer_Object;
using FluentValidation;

namespace BuisnesLogic.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required");

            RuleFor(e => e.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required");

            RuleFor(e => e.Role)
                .IsInEnum()
                .WithMessage("Value input to entry Role not correct");
        }
    }
}
