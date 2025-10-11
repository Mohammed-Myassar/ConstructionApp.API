namespace BuisnesLogic.Data_Transfer_Object
{
    public enum UnitCost : byte
    {
        Ton = 1,
        CubicMeter = 2
    }

    public class ResourceDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public UnitCost UnitCost { get; set; }
        public decimal Quantity { get; set; }

        public List<ResourceUsageDTO> ResourceUsageDTOs { get; set; }

        public ResourceDTO()
        {
            ResourceUsageDTOs = new List<ResourceUsageDTO>();
        }
    }
}
