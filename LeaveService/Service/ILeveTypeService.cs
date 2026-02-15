using LeaveService.DTO;
using LeaveService.Models;

namespace LeaveService.Service
{
    public interface ILeaveTypeService
    {
        Task<IEnumerable<LeaveTypeResponseDto>> GetAllLevaeTypesAsync();
        Task<ServiceResponse<LeaveType>> CreateLeaveTypeAsync(CreateLeaveTypeDto dto);
    }
}