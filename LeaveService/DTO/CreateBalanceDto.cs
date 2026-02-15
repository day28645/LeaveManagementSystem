namespace LeaveService.DTO
{
    public class CreateBalanceDto
    {
        public Guid UserId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int TotalQuata { get; set; }
        public int UsedDays { get; set; }
    }
}