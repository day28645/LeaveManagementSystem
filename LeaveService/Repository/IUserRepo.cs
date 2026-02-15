using LeaveService.DTO;
using LeaveService.Models;

namespace LeaveService.Repository
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetUserAllAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetUserByIdAsync(Guid id);
        Task<User?> GetUserReportToByIdAsync(Guid employeeId);
        Task AddUserAsync(User user);
        Task SaveUserAsync();
    }
}