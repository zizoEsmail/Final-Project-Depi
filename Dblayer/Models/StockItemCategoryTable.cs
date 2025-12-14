using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class StockItemCategoryTable
{
    public int StockItemCategoryId { get; set; }

    public string? StockItemCategory { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? VisibleStatusId { get; set; }

    public virtual ICollection<StockItemTable> StockItemTables { get; set; } = new List<StockItemTable>();

    public virtual VisibleStatusTable? VisibleStatus { get; set; }
}
