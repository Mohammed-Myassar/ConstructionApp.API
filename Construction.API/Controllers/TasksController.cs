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
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly IMapper mapper;
        private readonly ILogger<TasksController> logger;

        public TasksController(
            ITaskService taskService,
            IMapper mapper,
            ILogger<TasksController> logger
            )
        {
            this.taskService = taskService;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// This Endpoint to get task by ID
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{taskId}", Name = "GetTaskById")]
        public async Task<ActionResult<ProjectTaskDTO>> GetTask(int projectId, int taskId)
        {
            try
            {
                var task = await taskService.FindTaskById(projectId, taskId);
                return Ok(task);

            } catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to add task to project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="taskDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddTask(int projectId,
            [FromBody] ProjectTaskDTO taskDto)
        {
            try
            {
                var task = await taskService.AddTaskAsync(projectId, taskDto);
                return CreatedAtRoute("GetTaskById",
                    new { taskId = task.Id },
                    mapper.Map<ProjectTaskDTO>(task)
                    );
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to update status task in project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("{taskId}")]
        public async Task<ActionResult> 
            UpdateTaskStatus(int projectId, int taskId, Status status)
        {
            try
            {
                await taskService.UpdateTaskStatusAsync(projectId, taskId, status);
                return Ok();
            }catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
