namespace Domain.Entities
{
    public enum TransactionType : byte
    {
        Income = 1,
        Expense = 2,
    }

    public class PaymentTransaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public ConstructionProject Project { get; set; }
    }
}
