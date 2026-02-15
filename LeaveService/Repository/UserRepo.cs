using LeaveService.Data;
using LeaveService.DTO;
using LeaveService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LeaveService.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetUserAllAsync()
        {
            return await _context.Users
            .Include(u => u.Role).ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }
        
        public async Task SaveUserAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserReportToByIdAsync(Guid employeeId)
        {
            // var result = await _context.Set<UserReportToDto>()
            //     .FromSqlRaw(@"
            //         SELECT 
            //             e.UserId,
            //             e.FullName AS EmployeeName,
            //             e.ReportTo,
            //             m.FullName AS ManagerName
            //         FROM Users e
            //         JOIN Users m ON e.ReportTo = m.UserId
            //         WHERE e.UserId = @userId",
            //         new SqlParameter("@userId", employeeId))
            //     .AsNoTracking()
            // .FirstOrDefaultAsync();

            // return result;
            var employee = await _context.Users
                .Where(u => u.UserId == employeeId && 
                       u.ReportTo != null)
                .Select(u => u.ReportTo)
                .FirstOrDefaultAsync();

            if (employee == null) return null;

            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == employee);
        }
    }
}