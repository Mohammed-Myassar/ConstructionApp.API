namespace Domain.Entities
{
    public enum UserRole : byte
    {
        Manager = 1,
        Employee = 2,
        Factor = 3
    }
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public int ConstructionProjectId { get; set; }
        public ConstructionProject constructionProject { get; set;}
    }
}
