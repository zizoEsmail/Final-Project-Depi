using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class UserStatusTable
{
    public int UserStatusId { get; set; }

    public string UserStatus { get; set; } = null!;

    public virtual ICollection<UserTable> UserTables { get; set; } = new List<UserTable>();
}
