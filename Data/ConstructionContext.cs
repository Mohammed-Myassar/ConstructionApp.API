using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ConstructionContext : DbContext
    {
        public DbSet<ConstructionProject> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceUsage> ResourceUsages { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ConstructionApp");
        }
    }
}
