
using System.Text.Json;
using LeaveService.DTO;
using LeaveService.Models;
using LeaveService.Repository;
using Microsoft.AspNetCore.Http.HttpResults;


namespace LeaveService.Service
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepo _repository;
        private readonly ILeaveTypeRepo _leaveTypeRepo;

        public BalanceService(IBalanceRepo repository, ILeaveTypeRepo leaveTypeRepo)
        {
            _repository = repository;
            _leaveTypeRepo = leaveTypeRepo;
        }
        public async Task<ServiceResponse<LeaveBalance>> CreateBalanceAsync(CreateBalanceDto dto)
        {
            // check balance duplicate and leave type
            var existBalance = await _repository.GetBalanceByUserAndTypeAsync(dto.UserId, dto.LeaveTypeId);

            // Console.WriteLine("--> Query --" + JsonSerializer.Serialize(existBalance));

            if(existBalance != null)
            {
                return new ServiceResponse<LeaveBalance>
                {
                    IsSuccess = false,
                    Message = "LeaveBalance is already exists!"
                };
            }

            // check leave type
            var leaveType = await _leaveTypeRepo.GetLeaveTypeByIdAsync(dto.LeaveTypeId);

            int leaveDays = 0;

            if(leaveType.Type == "Sick Leave")
            {
                leaveDays = 30;
            }
            else if(leaveType.Type == "Annual Leave")
            {
                leaveDays = 10;
            }
            else
            {
                return new ServiceResponse<LeaveBalance>
                {
                    IsSuccess = false,
                    Message = "Invalid leave type!"
                };
            }

            var leaveBalance = new LeaveBalance
            {
                LeaveBalanceId = Guid.NewGuid(),
                TotalQuata = leaveDays,
                UsedDays = dto.UsedDays,
                LeaveTypeId = dto.LeaveTypeId,
                UserId = dto.UserId
            };

            await _repository.AddBalanceAsync(leaveBalance);
            await _repository.SaveBalanceAsync();

            return new ServiceResponse<LeaveBalance>
            {
                IsSuccess = true,
                Message = "LeaveBalance created successfully!",
                Data = leaveBalance
            };
        }
        public async Task<ServiceResponse<List<BalanceResponseDto>>> GetBalanceByUserIdAsync(Guid userId)
        {
            var balances = await _repository.GetBalanceByUserIdAsync(userId);

            //Console.WriteLine("--> Query --" + JsonSerializer.Serialize(balances));

            if (!balances.Any())
            {
                return new ServiceResponse<List<BalanceResponseDto>>
                {
                    IsSuccess = false,
                    Message = "No balance found!"
                };
            }

            var response = balances.Select(b => new BalanceResponseDto
            {
                Id = b.LeaveBalanceId,
                LeaveTypeId = b.LeaveTypeId,
                LeaveTypeName = b.LeaveType.Type,
                TotalQuata = b.TotalQuata,
                UsedDays = b.UsedDays,
                RemainingDays = b.TotalQuata - b.UsedDays
            })
            .ToList();

            return new ServiceResponse<List<BalanceResponseDto>>
            {
                IsSuccess = true,
                Message = "Success!",
                Data = response
            };
        }

        public async Task<ServiceResponse<BalanceResponseDto>> GetLeaveBalanceByIdAsync(Guid id)
        {
            var balance = await _repository.GetBalanceByIdAsync(id);
            if (balance == null)
            {
                return new ServiceResponse<BalanceResponseDto>
                {
                    IsSuccess = false,
                    Message = "No balance found!"
                };
            }

            var response = new BalanceResponseDto
            {
                Id = balance.LeaveBalanceId,
                LeaveTypeId = balance.LeaveTypeId,
                LeaveTypeName = balance.LeaveType.Type,
                TotalQuata = balance.TotalQuata,
                UsedDays = balance.UsedDays,
                RemainingDays = balance.TotalQuata - balance.UsedDays
            };

            return new ServiceResponse<BalanceResponseDto>
            {
                IsSuccess = true,
                Message = "Success!",
                Data = response
            };
        }

        public async Task<ServiceResponse<UpdateBalanceDto>> UpdateBalanceAsync(Guid id, UpdateBalanceDto dto)
        {
            // check balance
            var existBalance = await _repository.GetBalanceByIdAsync(dto.BalanceId);

            if (existBalance == null)
            {
                return new ServiceResponse<UpdateBalanceDto>
                {
                    IsSuccess = false,
                    Message = "No balance found!"
                };
            }

            // check negative leave day
            if(dto.UsedDays < 0)
            {
                return new ServiceResponse<UpdateBalanceDto>
                {
                    IsSuccess = false,
                    Message = "Used days cannot be negative!"
                };
            }

            //checek total quota 
            if(dto.UsedDays > existBalance.TotalQuata)
            {
                return new ServiceResponse<UpdateBalanceDto>
                {
                    IsSuccess = false,
                    Message = $"Used days cannot exceed {existBalance.TotalQuata}."
                };
            }

            existBalance.UsedDays = dto.UsedDays;

            await _repository.SaveBalanceAsync();

            return new ServiceResponse<UpdateBalanceDto>
            {
                IsSuccess = true,
                Message = "Success!",
            };
        }
    }
}