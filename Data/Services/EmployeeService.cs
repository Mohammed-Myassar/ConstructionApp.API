using Domain.Entities;

namespace Data.Services
{
    public class EmployeeService
    {
        public void AddEmployeeToProject(int id, string firstName,
            string lastname, UserRole role)
        {
            using ConstructionContext context = new ConstructionContext();
            var employee = new Employee 
            { 
                FirstName = firstName,
                LastName = lastname,
                Role = role,
                ConstructionProjectId = id
            };
            context.Employees.Add(employee);
            context.SaveChanges();
            Console.WriteLine("The employee has been assigned to the project.");
        }
    }
}
