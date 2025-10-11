using BuisnesLogic.Data_Transfer_Object;
using Domain.Entities;

namespace BuisnesLogic.Interface_Services
{
    public interface ITaskService
    {
        Task<ProjectTask> AddTaskAsync(int projectId, ProjectTaskDTO task);

        Task UpdateTaskStatusAsync(int projectId, int taskId, Status status);

        Task<ProjectTaskDTO> FindTaskById(int projectId, int taskId);
    }
}
