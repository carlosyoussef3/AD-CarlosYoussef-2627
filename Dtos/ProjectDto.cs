namespace TimesheetApi.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Client { get; set; } = "";
        public string Status { get; set; } = "";
        public decimal TotalHours { get; set; }
    }
}
