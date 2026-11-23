using TimesheetApi.Model;

namespace TimesheetApi.Data
{
    public interface ITimeEntryRepository
    {
        Task<List<TimeEntry>> GetAllAsync(int? projectId);
        Task<TimeEntry?> GetByIdAsync(int id);
        Task AddAsync(TimeEntry timeEntry);
        Task DeleteAsync(TimeEntry timeEntry);
    }
}
