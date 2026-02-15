using LeaveService.DTO;
using LeaveService.Models;

namespace LeaveService.Service
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestResponseDto>> GetAllRequestsAsync();
        Task<ServiceResponse<RequestResponseDto>> CreateLRequestAsync(CreateRequestDto dto);
        Task<ServiceResponse<LeaveRequest>> UpdateRequestAsync(Guid id, UpdateRequestDto dto);
    }
}