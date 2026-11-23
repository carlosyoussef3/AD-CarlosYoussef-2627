using TimesheetApi.Model;

namespace TimesheetApi.Dtos
{
    public static class ProjectMappings
    {
        public static ProjectDto ToDto(this Project project)
        {
            return new ProjectDto()
            {
                Id = project.Id,
                Name = project.Name,
                Client = project.Client,
                Status = project.Status,
                TotalHours = project.TimeEntries.Sum(t => t.Hours)
            };
        }

        public static ProjectDetailDto ToDetailDto(this Project project)
        {
            return new ProjectDetailDto()
            {
                Id = project.Id,
                Name = project.Name,
                Client = project.Client,
                Status = project.Status,
                TotalHours = project.TimeEntries.Sum(t => t.Hours),
                TimeEntries = project.TimeEntries
                                .OrderByDescending(t => t.Date)
                                .Select(t => t.ToDto())
                                .ToList()
            };
        }

        public static Project ToEntity(this ProjectInputDto dto)
        {
            return new Project()
            {
                Name = dto.Name,
                Client = dto.Client,
                Status = "Active"   // businessregel: nieuw project = Active
            };
        }
    }
}
