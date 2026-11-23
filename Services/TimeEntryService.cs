using TimesheetApi.Data;
using TimesheetApi.Dtos;

namespace TimesheetApi.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IProjectRepository _projectRepository;

        public TimeEntryService(ITimeEntryRepository timeEntryRepository, IProjectRepository projectRepository)
        {
            _timeEntryRepository = timeEntryRepository;
            _projectRepository = projectRepository;
        }

        public async Task<List<TimeEntryDto>> GetAllAsync(int? projectId)
        {
            var entries = await _timeEntryRepository.GetAllAsync(projectId);
            return entries.Select(t => t.ToDto()).ToList();
        }

        public async Task<TimeEntryDto?> GetByIdAsync(int id)
        {
            var entry = await _timeEntryRepository.GetByIdAsync(id);
            return entry?.ToDto();
        }

        public async Task<(CreateTimeEntryResult result, TimeEntryDto? timeEntry)> CreateAsync(TimeEntryInputDto dto, string registeredBy)
        {
            // regel 1: het project moet bestaan
            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);
            if (project is null)
            {
                return (CreateTimeEntryResult.ProjectNotFound, null);
            }

            // regel 2 (businessregel): geen uren boeken op een afgesloten project
            if (project.Status == "Closed")
            {
                return (CreateTimeEntryResult.ProjectClosed, null);
            }

            var entry = dto.ToEntity();
            entry.RegisteredBy = registeredBy;   // komt uit het token, niet van de client
            entry.Project = project;             // zodat ProjectName in de DTO ingevuld is
            await _timeEntryRepository.AddAsync(entry);

            return (CreateTimeEntryResult.Success, entry.ToDto());
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entry = await _timeEntryRepository.GetByIdAsync(id);
            if (entry is null)
            {
                return false;
            }

            await _timeEntryRepository.DeleteAsync(entry);
            return true;
        }
    }
}
