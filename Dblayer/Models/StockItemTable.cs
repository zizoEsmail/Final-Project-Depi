using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class StockItemTable
{
    public int StockItemId { get; set; }

    public int? StockItemCategoryId { get; set; }

    public string? ItemPhotoPath { get; set; }

    public string? StockItemTitle { get; set; }

    public string? ItemSize { get; set; }

    public decimal? UnitPrice { get; set; }

    public DateTime? RegisterDate { get; set; }

    public int? VisibleStatusId { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? OrderTypeId { get; set; }

    public virtual ICollection<OrderItemDetailTable> OrderItemDetailTables { get; set; } = new List<OrderItemDetailTable>();

    public virtual OrderTypeTable? OrderType { get; set; }

    public virtual ICollection<StockDealDetailTable> StockDealDetailTables { get; set; } = new List<StockDealDetailTable>();

    public virtual StockItemCategoryTable? StockItemCategory { get; set; }

    public virtual ICollection<StockItemIngredientTable> StockItemIngredientTables { get; set; } = new List<StockItemIngredientTable>();

    public virtual ICollection<StockMenuItemTable> StockMenuItemTables { get; set; } = new List<StockMenuItemTable>();

    public virtual VisibleStatusTable? VisibleStatus { get; set; }
}
