using System;
using System.Collections.Generic;
using LeaveService.Models;

namespace LeaveService.Models;

public partial class LeaveRequest
{
    public Guid LeaveRequestId { get; set; }

    public Guid LeaveTypeId { get; set; }

    public Guid UserId { get; set; }

    public Guid ApprovedBy { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? TotalDays { get; set; }

    public string? Status { get; set; }

    public string? Reason { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual User ApprovedByNavigation { get; set; } = null!;

    public virtual LeaveType LeaveType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
