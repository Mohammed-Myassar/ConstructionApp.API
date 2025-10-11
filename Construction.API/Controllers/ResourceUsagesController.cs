using Asp.Versioning;
using AutoMapper;
using Buisnes_Logic.Interface;
using BuisnesLogic.Data_Transfer_Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [Authorize]
    [Route("api/projects/{projectId}tasks/{taskId}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ResourceUsagesController : ControllerBase
    {
        private readonly ILogger<ResourceUsagesController> logger;
        private readonly IMapper mapper;
        private readonly IResourceUsageService resourceUsageService;

        public ResourceUsagesController(
            ILogger<ResourceUsagesController> logger,
            IMapper mapper,
            IResourceUsageService resourceUsageService
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.resourceUsageService = resourceUsageService;
        }

        /// <summary>
        /// This Endpoint to get employee by ID
        /// </summary>
        /// <param name="resourceUsageId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{resourceUsageId}", Name = "GetResourcesUsageById")]
        public async Task<ActionResult<ResourceUsageDTO>>
            GetResourceUsage(int taskId, int resourceUsageId)
        {
            try
            {
                var resourceUsage = await resourceUsageService
                    .FindResourceUsageByIdAsync(taskId, resourceUsageId);
                return Ok(resourceUsage);
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to add resource usage to project
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="resourceUsageDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult>
            AddResourceUsage(int projectId, int taskId,
            [FromBody] ResourceUsageDTO resourceUsageDto)
        {
            try
            {
                var resourceUsage = await resourceUsageService
                    .AddResourceUsageAsync(projectId, taskId, resourceUsageDto);

                return CreatedAtRoute("GetResourcesUsageById",
                    new { projectId = resourceUsage.Id },
                    mapper.Map<ResourceUsageDTO>(resourceUsage)
                    );
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
