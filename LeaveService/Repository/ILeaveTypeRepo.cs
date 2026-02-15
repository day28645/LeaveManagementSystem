using LeaveService.Models;

namespace LeaveService.Repository
{
    public interface ILeaveTypeRepo
    {
        Task<IEnumerable<LeaveType>> GetLeaveTypeAllAsync();
        Task<LeaveType> GetLeaveTypeByIdAsync(Guid id);
        Task<LeaveType> GetLeaveTypeByTypeAsync(string type);
        Task AddLeaveTypeAsync(LeaveType leaveType);
        Task SaveLeaveTypeAsync();
    }
}