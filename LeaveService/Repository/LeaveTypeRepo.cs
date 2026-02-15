using LeaveService.Data;
using LeaveService.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveService.Repository
{
    public class LeaveTypeRepo : ILeaveTypeRepo
    {
        private readonly AppDbContext _context;

        public LeaveTypeRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLeaveTypeAsync(LeaveType leaveType)
        {
            await _context.LeaveTypes.AddAsync(leaveType);
        }

        public async Task<IEnumerable<LeaveType>> GetLeaveTypeAllAsync()
        {
            return await _context.LeaveTypes.ToListAsync();
        }

        public async Task<LeaveType> GetLeaveTypeByIdAsync(Guid id)
        {
            return await _context.LeaveTypes.FirstOrDefaultAsync(x => x.LeaveTypeId == id);
        }

        public async Task<LeaveType> GetLeaveTypeByTypeAsync(string type)
        {
            return await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Type == type);
        }

        public async Task SaveLeaveTypeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}