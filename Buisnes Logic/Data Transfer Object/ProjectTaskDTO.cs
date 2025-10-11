namespace BuisnesLogic.Data_Transfer_Object
{
    public class ProjectTaskDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public Status Status { get; set; }
    }
}
