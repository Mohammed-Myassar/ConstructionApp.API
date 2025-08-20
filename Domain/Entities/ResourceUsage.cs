namespace Domain.Entities
{
    public class ResourceUsage
    {
        public int Id { get; set; }
        public decimal QuantityUsed { get; set; }
        public DateTime UsageDate { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }

        public int ProjectId { get; set; }
        public ConstructionProject Project { get; set; }
    }
}
