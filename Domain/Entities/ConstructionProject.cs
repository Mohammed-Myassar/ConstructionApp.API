namespace Domain.Entities
{
    public enum ProjectStatus : byte
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3
    }
    public class ConstructionProject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public ProjectStatus Status { get; set; }

        public List<Employee> Employees { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
        public List<Resource> Resources { get; set; }
        public List<PaymentTransaction> PaymentTransactions { get; set; }

        public bool IsDeleted { get; set; }

        public ConstructionProject()
        {
            Employees = new List<Employee>();
            ProjectTasks = new List<ProjectTask>();
            Resources = new List<Resource>();
            PaymentTransactions = new List<PaymentTransaction>();
        }
    }
}
