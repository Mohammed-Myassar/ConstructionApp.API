using BuisnesLogic.Data_Transfer_Object;
using Domain.Entities;

namespace BuisnesLogic.Interface_Services
{
    public interface IPaymentService
    {
        Task<PaymentTransaction> AddTransactionAsync(int projectId,
            PaymentTransactionDTO transactionDto);
        Task<PaymentTransactionDTO> FindPaymentTransactionById(int projectId, int transactionId);
    }
}
