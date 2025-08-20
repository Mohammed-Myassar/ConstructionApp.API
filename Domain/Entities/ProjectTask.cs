namespace Domain.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public ProjectStatus Status { get; set; }

        public int ConstructionProjectId { get; set; }
        public ConstructionProject constructionProject { get; set; }
    }
}
