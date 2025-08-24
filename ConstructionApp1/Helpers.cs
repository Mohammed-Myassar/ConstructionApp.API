using Domain.Entities;

namespace ConstructionApp
{
    public static class Helpers
    {
        public static bool IsValidValue(string name, decimal budget, DateTime date)
        {
            if (!string.IsNullOrWhiteSpace(name) && budget > 0 && date > DateTime.MinValue)
            {
                return true;
            }
            return false;
        }

        public static bool IsValidValue(string name, string description,
            DateTime date, ProjectStatus status)
        {
            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(description) 
                && Enum.IsDefined(typeof(ProjectStatus), status)
                && date > DateTime.MinValue)
            {
                return true;
            }
            return false;
        }
    }
}
