namespace Domain.Entities
{
    public enum UnitCost : byte
    {
        Ton = 1,
        CubicMeter = 2
    }

    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UnitCost UnitCost { get; set; }
        public decimal Quantity { get; set; }

        public List<ResourceUsage> ResourceUsages { get; set; }

        public Resource()
        {
            ResourceUsages = new List<ResourceUsage>();
        }
    }
}
