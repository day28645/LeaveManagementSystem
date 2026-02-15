namespace LeaveService.DTO
{
    public class UpdateBalanceDto
    {
        public Guid BalanceId { get; set; }
        public int TotalQuata { get; set; }
        public int UsedDays { get; set; }
        public string Status { get; set; }
    }
}