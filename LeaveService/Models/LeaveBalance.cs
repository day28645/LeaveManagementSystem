using System;
using System.Collections.Generic;

namespace LeaveService.Models;

public partial class LeaveBalance
{
    public Guid LeaveBalanceId { get; set; }

    public Guid LeaveTypeId { get; set; }

    public Guid UserId { get; set; }

    public int TotalQuata { get; set; }

    public int UsedDays { get; set; }

    public virtual LeaveType LeaveType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
