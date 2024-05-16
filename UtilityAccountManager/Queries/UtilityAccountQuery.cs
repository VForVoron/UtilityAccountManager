namespace UtilityAccountManager.Queries
{
    public class UtilityAccountQuery
    {
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public bool OnlyWithResidents { get; set; } = false;
        public string? ResidentName { get; set; } = null;
        public string? Address { get; set; } = null;
    }
}
