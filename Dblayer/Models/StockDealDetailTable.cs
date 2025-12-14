using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class StockDealDetailTable
{
    public int StockDealDetailId { get; set; }

    public int? StockDealId { get; set; }

    public int? StockItemId { get; set; }

    public decimal? Discount { get; set; }

    public int? VisibleStatusId { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual StockDealTable? StockDeal { get; set; }

    public virtual StockItemTable? StockItem { get; set; }

    public virtual VisibleStatusTable? VisibleStatus { get; set; }
}
