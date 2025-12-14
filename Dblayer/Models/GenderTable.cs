using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class GenderTable
{
    public int GenderId { get; set; }

    public string GenderTitle { get; set; } = null!;

    public virtual ICollection<UserTable> UserTables { get; set; } = new List<UserTable>();
}
