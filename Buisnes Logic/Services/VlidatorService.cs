using BuisnesLogic.Interface_Services;
using FluentValidation;

namespace BuisnesLogic.Services
{
    public class ValidatorService<T> : IValidatorService<T>
    {
        private readonly IValidator<T> validator;

        public ValidatorService(IValidator<T> validator)
        {
            this.validator = validator;
        }

        public async Task ValidatorAsync(T objectValidate)
        {
            var v = await validator.ValidateAsync(objectValidate);

            if (!v.IsValid)
                throw new ValidationException(v.Errors);
        }
    }
}
