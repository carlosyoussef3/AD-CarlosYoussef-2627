namespace TimesheetApi.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Client { get; set; } = "";

        // "Active" of "Closed". Een nieuw project start altijd als Active.
        public string Status { get; set; } = "Active";

        // één project heeft meerdere urenregistraties (één-op-veel)
        public List<TimeEntry> TimeEntries { get; set; } = new();
    }
}
