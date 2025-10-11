using BuisnesLogic.Data_Transfer_Object;
using Domain.Entities;

namespace BuisnesLogic.Interface_Services
{
    public interface IResourceService
    {
        Task<Resource> AddResourceAsync(int projectId, ResourceDTO resourceDto);

        Task<ResourceDTO> FindResourceByIdAsync(int projectId, int resourceId);
    }
}
