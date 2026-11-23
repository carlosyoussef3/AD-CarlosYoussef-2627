using Microsoft.EntityFrameworkCore;
using TimesheetApi.Model;

namespace TimesheetApi.Data
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly AppDbContext _context;

        public TimeEntryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeEntry>> GetAllAsync(int? projectId)
        {
            // Include(Project): zo kennen we ook de projectnaam voor de DTO
            var query = _context.TimeEntries
                            .Include(t => t.Project)
                            .AsQueryable();

            // optionele filter: enkel de registraties van één project
            if (projectId is not null)
            {
                query = query.Where(t => t.ProjectId == projectId);
            }

            return await query
                            .OrderByDescending(t => t.Date)
                            .ToListAsync();
        }

        public async Task<TimeEntry?> GetByIdAsync(int id)
        {
            return await _context.TimeEntries
                            .Include(t => t.Project)
                            .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(TimeEntry timeEntry)
        {
            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TimeEntry timeEntry)
        {
            _context.TimeEntries.Remove(timeEntry);
            await _context.SaveChangesAsync();
        }
    }
}
