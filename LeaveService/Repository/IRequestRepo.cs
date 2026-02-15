using LeaveService.Models;

namespace LeaveService.Repository
{
    public interface IRequestRepo
    {
        Task<IEnumerable<LeaveRequest>> GetAllRequestAsync();
        Task<LeaveRequest> GetRequestByIdAsync(Guid id);
        Task<bool> HasOverlappingRequestAsync
        (
            Guid userId,
            DateOnly startDate,
            DateOnly endDate
        );
        Task UpdateRequestAsync(LeaveRequest leaveRequest); 
        Task AddRequestAsync(LeaveRequest leaveRequest);
        Task SaveRequestAsync();
    }
}