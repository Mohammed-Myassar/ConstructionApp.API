using Domain.Entities;

namespace Data.Services
{
    public class PaymentService
    {
        public void AddTransaction(int projectId,
            TransactionType type, decimal amount,
            string description)
        {
            using ConstructionContext context = new ConstructionContext();
            var transaction = new PaymentTransaction
            {
                ProjectId = projectId,
                Type = type,
                Amount = amount,
                Description = description,
                TransactionDate = DateTime.Now
            };
            context.PaymentTransactions.Add(transaction);
            context.SaveChanges();
            Console.WriteLine("The financial transaction has been recorded.");
        }
    }
}
