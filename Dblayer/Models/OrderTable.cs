using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class OrderTable
{
    public int OrderId { get; set; }

    public int? OrderByUserId { get; set; }

    public DateTime? OrderDateTime { get; set; }

    public int? OrderTypeId { get; set; }

    public int? DeliveryAddressUserAddressId { get; set; }

    public string? OrderReceivedByContactNo { get; set; }

    public string? OrderReceivedByFullName { get; set; }

    public int? OrderStatusId { get; set; }

    public string? Description { get; set; }

    public int? ProcessByUserId { get; set; }

    public virtual UserAddressTable? DeliveryAddressUserAddress { get; set; }

    public virtual UserTable? OrderByUser { get; set; }

    public virtual ICollection<OrderDealDetailTable> OrderDealDetailTables { get; set; } = new List<OrderDealDetailTable>();

    public virtual ICollection<OrderItemDetailTable> OrderItemDetailTables { get; set; } = new List<OrderItemDetailTable>();

    public virtual OrderStatusTable? OrderStatus { get; set; }

    public virtual OrderTypeTable? OrderType { get; set; }
}
