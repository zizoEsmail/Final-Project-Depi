using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class StockMenuItemTable
{
    public int StockMenuItemId { get; set; }

    public int? StockMenuCategoryId { get; set; }

    public int? StockItemId { get; set; }

    public int? VisibleStatusId { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual StockItemTable? StockItem { get; set; }

    public virtual StockMenuCategoryTable? StockMenuCategory { get; set; }

    public virtual VisibleStatusTable? VisibleStatus { get; set; }
}
