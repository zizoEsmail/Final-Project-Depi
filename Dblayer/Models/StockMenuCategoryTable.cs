using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class StockMenuCategoryTable
{
    public int StockMenuCategoryId { get; set; }

    public string? StockMenuCategory { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual ICollection<StockMenuItemTable> StockMenuItemTables { get; set; } = new List<StockMenuItemTable>();
}
