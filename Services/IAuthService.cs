using TimesheetApi.Dtos;

namespace TimesheetApi.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<TokenDto?> LoginAsync(LoginDto dto);
    }
}
