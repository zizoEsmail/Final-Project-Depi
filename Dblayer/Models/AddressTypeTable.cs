using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class AddressTypeTable
{
    public int AddressTypeId { get; set; }

    public string AddressType { get; set; } = null!;

    public virtual ICollection<UserAddressTable> UserAddressTables { get; set; } = new List<UserAddressTable>();
}
