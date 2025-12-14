using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class TableReservationTable
{
    public int TableReservationId { get; set; }

    public int? ReservationUserId { get; set; }

    public string? FullName { get; set; }

    public string? EmailAddress { get; set; }

    public string? MobileNo { get; set; }

    public DateTime? ReservationRequestDate { get; set; }

    public DateTime? ReservationDateTime { get; set; }

    public int? NoOfPersons { get; set; }

    public int? ProcessByUserId { get; set; }

    public int? ReservationStatusId { get; set; }

    public string? Description { get; set; }

    public virtual UserTable? ProcessByUser { get; set; }

    public virtual ReservationStatusTable? ReservationStatus { get; set; }

    public virtual UserTable? ReservationUser { get; set; }
}
