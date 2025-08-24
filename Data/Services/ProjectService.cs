using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class ProjectService
    {
        public void AddProject(string name, decimal budget, DateTime startdate)
        {
            using ConstructionContext context = new ConstructionContext();
            var project = new ConstructionProject 
            { 
                Name = name,
                Budget = budget,
                StartDate = startdate,
                IsDeleted = false
            };

            context.Projects.Add(project);
            context.SaveChanges();
            Console.WriteLine("The project has been added successfully.");
        }

        public ConstructionProject? ReadProjectsByName(string name)
        {
            using ConstructionContext context = new ConstructionContext();

            var project = context.Projects
                                 .Where(proj => proj.Name == name && !proj.IsDeleted)
                                 .Include(t => t.ProjectTasks)
                                 .Include(e => e.Employees)
                                 .Include(r => r.ResourceUsage)
                                 .Include(p => p.PaymentTransactions)
                                 .AsNoTracking()
                                 .FirstOrDefault();

            return project;
        }

        public List<ConstructionProject> ReadProjects()
        {
            using ConstructionContext context = new ConstructionContext();

            var project = context.Projects
                                 .Where(proj =>  !proj.IsDeleted)
                                 .Include(t => t.ProjectTasks)
                                 .Include(e => e.Employees)
                                 .Include(r => r.ResourceUsage)
                                 .Include(p => p.PaymentTransactions)
                                 .AsNoTracking()
                                 .ToList();

            return project;
        }

        public void UpdateProject(int id, decimal newBudget)
        {
            using ConstructionContext context = new ConstructionContext();
            var project = context.Projects.Find(id);
            if (project != null)
            {
                project.Budget = newBudget;
                context.SaveChanges();
                Console.WriteLine("The project has been successfully modified.");
            }
        }

        public void SoftDeleteProject(int id)
        {
            using ConstructionContext context = new ConstructionContext();
            var project = context.Projects.Find(id);
            if (project != null)
            {
                project.IsDeleted = true;
                context.SaveChanges();
                Console.WriteLine("The project was successfully deleted.");
            }
        }
    }
}
