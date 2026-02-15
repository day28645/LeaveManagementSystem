using LeaveService.Models;

namespace LeaveService.DTO
{
    public class CreateRequestDto
    {
        public Guid UserId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Reason { get; set; }
    }
}