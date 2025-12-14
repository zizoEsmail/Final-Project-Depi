using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class OrderDealDetailTable
{
    public int OrderDealDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? StockDealId { get; set; }

    public int? Qty { get; set; }

    public double? DealPrice { get; set; }

    public virtual OrderTable? Order { get; set; }

    public virtual StockDealTable? StockDeal { get; set; }
}
