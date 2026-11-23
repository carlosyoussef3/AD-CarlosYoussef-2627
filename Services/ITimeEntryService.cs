using TimesheetApi.Dtos;

namespace TimesheetApi.Services
{
    public interface ITimeEntryService
    {
        Task<List<TimeEntryDto>> GetAllAsync(int? projectId);
        Task<TimeEntryDto?> GetByIdAsync(int id);

        // geeft 2 dingen terug (tuple): het resultaat én de nieuwe registratie (enkel bij Success)
        Task<(CreateTimeEntryResult result, TimeEntryDto? timeEntry)> CreateAsync(TimeEntryInputDto dto, string registeredBy);

        Task<bool> DeleteAsync(int id);
    }
}
