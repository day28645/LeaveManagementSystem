namespace LeaveService.DTO
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Guid ReportToId { get; set; }
    }
}