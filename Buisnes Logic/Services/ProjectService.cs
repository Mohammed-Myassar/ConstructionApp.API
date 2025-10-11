using AutoMapper;
using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_repository;
using BuisnesLogic.Interface_Services;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace BuisnesLogic.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository repository;
        private readonly IValidatorService<ConstructionProjectDTO> validator;
        private readonly IMapper mapper;

        public ProjectService(
            IRepository repository,
            IValidatorService<ConstructionProjectDTO> validator,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }
        public async Task<ConstructionProject> AddProjectAsync(
            ConstructionProjectDTO projectDto)
        {
            // Check the validity of values
            await validator.ValidatorAsync(projectDto);

            // Mapping from view model to Entity
            var project = mapper.Map<ConstructionProject>(projectDto);

            // Added Entity to DataBase
            await repository.AddProjectAsync(project);

            return project;
        }

        public async Task<ConstructionProjectDTO> ReadProjectByIdAsync(int projectId)
        {
            // Read project by Id from DataBase
            var project = await repository.ReadProjectByIdAsync(projectId);

            // Chech project is null
            if (project == null)
                throw new InvalidOperationException($"Project with ID: {projectId} not found.");

            // Mapping from Entity to view model
            var projectDto = mapper.Map<ConstructionProjectDTO>(project);

            return projectDto;
        }

        public async Task<List<ConstructionProjectDTO>> ReadProjectsAsync()
        {
            // Read all projects from DataBase
            var projects = await repository.ReadProjectsAsync();

            // Check list projects is null
            if (projects is null)
                throw new InvalidOperationException($"Projects is not found.");

            // Mapping from Entity to view model
            var projectDtos = mapper.Map<List<ConstructionProjectDTO>>(projects);

            return projectDtos;
        }

        public async Task UpdateProjectAsync(int projectId,
            JsonPatchDocument<ConstructionProjectDTO> patchDocument)
        {
            // Read project by Id from DataBase
            var project = await repository.ReadProjectByIdAsync(projectId);

            // Chech project is null
            if (project == null)
                throw new InvalidOperationException($"Project with ID: {projectId} not found.");

            // Mapping from Entity to view model
            var projectDto = mapper.Map<ConstructionProjectDTO>(project);

            // Update partialy to Data by JsonPatch
            patchDocument.ApplyTo(projectDto);

            // Check the validity of values
            await validator.ValidatorAsync(projectDto);

            // Mapping from view model to Entity
            project = mapper.Map<ConstructionProject>(projectDto);

            // Assign the Project Id to Entity
            project.Id = projectId;

            // Update Entity in DataBase
            await repository.UpdateProjectAsync(project);
        }

        public async Task SoftDeleteProjectAsync(int projectId)
        {
            // Read project by Id from DataBase
            var project = await repository.ReadProjectByIdAsync(projectId);

            // Chech project is null
            if (project == null)
                throw new InvalidOperationException($"Project with ID: {projectId} not found.");

            // Delete project by way soft Delete
            await repository.SoftDeleteProjectAsync(project);
        }
    }
}