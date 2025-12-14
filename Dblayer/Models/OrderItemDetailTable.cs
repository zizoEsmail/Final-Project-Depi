using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class OrderItemDetailTable
{
    public int OrderItemDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? StockItemId { get; set; }

    public int? Qty { get; set; }

    public double? UnitPrice { get; set; }

    public int? DiscountId { get; set; }

    public double? DiscountAmount { get; set; }

    public virtual DiscountTable? Discount { get; set; }

    public virtual OrderTable? Order { get; set; }

    public virtual StockItemTable? StockItem { get; set; }
}
