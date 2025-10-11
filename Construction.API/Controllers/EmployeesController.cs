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
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> logger;
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;

        public EmployeesController(
            ILogger<EmployeesController> logger,
            IMapper mapper,
            IEmployeeService employeeService
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        /// <summary>
        /// This Endpoint to get employee by ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{employeeId}", Name = "GetEmployeeById")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int projectId, int employeeId)
        {
            try
            {
                var employee = await employeeService.FindEmployeeById(projectId, employeeId);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This Endpoint to add employee to project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddEmployee(int projectId,
            [FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                var employee = await employeeService
                    .AddEmployeeToProjectAsync(projectId, employeeDto);

                return CreatedAtRoute("GetEmployeeById",
                    new { projectId = employee.Id },
                    mapper.Map<EmployeeDTO>(employee)
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
