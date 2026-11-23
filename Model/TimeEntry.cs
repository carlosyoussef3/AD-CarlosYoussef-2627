namespace TimesheetApi.Model
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public decimal Hours { get; set; }
        public DateTime Date { get; set; }
        public string RegisteredBy { get; set; } = "";

        // foreign key + navigatie-property: elke registratie hoort bij één project
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
