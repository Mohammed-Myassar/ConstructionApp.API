namespace Domain.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public ProjectStatus Status { get; set; }

        public int ConstructionProjectId { get; set; }
        public ConstructionProject ConstructionProject { get; set; }

        public List<ResourceUsage> ResourceUsages { get; set; }

        public ProjectTask()
        {
            ResourceUsages = new List<ResourceUsage>();
        }
    }
}
