using TimesheetApi.Data;
using TimesheetApi.Dtos;

namespace TimesheetApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProjectDto>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();
            return projects.Select(p => p.ToDto()).ToList();
        }

        public async Task<ProjectDetailDto?> GetByIdAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            return project?.ToDetailDto();
        }

        public async Task<ProjectDto> CreateAsync(ProjectInputDto dto)
        {
            var project = dto.ToEntity();   // status wordt hier op Active gezet
            await _repository.AddAsync(project);
            return project.ToDto();
        }

        public async Task<bool> UpdateAsync(int id, ProjectUpdateDto dto)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project is null)
            {
                return false;
            }

            project.Name = dto.Name;
            project.Client = dto.Client;
            project.Status = dto.Status;
            await _repository.UpdateAsync(project);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project is null)
            {
                return false;
            }

            await _repository.DeleteAsync(project);
            return true;
        }
    }
}
