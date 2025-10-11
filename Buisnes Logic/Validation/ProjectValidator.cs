using BuisnesLogic.Data_Transfer_Object;
using FluentValidation;

namespace BuisnesLogic.Validation
{
    public class ProjectValidator : AbstractValidator<ConstructionProjectDTO>
    {
        public ProjectValidator ()
        {
            RuleFor(project => project.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(project => project.Budget)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Budget is less 0");

            RuleFor(project => project.StartDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(
                """
                Start date is less than current time,
                must be greater than or equal current time
                """);

            RuleFor(project => project.EndDate)
                .GreaterThan(dt => dt.StartDate)
                .WithMessage(
                """
                End date is less than or equal start date,
                must be greater than start date
                """);

            RuleFor(project => project.Status)
                .IsInEnum()
                .WithMessage("Value input to entry Status not correct"); 
        }
    }
}
