using System.Text.Json;
using LeaveService.DTO;
using LeaveService.Models;
using LeaveService.Repository;

namespace LeaveService.Service
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepo _repository;

        public LeaveTypeService(ILeaveTypeRepo repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse<LeaveType>> CreateLeaveTypeAsync(CreateLeaveTypeDto dto)
        {
            // check leave type duplicate
            var existLeaveType = await _repository.GetLeaveTypeByTypeAsync(dto.LeaveType);

            // Console.WriteLine("--> Query --" + JsonSerializer.Serialize(existLeaveType));

            if(existLeaveType != null)
            {
                return new ServiceResponse<LeaveType>
                {
                    IsSuccess = false,
                    Message = "Leave Type is already exists!"
                };
            }

            var type = new LeaveType
            {
                LeaveTypeId = Guid.NewGuid(),
                Type = dto.LeaveType
            };

            await _repository.AddLeaveTypeAsync(type);
            await _repository.SaveLeaveTypeAsync();

            return new ServiceResponse<LeaveType>
            {
                IsSuccess = true,
                Message = "LeaveType created successfully!",
                Data = type
            };
        }

        public async Task<IEnumerable<LeaveTypeResponseDto>> GetAllLevaeTypesAsync()
        {
            var levaeTypes = await _repository.GetLeaveTypeAllAsync();

            return levaeTypes.Select(x => new LeaveTypeResponseDto
            {
                Id = x.LeaveTypeId,
                LeaveType = x.Type
            });
        }
    }
}