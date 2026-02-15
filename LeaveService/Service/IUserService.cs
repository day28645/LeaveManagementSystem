using LeaveService.DTO;
using LeaveService.Models;

namespace LeaveService.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<ServiceResponse<User>> CreateUserAsync(CreateUserDto dto);
        Task<ServiceResponse<User>> GetUserReportToByIdAsync(Guid id);
    }
}