using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class UserAddressTable
{
    public int UserAddressId { get; set; }

    public int UserId { get; set; }

    public int AddressTypeId { get; set; }

    public string FullAddress { get; set; } = null!;

    public int VisibleStatusId { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual AddressTypeTable AddressType { get; set; } = null!;

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();

    public virtual UserTable User { get; set; } = null!;

    public virtual VisibleStatusTable VisibleStatus { get; set; } = null!;
}
