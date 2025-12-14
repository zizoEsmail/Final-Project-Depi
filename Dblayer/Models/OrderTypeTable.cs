using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class OrderTypeTable
{
    public int OrderTypeId { get; set; }

    public string? OrderType { get; set; }

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();

    public virtual ICollection<StockItemTable> StockItemTables { get; set; } = new List<StockItemTable>();
}
