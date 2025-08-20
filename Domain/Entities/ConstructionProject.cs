namespace Domain.Entities
{
    public enum ProjectStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
    public class ConstructionProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public ProjectStatus Status { get; set; }
        public List<Employee> Employees { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
        public List<ResourceUsage> ResourceUsage { get; set; }
        public List<PaymentTransaction> PaymentTransactions { get; set; }

        public ConstructionProject()
        {
            Employees = new List<Employee>();
            ProjectTasks = new List<ProjectTask>();
            ResourceUsage = new List<ResourceUsage>();
            PaymentTransactions = new List<PaymentTransaction>();
        }
    }
}
