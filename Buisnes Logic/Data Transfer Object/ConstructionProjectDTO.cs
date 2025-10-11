namespace BuisnesLogic.Data_Transfer_Object
{
    public enum Status : byte
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3
    }
    public class ConstructionProjectDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public Status Status { get; set; }

        public List<EmployeeDTO> EmployeeDTOs { get; set; }
        public List<ProjectTaskDTO> ProjectTaskDTOs { get; set; }
        public List<ResourceDTO> ResourceDTOs { get; set; }
        public List<PaymentTransactionDTO> paymentTransactionDTOs { get; set; }

        public ConstructionProjectDTO()
        {
            EmployeeDTOs = new List<EmployeeDTO>();
            ProjectTaskDTOs = new List<ProjectTaskDTO>();
            ResourceDTOs = new List<ResourceDTO>();
            paymentTransactionDTOs = new List<PaymentTransactionDTO>();
        }
    }
}
