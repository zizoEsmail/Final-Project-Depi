using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class UserTypeTable
{
    public int UserTypeId { get; set; }

    public string? UserType { get; set; }

    public virtual ICollection<UserTable> UserTables { get; set; } = new List<UserTable>();
}
