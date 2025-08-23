using Domain.Entities;

namespace Data.Services
{
    public class TaskService
    {
        public void AddTask(int id, string name,
            string description, DateTime startDate, ProjectStatus status)
        {
            using ConstructionContext context = new ConstructionContext();
            var task = new ProjectTask 
            { 
                Name = name,
                Description = description,
                StartDate = startDate,
                Status = status,
                ConstructionProjectId = id
            };
            context.ProjectTasks.Add(task);
            context.SaveChanges();
            Console.WriteLine("New task added.");
        }

        public void UpdateTaskStatus(int id, ProjectStatus status)
        {
            using ConstructionContext context = new ConstructionContext();
            var task = context.ProjectTasks.Find(id);
            if (task != null)
            {
                task.Status = status;
                context.SaveChanges();
                Console.WriteLine("Task status has been modified.");
            }
        }
    }
}
