using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class OrderStatusTable
{
    public int OrderStatusId { get; set; }

    public string? OrderStatus { get; set; }

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
