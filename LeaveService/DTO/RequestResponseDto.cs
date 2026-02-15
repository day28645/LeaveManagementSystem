namespace LeaveService.DTO
{
    public class RequestResponseDto 
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public Guid LeaveBalanceId { get; set; }
        public Guid ApprovedBy { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? TotalDays { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreateAt { get; set; }
    }
    
}