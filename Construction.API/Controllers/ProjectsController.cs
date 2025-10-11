using Asp.Versioning;
using AutoMapper;
using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly IMapper mapper;
        private readonly IProjectService projectService;

        public ProjectsController(
            ILogger<ProjectsController> logger,
            IMapper mapper,
            IProjectService projectService
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.projectService = projectService;
        }

        /// <summary>
        /// This Endpoint to get all project
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ConstructionProjectDTO>>> GetProjects()
        {
            try
            {
                var projects = await projectService.ReadProjectsAsync();

                return Ok(projects);
            } catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to get project by ID
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{projectId}", Name = "GetProjectById")]
        public async Task<ActionResult<ConstructionProjectDTO>>
            GetProject(int projectId)
        {
            try
            {
                var projects = await projectService.ReadProjectByIdAsync(projectId);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to add project
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ConstructionProjectDTO>> AddProject(
            [FromBody] ConstructionProjectDTO projectDto)
        {
            try
            {
                var project = await projectService.AddProjectAsync(projectDto);

                return CreatedAtRoute("GetProjectById",
                    new { projectId = project.Id },
                    mapper.Map<ConstructionProjectDTO>(project)
                    );
            } catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to update project in a way JsonPatch
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{projectId}")]
        public async Task<ActionResult> UpdateProject(int projectId,
            [FromBody] JsonPatchDocument<ConstructionProjectDTO> patchDocument)
        {
            try
            {
                await projectService
                    .UpdateProjectAsync(projectId, patchDocument);

                return NoContent();
            } catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to Delete project in a way Soft Delete
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId}")]
        public async Task<ActionResult> DeleteProject(int projectId)
        {
            try
            {
                await projectService.SoftDeleteProjectAsync(projectId);

                return NoContent();
            }catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
