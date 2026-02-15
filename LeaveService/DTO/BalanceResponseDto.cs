namespace LeaveService.DTO
{
    public class BalanceResponseDto
    {
        public Guid Id { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public int TotalQuata { get; set; }
        public int UsedDays { get; set; }
        public int RemainingDays { get; set; }
    }
}