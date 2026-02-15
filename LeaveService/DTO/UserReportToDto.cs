namespace LeaveService.DTO
{
    public class UserReportToDto
{
    public Guid UserId { get; set; }
    public string EmployeeName { get; set; }
    public Guid? ReportTo { get; set; }
    public string ManagerName { get; set; }
}
}

