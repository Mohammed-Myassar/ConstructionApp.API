using AutoMapper;
using Buisnes_Logic.Interface;
using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_repository;
using BuisnesLogic.Interface_Services;
using Domain.Entities;

namespace Buisnes_Logic.Services
{
    class ResourceUsageService : IResourceUsageService
    {
        private readonly IRepository repository;
        private readonly IValidatorService<ResourceUsageDTO> validator;
        private readonly IMapper mapper;

        public ResourceUsageService(
            IRepository repository,
            IValidatorService<ResourceUsageDTO> validator,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }
        public async Task<ResourceUsage> AddResourceUsageAsync(int projectId, int taskId,
            ResourceUsageDTO resourceUsageDto)
        {
            // Check the validity of values
            await validator.ValidatorAsync(resourceUsageDto);

            // Read task by Id and Id project from DataBase
            var task = await repository.FindTaskByIdAsync(projectId, taskId);

            // Chech task is null
            if (task == null)
                throw new InvalidOperationException($"Task wiht ID: {taskId} not found.");

            // Mapping from view model to Entity
            var usage = mapper.Map<ResourceUsage>(resourceUsageDto);

            // Assign the Project Id to Entity
            usage.ProjectTaskId = taskId;

            // Added Entity to DataBase
            await repository.AddResourceUsageAsync(usage);

            return usage;
        }

        public async Task<ResourceUsageDTO> 
            FindResourceUsageByIdAsync(int taskId, int resourceUsageId)
        {
            // Read resource usage by Id and Id project from DataBase
            var resourceUsage = await repository
                .FindResourceUsageByIdAsync(taskId, resourceUsageId);

            // Chech resource usage is null
            if (resourceUsage == null)
                throw new InvalidOperationException
                    ($"Resource Usage with ID: {resourceUsageId} not found");

            // Mapping from Entity to view model
            var resourceUsageDto = mapper.Map<ResourceUsageDTO>(resourceUsage);

            return resourceUsageDto;
        }
    }
}
