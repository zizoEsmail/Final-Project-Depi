using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class ReservationStatusTable
{
    public int ReservationStatusId { get; set; }

    public string? ReservationStatus { get; set; }

    public virtual ICollection<TableReservationTable> TableReservationTables { get; set; } = new List<TableReservationTable>();
}
