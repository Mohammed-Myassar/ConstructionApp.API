using AutoMapper;
using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_repository;
using BuisnesLogic.Interface_Services;
using Domain.Entities;

namespace BuisnesLogic.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IRepository repository;
        private readonly IValidatorService<ResourceDTO> validator;
        private readonly IMapper mapper;

        public ResourceService(
            IRepository repository,
            IValidatorService<ResourceDTO> validator,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<Resource> AddResourceAsync(int projectId, ResourceDTO resourceDto)
        {
            // Check the validity of values
            await validator.ValidatorAsync(resourceDto);

            // Read project by Id from DataBase
            var project = await repository.ReadProjectByIdAsync(projectId);

            // Chech Project is null
            if (project == null)
                throw new InvalidOperationException($"Project wiht ID: {projectId} not found.");

            // Mapping from view model to Entity
            var resource = mapper.Map<Resource>(resourceDto);

            // Assign the Project Id to Entity
            resource.ConstructionProjectId = projectId;

            // Added Entity to DataBase
            await repository.AddResourceAsync(resource);

            return resource;
        }

        public async Task<ResourceDTO> FindResourceByIdAsync(int projectId, int resourceId)
        {
            // Read resource by Id and Id project from DataBase
            var resource = await repository.FindResourceByIdAsync(projectId, resourceId);

            // Chech resource is null
            if (resource == null)
                throw new InvalidOperationException($"Resource with ID: {resourceId} not found");

            // Mapping from Entity to view model
            var resourceDto = mapper.Map<ResourceDTO>(resource);

            return resourceDto;
        }
    }
}
