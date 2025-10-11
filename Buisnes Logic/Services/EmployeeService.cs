using AutoMapper;
using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_repository;
using BuisnesLogic.Interface_Services;
using Domain.Entities;

namespace BuisnesLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repository;
        private readonly IValidatorService<EmployeeDTO> validator;
        private readonly IMapper mapper;

        public EmployeeService(
            IRepository repository,
            IValidatorService<EmployeeDTO> validator,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<Employee> AddEmployeeToProjectAsync(int projectId,
            EmployeeDTO employeeDto)
        {
            // Check the validity of values
            await validator.ValidatorAsync(employeeDto);

            // Read project by Id from DataBase
            var project = await repository.ReadProjectByIdAsync(projectId);

            // Chech Project is null
            if (project == null)
                throw new InvalidOperationException($"Project wiht ID: {projectId} not found.");

            // Mapping from view model to Entity
            var employee = mapper.Map<Employee>(employeeDto);

            // Assign the Project Id to Entity
            employee.ConstructionProjectId = projectId;

            // Added Entity to DataBase
            await repository.AddEmployeeToProjectAsync(employee);

            return employee;
        }

        public async Task<EmployeeDTO> FindEmployeeById(int projectId, int employeeId)
        {
            // Read employee by Id and Id project from DataBase
            var employee = await repository.FindEmployeeByIdAsync(projectId, employeeId);

            // Chech employee is null
            if (employee == null)
                throw new InvalidOperationException($"Employee with ID: {employeeId} not found");

            // Mapping from Entity to view model
            var taskDto = mapper.Map<EmployeeDTO>(employee);

            return taskDto;
        }
    }
}
