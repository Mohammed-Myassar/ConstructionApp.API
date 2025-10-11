using Domain.Entities;
using BuisnesLogic.Interface_repository;
using BuisnesLogic.Interface_Services;
using BuisnesLogic.Data_Transfer_Object;
using AutoMapper;

namespace BuisnesLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository repository;
        private readonly IValidatorService<ProjectTaskDTO> validator;
        private readonly IMapper mapper;

        public TaskService(
            IRepository repository,
            IValidatorService<ProjectTaskDTO> validator,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<ProjectTask> AddTaskAsync(int projectId, ProjectTaskDTO taskDto)
        {
            // Check the validity of values
            await validator.ValidatorAsync(taskDto);

            // Read project by Id from DataBase
            var project = await repository.ReadProjectByIdAsync(projectId);

            // Chech Project is null
            if (project == null)
                throw new InvalidOperationException($"Project wiht ID: {projectId} not found.");

            // Mapping from view model to Entity
            var task = mapper.Map<ProjectTask>(taskDto);

            // Assign the Project Id to Entity
            task.ConstructionProjectId = projectId;

            // Added Entity to DataBase
            await repository.AddTaskAsync(task);

            return task;
        }

        public async Task UpdateTaskStatusAsync(int projectId, int taskId, Status status)
        {
            // Read task by Id and Id project from DataBase
            var task = await repository.FindTaskByIdAsync(projectId, taskId);

            // Chech task is null
            if (task == null)
                throw new InvalidOperationException($"Task with ID: {taskId} not found");

            // Update Status task in DataBase
            await repository.UpdateTaskStatusAsync(task, status);
        }

        public async Task<ProjectTaskDTO> FindTaskById(int prjectId, int taskId)
        {
            // Read task by Id and Id project from DataBase
            var task = await repository.FindTaskByIdAsync(prjectId, taskId);

            // Chech task is null
            if (task == null)
                throw new InvalidOperationException($"Task with ID: {taskId} not found");

            // Mapping from Entity to view model
            var taskDto = mapper.Map<ProjectTaskDTO>(task);

            return taskDto;
        }
    }
}
