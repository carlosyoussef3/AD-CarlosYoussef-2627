using TimesheetApi.Dtos;

namespace TimesheetApi.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllAsync();
        Task<ProjectDetailDto?> GetByIdAsync(int id);
        Task<ProjectDto> CreateAsync(ProjectInputDto dto);
        Task<bool> UpdateAsync(int id, ProjectUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
