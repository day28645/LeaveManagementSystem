using LeaveService.Data;
using LeaveService.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveService.Repository
{
    public class BalanceRepo : IBalanceRepo
    {
        private readonly AppDbContext _context;

        public BalanceRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddBalanceAsync(LeaveBalance leaveBalance)
        {
            await _context.LeaveBalances.AddAsync(leaveBalance);
        }

        public async Task<IEnumerable<LeaveBalance>> GetAllBalanceAsync()
        {
            return await _context.LeaveBalances.ToListAsync();
        }

        public async Task<IEnumerable<LeaveBalance>> GetBalanceByUserIdAsync(Guid userId)
        {
            return await _context.LeaveBalances.
                Include(x => x.LeaveType).
                Where(x => x.UserId == userId).
                ToListAsync();
        }

        public async Task<LeaveBalance?> GetBalanceByUserAndTypeAsync(Guid userId, Guid leaveTypeId)
        {
            return await _context.LeaveBalances.FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.LeaveTypeId == leaveTypeId
            );
        }

        public async Task SaveBalanceAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<LeaveBalance> GetBalanceByIdAsync(Guid id)
        {
            return await _context.LeaveBalances.
            Include(x => x.LeaveType).
            Include(x => x.User).
            FirstOrDefaultAsync(x => x.LeaveBalanceId == id);
        }

        public async Task UpdateBalanceAsync(LeaveBalance balance)
        {
            _context.LeaveBalances.Update(balance);
        }
    }
}