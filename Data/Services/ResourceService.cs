using Domain.Entities;
using System;

namespace Data.Services
{
    public class ResourceService
    {
        public void AddResource(string name, string description,
            UnitCost cost, decimal quantity)
        {
            using ConstructionContext context = new ConstructionContext();
            var resource = new Resource
            { 
                Name = name,
                Description = description,
                UnitCost = cost,
                Quantity = quantity
            };
            context.Resources.Add(resource);
            context.SaveChanges();
            Console.WriteLine("Resource added.");
        }

        public void AddResourceUsage(int taskId, int resourceId,
            decimal quantity, DateTime usageDate)
        {
            using ConstructionContext context = new ConstructionContext();

            var task = context.ProjectTasks.Find(taskId);
            var resource = context.Resources.Find(resourceId);

            if (task == null || resource == null)
            {
                Console.WriteLine("Task or Resource not found.");
                return;
            }

            var usage = new ResourceUsage
            {
                QuantityUsed = quantity,
                UsageDate = usageDate,
                ProjectTaskId = taskId,
                ResourceId = resourceId
            };

            context.ResourceUsages.Add(usage);
            context.SaveChanges();

            Console.WriteLine("Resource consumption is recorded for the task.");
        }
    }
}
