using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class UserDetailTable
{
    public int UserDetailId { get; set; }

    public int UserId { get; set; }

    public DateTime? UserDataProvidedDate { get; set; }

    public string? PhotoPath { get; set; }

    public string? Cnic { get; set; }

    public string? EducationLevel { get; set; }

    public string? EducationLastDegreeScanPath { get; set; }

    public string? LastExperienceScanPhotoPath { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual UserTable User { get; set; } = null!;
}
