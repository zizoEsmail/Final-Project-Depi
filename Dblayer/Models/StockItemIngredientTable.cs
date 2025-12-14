using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class StockItemIngredientTable
{
    public int StockItemIngredientId { get; set; }

    public int? StockItemId { get; set; }

    public string? StockItemIngredientPhotoPath { get; set; }

    public string? StockItemIngredientTitle { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual StockItemTable? StockItem { get; set; }
}
