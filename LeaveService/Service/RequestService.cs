using LeaveService.DTO;
using LeaveService.Models;
using LeaveService.Repository;

namespace LeaveService.Service
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepo _repository;
        private readonly IUserRepo _userRepo;
        private readonly IBalanceRepo _balanceRepo;

        public RequestService(IRequestRepo repository, IUserRepo userRepo, IBalanceRepo balanceRepo)
        {
            _repository = repository;
            _userRepo = userRepo;
            _balanceRepo = balanceRepo;
        }
        public async Task<ServiceResponse<RequestResponseDto>> CreateLRequestAsync(CreateRequestDto dto)
        {
            // check request overlap
            var isOverlap = await _repository.HasOverlappingRequestAsync(
                dto.UserId, dto.StartDate, dto.EndDate
            );
            
            if(isOverlap)
            {
                return new ServiceResponse<RequestResponseDto>
                {
                    IsSuccess = false,
                    Message = "Leave Request is Overlapping!"
                };
            }

            // Validate date
            if (dto.EndDate < dto.StartDate)
            {
                return new ServiceResponse<RequestResponseDto>
                {
                    IsSuccess = false,
                    Message = "End Date cannot be earlier than StartDate."
                };
            }           

            // calculate total days
            var totalDays = dto.EndDate.DayNumber - dto.StartDate.DayNumber + 1;

            // check ApproveBy
            var approveBy = await _userRepo.GetUserReportToByIdAsync(dto.UserId);

            var request = new LeaveRequest
            {
                LeaveRequestId = Guid.NewGuid(),
                UserId = dto.UserId,
                LeaveTypeId = dto.LeaveTypeId,
                ApprovedBy = approveBy.UserId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TotalDays = totalDays,
                Reason = dto.Reason,
                Status = "Pending",
                CreateAt = DateTime.Now
            };

            await _repository.AddRequestAsync(request);
            await _repository.SaveRequestAsync();

            var response = new RequestResponseDto
            {
                Id          = request.LeaveRequestId,
                UserId      = request.UserId,
                LeaveTypeId = request.LeaveTypeId,
                ApprovedBy  = request.ApprovedBy,
                StartDate   = request.StartDate,
                EndDate     = request.EndDate,
                TotalDays   = request.TotalDays,
                Status      = request.Status,
                Reason      = request.Reason,
                CreateAt    = request.CreateAt
            };

            return new ServiceResponse<RequestResponseDto>
            {
                IsSuccess = true,
                Message = "LeaveRequest created successfully!",
                Data = response
            };
        }

        public async Task<IEnumerable<RequestResponseDto>> GetAllRequestsAsync()
        {
            var requests = await _repository.GetAllRequestAsync();

            return requests.Select(x => new RequestResponseDto
            {
                Id = x.LeaveRequestId,
                UserId = x.UserId,
                LeaveTypeId = x.LeaveTypeId,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                TotalDays = x.TotalDays,
                Status = x.Status,
                Reason = x.Reason,
                CreateAt = x.CreateAt
            });
        }

        public async Task<ServiceResponse<LeaveRequest>> UpdateRequestAsync(Guid id, UpdateRequestDto dto)
        {
            // check leave request
            var leaveRequest = await _repository.GetRequestByIdAsync(id);

            if (leaveRequest == null)
            {
                return new ServiceResponse<LeaveRequest>
                {
                    IsSuccess = false,
                    Message = "Leave Request not found!"
                };
            }

            // pending request can be approved / reject
            if (leaveRequest.Status != "Pending")
            {
                return new ServiceResponse<LeaveRequest>
                {       
                    IsSuccess = false,
                    Message = $"Cannot update a request that is already {leaveRequest.Status}."
                };
            }

            // Approved >> deduct balance
            if (dto.Status == "Approved")
            {
                var balance = await _balanceRepo.GetBalanceByUserAndTypeAsync
                (
                    leaveRequest.UserId, 
                    leaveRequest.LeaveTypeId
                );

            if (balance == null)
            {
                return new ServiceResponse<LeaveRequest>
                {
                    IsSuccess = false,
                    Message = "Leave balance record not found!"
                };
            }

            int RemainingDays = balance.TotalQuata - balance.UsedDays;

            // check balance will not go negative
            if (RemainingDays  < leaveRequest.TotalDays)
            {
                return new ServiceResponse<LeaveRequest>
                {
                    IsSuccess = false,
                    Message = $"Insufficient leave balance. " +
                          $"Remaining: {RemainingDays} days, " +
                          $"Requested: {leaveRequest.TotalDays} days."
                    };
            }

            // deduct balance
            balance.UsedDays += leaveRequest.TotalDays ?? 0;

            await _balanceRepo.UpdateBalanceAsync(balance);
            await _balanceRepo.SaveBalanceAsync();
            }

            leaveRequest.Status     = dto.Status;          // "Approved" or "Rejected"
            leaveRequest.UpdateAt   = DateTime.Now;

            await _repository.UpdateRequestAsync(leaveRequest);

            await _repository.SaveRequestAsync();

            return new ServiceResponse<LeaveRequest>
            {
                IsSuccess = true,
                Message   = $"Leave Request has been {leaveRequest.Status} successfully!",
                Data      = leaveRequest
            };
        }
    }
}