using System;
using System.Collections.Generic;
using LeaveService.Models;

namespace LeaveService.Models;

public partial class LeaveType
{
    public Guid LeaveTypeId { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
