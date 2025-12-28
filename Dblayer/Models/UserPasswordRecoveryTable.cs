using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dblayer.Models;

public partial class UserPasswordRecoveryTable
{
    [Key]
    public int UserPasswordRecoveryId { get; set; }

    public int UserId { get; set; }

    public string? RecoveryCode { get; set; }

    public DateTime RecoveryCodeExpiryDateTime { get; set; }

    public bool RecoveryStatus { get; set; }

    public string? OldPassword { get; set; }

    public virtual UserTable User { get; set; } = null!;
}