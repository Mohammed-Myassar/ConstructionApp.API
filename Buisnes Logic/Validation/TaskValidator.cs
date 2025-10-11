using BuisnesLogic.Data_Transfer_Object;
using FluentValidation;

namespace Buisnes_Logic.Validation
{
    public class TaskValidator : AbstractValidator<ProjectTaskDTO>
    {
        public TaskValidator()
        {
            RuleFor(task => task.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(task => task.Description)
                 .NotEmpty()
                 .WithMessage("Description is required");

            RuleFor(task => task.StartDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(
                """
                Start date is less than current time,
                must be greater than or equal current time
                """);

            RuleFor(task => task.EndDate)
                .GreaterThan(dt => dt.StartDate)
                .WithMessage(
                """
                End date is less than or equal start date,
                must be greater than start date
                """);

            RuleFor(task => task.Status)
                .IsInEnum()
                .WithMessage("Value input to entry Status not correct");
        }
    }
}
