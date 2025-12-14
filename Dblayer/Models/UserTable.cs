using System;
using System.Collections.Generic;

namespace Dblayer.Models;

public partial class UserTable
{
    public int UserId { get; set; }

    public int UserTypeId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ContactNo { get; set; }

    public string? EmailAddress { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public int GenderId { get; set; }

    public int? UserStatusId { get; set; }

    public DateOnly? UserStatusChangeData { get; set; }

    public virtual GenderTable Gender { get; set; } = null!;

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();

    public virtual ICollection<TableReservationTable> TableReservationTableProcessByUsers { get; set; } = new List<TableReservationTable>();

    public virtual ICollection<TableReservationTable> TableReservationTableReservationUsers { get; set; } = new List<TableReservationTable>();

    public virtual ICollection<UserAddressTable> UserAddressTables { get; set; } = new List<UserAddressTable>();

    public virtual ICollection<UserDetailTable> UserDetailTables { get; set; } = new List<UserDetailTable>();

    public virtual UserStatusTable? UserStatus { get; set; }

    public virtual UserTypeTable UserType { get; set; } = null!;
}
