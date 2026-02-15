using System;
using System.Collections.Generic;
using LeaveService.Data;

namespace LeaveService.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public Guid? RoleId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime CreateAt { get; set; }
    
    public Guid? ReportTo { get; set; }

    public virtual ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();

    public virtual ICollection<LeaveRequest> LeaveRequestApprovedByNavigations { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestUsers { get; set; } = new List<LeaveRequest>();

    public virtual Role? Role { get; set; }
}
