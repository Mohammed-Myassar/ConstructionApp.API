using BuisnesLogic.Data_Transfer_Object;
using FluentValidation;

namespace Buisnes_Logic.Validation
{
    public class ResourceUsageValidator : AbstractValidator<ResourceUsageDTO>
    {
        public ResourceUsageValidator()
        {
            RuleFor(usage => usage.QuantityUsed)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity used is less 0");

            RuleFor(usage => usage.UsageDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(
                """
                Usage date is less than current time,
                must be greater than or equal current time
                """);
        }
    }
}