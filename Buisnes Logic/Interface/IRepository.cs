using BuisnesLogic.Data_Transfer_Object;
using Domain.Entities;

namespace BuisnesLogic.Interface_repository
{
    public interface IRepository
    {
        // ==========Employee Manager=============
        Task AddEmployeeToProjectAsync(Employee employee);
        Task<Employee?> FindEmployeeByIdAsync(int projectId, int employeeId);

        // ==========Payment Manager=============
        Task AddTransactionAsync(PaymentTransaction transaction);
        Task<PaymentTransaction?> FindPaymentTransactionByIdAsync(int projectId,
            int transactionId);

        // ==========Project Manager=============
        Task AddProjectAsync(ConstructionProject project);

        Task<ConstructionProject?> ReadProjectByIdAsync(int projectId);

        Task<List<ConstructionProject>> ReadProjectsAsync();

        Task UpdateProjectAsync(ConstructionProject project);

        Task SoftDeleteProjectAsync(ConstructionProject project);

        // ==========Resource Manager=============
        Task AddResourceAsync(Resource resource);
        Task<Resource?> FindResourceByIdAsync(int projectId, int resourceId);

        // ==========Resource Usage Manager=============
        Task AddResourceUsageAsync(ResourceUsage resourceUsage);
        Task<ResourceUsage?> FindResourceUsageByIdAsync(int taskId, int resourceUsageId);

        // ==========Task Manager=============
        Task AddTaskAsync(ProjectTask task);

        Task UpdateTaskStatusAsync(ProjectTask task, Status status);

        Task<ProjectTask?> FindTaskByIdAsync(int projectId, int taskId);
    }
}
