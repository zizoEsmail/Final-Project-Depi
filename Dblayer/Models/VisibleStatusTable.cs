using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class VisibleStatusTable
{
    public int VisibleStatusId { get; set; }

    public string VisibleStatus { get; set; } = null!;

    public virtual ICollection<StockDealDetailTable> StockDealDetailTables { get; set; } = new List<StockDealDetailTable>();

    public virtual ICollection<StockDealTable> StockDealTables { get; set; } = new List<StockDealTable>();

    public virtual ICollection<StockItemCategoryTable> StockItemCategoryTables { get; set; } = new List<StockItemCategoryTable>();

    public virtual ICollection<StockItemTable> StockItemTables { get; set; } = new List<StockItemTable>();

    public virtual ICollection<StockMenuItemTable> StockMenuItemTables { get; set; } = new List<StockMenuItemTable>();

    public virtual ICollection<UserAddressTable> UserAddressTables { get; set; } = new List<UserAddressTable>();
}
