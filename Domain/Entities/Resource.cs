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
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public UnitCost UnitCost { get; set; }
        public decimal Quantity { get; set; }

        public int ConstructionProjectId { get; set; }

        public ConstructionProject ConstructionProject { get; set; }
    }
}
