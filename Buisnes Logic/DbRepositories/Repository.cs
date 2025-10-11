using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_repository;
using Data.DbContexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbRepositories.Repositories
{
    public class Repository : IRepository
    {
        private readonly ConstructionContext context;

        public Repository(ConstructionContext context)
        {
            this.context = context;
        }

        // ==========Employee Manager=============
        // Add Employee To Project
        public async Task AddEmployeeToProjectAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
        }
        // Find Employee
        public async Task<Employee?> FindEmployeeByIdAsync(int projectId, int employeeId)
        {
            return await context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == employeeId && e.ConstructionProjectId == projectId);
        }

        // ==========Resource Manager=============
        // Add Resource
        public async Task AddResourceAsync(Resource resource)
        {
            await context.Resources.AddAsync(resource);
            await context.SaveChangesAsync();
        }
        // Find Resource
        public async Task<Resource?> FindResourceByIdAsync(int projectId, int resourceId)
        {
            return await context.Resources
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Id == resourceId && r.ConstructionProjectId == projectId);
        }

        // ==========Resource Usage Manager=============
        // Add Resource Usage
        public async Task AddResourceUsageAsync(ResourceUsage resourceUsage)
        {
            await context.ResourceUsages.AddAsync(resourceUsage);
            await context.SaveChangesAsync();
        }
        // Find Resource Usage
        public async Task<ResourceUsage?>
            FindResourceUsageByIdAsync(int taskId, int resourceUsageId)
        {
            return await context.ResourceUsages
                .AsNoTracking()
                .FirstOrDefaultAsync(ru =>  ru.Id == resourceUsageId);
        }

        // ==========Task Manager=============
        // Add Task
        public async Task AddTaskAsync(ProjectTask task)
        {
            await context.ProjectTasks.AddAsync(task);
            await context.SaveChangesAsync();
        }
        // Find Task
        public async Task<ProjectTask?> FindTaskByIdAsync(int prjectId, int taskId)
        {
            return await context.ProjectTasks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == taskId && t.ConstructionProjectId == prjectId);
        }
        // Update Task Status
        public async Task UpdateTaskStatusAsync(ProjectTask task, Status status)
        {
            task.Status = (ProjectStatus)status;
            await context.SaveChangesAsync();
        }

        // ==========Payment Manager=============
        // Add Transaction
        public async Task AddTransactionAsync(PaymentTransaction transaction)
        {
            await context.PaymentTransactions.AddAsync(transaction);
            await context.SaveChangesAsync();
        }
        // Find Payment Transaction
        public async Task<PaymentTransaction?> 
            FindPaymentTransactionByIdAsync(int projectId, int transactionId)
        {
            return await context.PaymentTransactions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == transactionId && p.ConstructionProjectId == projectId);
        }

        // ==========Project Manager=============
        // Add Project
        public async Task AddProjectAsync(ConstructionProject project)
        {
            await context.Projects.AddAsync(project);
            await context.SaveChangesAsync();
        }
        // Read Project By ID
        public async Task<ConstructionProject?> ReadProjectByIdAsync(int id)
        {
            var project = await context.Projects
                                 .Include(e => e.Employees)
                                 .Include(t => t.ProjectTasks)
                                 .Include(r => r.Resources)
                                 .Include(p => p.PaymentTransactions)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            return project;
        }
        // Read All Projects
        public async Task<List<ConstructionProject>> ReadProjectsAsync()
        {
            var project = await context.Projects
                                 .Where(proj => !proj.IsDeleted)
                                 .Include(e => e.Employees)
                                 .Include(t => t.ProjectTasks)
                                 .Include(r => r.Resources)
                                 .Include(p => p.PaymentTransactions)
                                 .AsNoTracking()
                                 .ToListAsync();
            return project;
        }
        // Soft Delete Project
        public async Task SoftDeleteProjectAsync(ConstructionProject project)
        {
            project.IsDeleted = true;
            await context.SaveChangesAsync();
        }
        // Update Project
        public async Task UpdateProjectAsync(ConstructionProject project)
        {
            context.Projects.Update(project);
            await context.SaveChangesAsync();
        }
    }
}
