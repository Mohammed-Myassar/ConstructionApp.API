using BuisnesLogic.Data_Transfer_Object;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace BuisnesLogic.Interface_Services
{
    public interface IProjectService
    {
        Task<ConstructionProject> AddProjectAsync(
            ConstructionProjectDTO projectDto);

        Task<ConstructionProjectDTO> ReadProjectByIdAsync(int projectId);

        Task<List<ConstructionProjectDTO>> ReadProjectsAsync();

        Task UpdateProjectAsync(int projectId,
            JsonPatchDocument<ConstructionProjectDTO> patchDocument);

        Task SoftDeleteProjectAsync(int projectId);
    }
}
