using BuisnesLogic.Data_Transfer_Object;
using FluentValidation;

namespace BuisnesLogic.Validation
{
    public class PaymentTransactionValidator : AbstractValidator<PaymentTransactionDTO>
    {
        public PaymentTransactionValidator()
        {
            RuleFor(p => p.Type)
                .IsInEnum()
                .WithMessage("Value input to entry Type not correct");

            RuleFor(p => p.Amount)
                .GreaterThan(0)
                .WithMessage("Budget is less or equal 0");

            RuleFor(p => p.TransactionDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("""
                Transaction date is less than current time,
                must be greater than or equal current time
                """);

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("Description is required");
        }
    }
}
