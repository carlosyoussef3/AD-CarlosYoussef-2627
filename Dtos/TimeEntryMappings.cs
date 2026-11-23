using TimesheetApi.Model;

namespace TimesheetApi.Dtos
{
    public static class TimeEntryMappings
    {
        public static TimeEntryDto ToDto(this TimeEntry timeEntry)
        {
            return new TimeEntryDto()
            {
                Id = timeEntry.Id,
                ProjectId = timeEntry.ProjectId,
                ProjectName = timeEntry.Project?.Name ?? "",
                Description = timeEntry.Description,
                Hours = timeEntry.Hours,
                Date = timeEntry.Date,
                RegisteredBy = timeEntry.RegisteredBy
            };
        }

        public static TimeEntry ToEntity(this TimeEntryInputDto dto)
        {
            return new TimeEntry()
            {
                ProjectId = dto.ProjectId,
                Description = dto.Description,
                Hours = dto.Hours,
                Date = dto.Date!.Value.Date   // enkel de dag, zonder uur
            };
        }
    }
}
