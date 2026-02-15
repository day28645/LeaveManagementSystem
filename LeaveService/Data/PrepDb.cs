using LeaveService.Models;

namespace LeaveService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope() )
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        
        private static void SeedData(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                Console.WriteLine("--> Seeding Roles...");

                var adminRole = new Role { RoleId = Guid.NewGuid(), RoleName = "Manager" };
                var employeeRole = new Role { RoleId = Guid.NewGuid(), RoleName = "Employee" };

                context.Roles.AddRange(adminRole, employeeRole);
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                Console.WriteLine("--> Seeding Users...");

                var managerRole = context.Roles.First(r => r.RoleName == "Manager");

                var manager = new User 
                { 
                    UserId = Guid.NewGuid(), FullName = "Manager", Email = "manager@test.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    RoleId = managerRole.RoleId,
                    CreateAt = DateTime.Now
                };

                context.Users.Add(manager);

                var employeeRole = context.Roles.First(r => r.RoleName == "Employee");

                var employee = new User
                {
                    UserId = Guid.NewGuid(),
                    FullName = "Employee",
                    Email = "employee@test.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    RoleId = employeeRole.RoleId,
                    CreateAt = DateTime.Now,
                    ReportTo = manager.UserId
                };

                context.Users.Add(employee);

                context.SaveChanges();
            }

            if (!context.LeaveTypes.Any())
            {
                Console.WriteLine("--> Seeding LeaveTypes...");

                context.LeaveTypes.AddRange(
                    new LeaveType { LeaveTypeId = Guid.NewGuid(), Type = "Sick Leave" },
                    new LeaveType { LeaveTypeId = Guid.NewGuid(), Type = "Annual Leave" }
                );

                context.SaveChanges();
            }

            if (!context.LeaveBalances.Any())
            {
                Console.WriteLine("--> Seeding LeaveBalances...");

                var users = context.Users.ToList();
                var leaveTypes = context.LeaveTypes.ToList();

                foreach (var user in users)
                {
                    foreach (var leaveType in leaveTypes)
                    {
                        int quota = 0;

                        if (leaveType.Type == "Sick Leave") { quota = 30; }
                        else if (leaveType.Type == "Annual Leave") { quota = 10; }

                        context.LeaveBalances.Add( new LeaveBalance
                        {
                            LeaveBalanceId = Guid.NewGuid(), UserId = user.UserId, LeaveTypeId = leaveType.LeaveTypeId, TotalQuata = quota, UsedDays = 0
                        });
                    }
                }

                context.SaveChanges();
            }

        }
    }
}