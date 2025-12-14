using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class StockDealTable
{
    public int StockDealId { get; set; }

    public string? StockDealTitle { get; set; }

    public decimal? DealPrice { get; set; }

    public int? VisibleStatusId { get; set; }

    public DateTime? StockDealStartDate { get; set; }

    public decimal? Discount { get; set; }

    public DateTime? StockDealEndDate { get; set; }

    public DateTime? StockDealRegisterDate { get; set; }

    public virtual ICollection<OrderDealDetailTable> OrderDealDetailTables { get; set; } = new List<OrderDealDetailTable>();

    public virtual ICollection<StockDealDetailTable> StockDealDetailTables { get; set; } = new List<StockDealDetailTable>();

    public virtual VisibleStatusTable? VisibleStatus { get; set; }
}
