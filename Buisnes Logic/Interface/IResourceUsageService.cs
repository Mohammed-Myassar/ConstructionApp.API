using BuisnesLogic.Data_Transfer_Object;
using Domain.Entities;

namespace Buisnes_Logic.Interface
{
    public interface IResourceUsageService
    {
        Task<ResourceUsage> AddResourceUsageAsync(int projectId, int taskId,
            ResourceUsageDTO resourceUsageDto);

        Task<ResourceUsageDTO> FindResourceUsageByIdAsync(int taskId, int resourceUsageId);
    }
}
