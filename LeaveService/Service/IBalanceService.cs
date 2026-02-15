using LeaveService.DTO;
using LeaveService.Models;

namespace LeaveService.Service
{
    public interface IBalanceService
    {
        Task<ServiceResponse<LeaveBalance>> CreateBalanceAsync(CreateBalanceDto dto);
        Task<ServiceResponse<BalanceResponseDto>> GetLeaveBalanceByIdAsync(Guid id);
        Task<ServiceResponse<List<BalanceResponseDto>>> GetBalanceByUserIdAsync(Guid userId);
        Task<ServiceResponse<UpdateBalanceDto>> UpdateBalanceAsync(Guid id, UpdateBalanceDto dto);
    }
}