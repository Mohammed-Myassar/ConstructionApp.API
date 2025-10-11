using Asp.Versioning;
using AutoMapper;
using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [Authorize]
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ResourcesController : ControllerBase
    {
        private readonly ILogger<ResourcesController> logger;
        private readonly IMapper mapper;
        private readonly IResourceService resourceService;

        public ResourcesController(
            ILogger<ResourcesController> logger,
            IMapper mapper,
            IResourceService resourceService
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.resourceService = resourceService;
        }

        /// <summary>
        /// This Endpoint to get resource by ID
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{resourceId}", Name = "GetResourcesById")]
        public async Task<ActionResult<ResourceDTO>> GetResource(int projectId, int resourceId)
        {
            try
            {
                var resource = await resourceService.FindResourceByIdAsync(projectId, resourceId);
                return Ok(resource);
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to add resource to project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="resourceDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult>
            AddResource(int projectId, [FromBody] ResourceDTO resourceDto)
        {
            try
            {
                var resource = await resourceService
                    .AddResourceAsync(projectId, resourceDto);

                return CreatedAtRoute("GetResourcesById",
                    new { projectId = resource.Id },
                    mapper.Map<ResourceDTO>(resource)
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
