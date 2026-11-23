namespace TimesheetApi.Dtos
{
    public class TimeEntryDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Hours { get; set; }
        public DateTime Date { get; set; }
        public string RegisteredBy { get; set; } = "";
    }
}
