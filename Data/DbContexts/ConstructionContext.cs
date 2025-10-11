using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContexts
{
    public class ConstructionContext : DbContext
    {
        public DbSet<ConstructionProject> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceUsage> ResourceUsages { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        public ConstructionContext(DbContextOptions<ConstructionContext> context) : base(context)
        {
        }
    }
}
