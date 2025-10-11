namespace BuisnesLogic.Interface_Services
{
    public interface IValidatorService<T>
    {
        Task ValidatorAsync(T objectValidate);
    }
}
