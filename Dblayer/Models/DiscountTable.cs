using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class DiscountTable
{
    public int DiscountId { get; set; }

    public string? DiscountCode { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public bool? IsUseStatus { get; set; }

    public virtual ICollection<OrderItemDetailTable> OrderItemDetailTables { get; set; } = new List<OrderItemDetailTable>();
}
