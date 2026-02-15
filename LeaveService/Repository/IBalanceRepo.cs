using LeaveService.Models;

namespace LeaveService.Repository
{
    public interface IBalanceRepo
    {
        Task<IEnumerable<LeaveBalance>> GetAllBalanceAsync();
        Task<IEnumerable<LeaveBalance>> GetBalanceByUserIdAsync(Guid userId);
        Task<LeaveBalance> GetBalanceByIdAsync(Guid id);
        Task<LeaveBalance?> GetBalanceByUserAndTypeAsync(Guid userId, Guid leaveTypeId);
        Task AddBalanceAsync(LeaveBalance leaveBalance);
        Task UpdateBalanceAsync(LeaveBalance balance);
        Task SaveBalanceAsync();
    }
}