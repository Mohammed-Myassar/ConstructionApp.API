using BuisnesLogic.Data_Transfer_Object;
using Domain.Entities;

namespace BuisnesLogic.Interface_Services
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployeeToProjectAsync(int projectId, EmployeeDTO employee);
        Task<EmployeeDTO> FindEmployeeById(int projectId, int employeeId);
    }
}
