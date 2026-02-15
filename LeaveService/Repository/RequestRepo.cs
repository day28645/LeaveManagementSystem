using LeaveService.Data;
using LeaveService.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveService.Repository
{
    public class RequestRepo : IRequestRepo
    {
        private readonly AppDbContext _context;

        public RequestRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddRequestAsync(LeaveRequest leaveRequest)
        {
            await _context.AddAsync(leaveRequest);
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllRequestAsync()
        {
            return await _context.LeaveRequests.ToListAsync();
        }

        public async Task<LeaveRequest> GetRequestByIdAsync(Guid id)
        {
            return await _context.LeaveRequests.FirstOrDefaultAsync(x => x.LeaveRequestId == id);
        }

        public async Task<bool> HasOverlappingRequestAsync(Guid userId, DateOnly startDate, DateOnly endDate)
        {
            return await _context.LeaveRequests.
            AnyAsync(x =>
                x.UserId == userId &&
                x.StartDate <= endDate &&
                x.EndDate >= startDate &&
                x.Status != "Reject"
            );
        }

        public async Task SaveRequestAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRequestAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
        }
    }
}