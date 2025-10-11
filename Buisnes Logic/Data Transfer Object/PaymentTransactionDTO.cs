namespace BuisnesLogic.Data_Transfer_Object
{
    public enum TransactionType : byte
    {
        Income = 1,
        Expense = 2,
    }

    public class PaymentTransactionDTO
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
