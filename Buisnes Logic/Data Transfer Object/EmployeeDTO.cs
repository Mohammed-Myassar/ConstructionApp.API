namespace BuisnesLogic.Data_Transfer_Object
{
    public enum UserRole : byte
    {
        Manager = 1,
        Employee = 2,
        Factor = 3
    }
    public class EmployeeDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
