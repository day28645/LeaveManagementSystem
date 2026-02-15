using System.Text.Json;
using LeaveService.DTO;
using LeaveService.Models;
using LeaveService.Repository;

namespace LeaveService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repository;

        public UserService(IUserRepo repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<User>>CreateUserAsync(CreateUserDto dto)
        {
            // check user duplicate
            var existUser = await _repository.GetByEmailAsync(dto.Email);

            // Console.WriteLine("--> Query --" + JsonSerializer.Serialize(existUser));

            if(existUser != null)
            {
                return new ServiceResponse<User>
                {
                    IsSuccess = false,
                    Message = "Email is already exists!"
                };
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId,
                CreateAt = DateTime.Now,
                ReportTo = dto.ReportToId
            };

            await _repository.AddUserAsync(user);
            await _repository.SaveUserAsync();

            return new ServiceResponse<User>
            {
                IsSuccess = true,
                Message = "User created successfully!",
                Data = user
            };
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetUserAllAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                RoleId = u.Role.RoleId,
                CreateAt = u.CreateAt
            });
        }

        public async Task<ServiceResponse<User>> GetUserReportToByIdAsync(Guid id)
        {
            var user = await _repository.GetUserReportToByIdAsync(id);

            if(user == null)
            {
                return new ServiceResponse<User>
                {
                    IsSuccess = false,
                    Message = "There is no Manager to report!"
                };
            }

            return new ServiceResponse<User>
            {
                IsSuccess = true,
                Message = "Get ReportTo successfully!",
                Data = user
            };
        }
    }
}