using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dblayer.Models;

public partial class ProPizzResturentandDrinkDbContext : DbContext
{
    public ProPizzResturentandDrinkDbContext()
    {
    }

    public ProPizzResturentandDrinkDbContext(DbContextOptions<ProPizzResturentandDrinkDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressTypeTable> AddressTypeTables { get; set; }

    public virtual DbSet<CartDetailTable> CartDetailTables { get; set; }

    public virtual DbSet<CartTable> CartTables { get; set; }

    public virtual DbSet<DiscountTable> DiscountTables { get; set; }

    public virtual DbSet<GenderTable> GenderTables { get; set; }

    public virtual DbSet<OrderDealDetailTable> OrderDealDetailTables { get; set; }

    public virtual DbSet<OrderItemDetailTable> OrderItemDetailTables { get; set; }

    public virtual DbSet<OrderStatusTable> OrderStatusTables { get; set; }

    public virtual DbSet<OrderTable> OrderTables { get; set; }

    public virtual DbSet<OrderTypeTable> OrderTypeTables { get; set; }

    public virtual DbSet<ReservationStatusTable> ReservationStatusTables { get; set; }

    public virtual DbSet<StockDealDetailTable> StockDealDetailTables { get; set; }

    public virtual DbSet<StockDealTable> StockDealTables { get; set; }

    public virtual DbSet<StockItemCategoryTable> StockItemCategoryTables { get; set; }

    public virtual DbSet<StockItemIngredientTable> StockItemIngredientTables { get; set; }

    public virtual DbSet<StockItemTable> StockItemTables { get; set; }

    public virtual DbSet<StockMenuCategoryTable> StockMenuCategoryTables { get; set; }

    public virtual DbSet<StockMenuItemTable> StockMenuItemTables { get; set; }

    public virtual DbSet<TableReservationTable> TableReservationTables { get; set; }

    public virtual DbSet<UserAddressTable> UserAddressTables { get; set; }

    public virtual DbSet<UserDetailTable> UserDetailTables { get; set; }
    
    public virtual DbSet<UserPasswordRecoveryTable> UserPasswordRecoveryTables { get; set; }

    public virtual DbSet<UserStatusTable> UserStatusTables { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    public virtual DbSet<UserTypeTable> UserTypeTables { get; set; }

    public virtual DbSet<VisibleStatusTable> VisibleStatusTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Pro_PizzResturentandDrinkDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressTypeTable>(entity =>
        {
            entity.HasKey(e => e.AddressTypeId).HasName("PK_AddressT_8BF56CC120914F20");

            entity.ToTable("AddressTypeTable");

            entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");
            entity.Property(e => e.AddressType).HasMaxLength(100);
        });

        modelBuilder.Entity<DiscountTable>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF6F957B4CE");

            entity.ToTable("DiscountTable");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.DiscountCode).HasMaxLength(50);
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<GenderTable>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK_GenderTa_4E24E8172B90A6D1");

            entity.ToTable("GenderTable");

            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.GenderTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderDealDetailTable>(entity =>
        {
            entity.HasKey(e => e.OrderDealDetailId).HasName("PK__OrderDea__A96242A1C36A7D39");

            entity.ToTable("OrderDealDetailTable");

            entity.Property(e => e.OrderDealDetailId).HasColumnName("OrderDealDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.StockDealId).HasColumnName("StockDealID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDealDetailTables)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__OrderDeal__Order__6754599E");

            entity.HasOne(d => d.StockDeal).WithMany(p => p.OrderDealDetailTables)
                .HasForeignKey(d => d.StockDealId)
                .HasConstraintName("FK__OrderDeal__Stock__68487DD7");
        });

        modelBuilder.Entity<OrderItemDetailTable>(entity =>
        {
            entity.HasKey(e => e.OrderItemDetailId).HasName("PK__OrderIte__C2300060C0E92313");

            entity.ToTable("OrderItemDetailTable");

            entity.Property(e => e.OrderItemDetailId).HasColumnName("OrderItemDetailID");
            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.StockItemId).HasColumnName("StockItemID");

            entity.HasOne(d => d.Discount).WithMany(p => p.OrderItemDetailTables)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__OrderItem__Disco__6A30C649");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItemDetailTables)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__OrderItem__Order__693CA210");

            entity.HasOne(d => d.StockItem).WithMany(p => p.OrderItemDetailTables)
                .HasForeignKey(d => d.StockItemId)
                .HasConstraintName("FK_OrderItemDetailTable_StockItemTable");
        });

        modelBuilder.Entity<OrderStatusTable>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("PK__OrderSta__BC674F41CAAD5B1C");

            entity.ToTable("OrderStatusTable");

            entity.Property(e => e.OrderStatusId)
                .ValueGeneratedNever()
                .HasColumnName("OrderStatusID");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderTable>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderTab__C3905BAF89FDF81F");

            entity.ToTable("OrderTable");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.DeliveryAddressUserAddressId).HasColumnName("DeliveryAddress_UserAddressID");
            entity.Property(e => e.OrderByUserId).HasColumnName("OrderBy_UserID");
            entity.Property(e => e.OrderDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderReceivedByContactNo)
                .HasMaxLength(20)
                .HasColumnName("OrderReceivedBy_ContactNo");
            entity.Property(e => e.OrderReceivedByFullName)
                .HasMaxLength(100)
                .HasColumnName("OrderReceivedBy_FullName");
            entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");
            entity.Property(e => e.OrderTypeId).HasColumnName("OrderTypeID");
            entity.Property(e => e.ProcessByUserId).HasColumnName("ProcessBy_UserID");

            entity.HasOne(d => d.DeliveryAddressUserAddress).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.DeliveryAddressUserAddressId)
                .HasConstraintName("FK__OrderTabl__Deliv__6C190EBB");

            entity.HasOne(d => d.OrderByUser).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.OrderByUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__OrderTabl__Order__6D0D32F4");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.OrderStatusId)
                .HasConstraintName("FK__OrderTabl__Order__6EF57B66");

            entity.HasOne(d => d.OrderType).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.OrderTypeId)
                .HasConstraintName("FK__OrderTabl__Order__6E01572D");
        });

        modelBuilder.Entity<OrderTypeTable>(entity =>
        {
            entity.HasKey(e => e.OrderTypeId).HasName("PK__OrderTyp__23AC264C4EDAEADE");

            entity.ToTable("OrderTypeTable");

            entity.Property(e => e.OrderTypeId)
                .ValueGeneratedNever()
                .HasColumnName("OrderTypeID");
            entity.Property(e => e.OrderType).HasMaxLength(50);
        });

        modelBuilder.Entity<ReservationStatusTable>(entity =>
        {
            entity.HasKey(e => e.ReservationStatusId).HasName("PK__Reservat__DFC0EF4A3A755272");

            entity.ToTable("ReservationStatusTable");

            entity.Property(e => e.ReservationStatusId)
                .ValueGeneratedNever()
                .HasColumnName("ReservationStatusID");
            entity.Property(e => e.ReservationStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<StockDealDetailTable>(entity =>
        {
            entity.HasKey(e => e.StockDealDetailId).HasName("PK__StockDea__64F0620877248BCD");

            entity.ToTable("StockDealDetailTable");

            entity.Property(e => e.StockDealDetailId).HasColumnName("StockDealDetailID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedBy_UserID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockDealId).HasColumnName("StockDealID");
            entity.Property(e => e.StockItemId).HasColumnName("StockItemID");
            entity.Property(e => e.VisibleStatusId).HasColumnName("VisibleStatusID");

            entity.HasOne(d => d.StockDeal).WithMany(p => p.StockDealDetailTables)
                .HasForeignKey(d => d.StockDealId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__StockDeal__Stock__6FE99F9F");

            entity.HasOne(d => d.StockItem).WithMany(p => p.StockDealDetailTables)
                .HasForeignKey(d => d.StockItemId)
                .HasConstraintName("FK_StockDealDetailTable_StockItemTable");

            entity.HasOne(d => d.VisibleStatus).WithMany(p => p.StockDealDetailTables)
                .HasForeignKey(d => d.VisibleStatusId)
                .HasConstraintName("FK__StockDeal__Visib__70DDC3D8");
        });

        modelBuilder.Entity<StockDealTable>(entity =>
        {
            entity.HasKey(e => e.StockDealId).HasName("PK__StockDea__CBB5E6B03F6A00DA");

            entity.ToTable("StockDealTable");

            entity.Property(e => e.StockDealId).HasColumnName("StockDealID");
            entity.Property(e => e.DealPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockDealEndDate).HasColumnType("datetime");
            entity.Property(e => e.StockDealRegisterDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StockDealStartDate).HasColumnType("datetime");
            entity.Property(e => e.StockDealTitle).HasMaxLength(100);
            entity.Property(e => e.VisibleStatusId).HasColumnName("VisibleStatusID");

            entity.HasOne(d => d.VisibleStatus).WithMany(p => p.StockDealTables)
                .HasForeignKey(d => d.VisibleStatusId)
                .HasConstraintName("FK__StockDeal__Visib__72C60C4A");
        });

        modelBuilder.Entity<StockItemCategoryTable>(entity =>
        {
            entity.HasKey(e => e.StockItemCategoryId).HasName("PK__StockIte__F656C4C21294EE6C");

            entity.ToTable("StockItemCategoryTable");

            entity.Property(e => e.StockItemCategoryId).HasColumnName("StockItemCategoryID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedBy_UserID");
            entity.Property(e => e.StockItemCategory).HasMaxLength(100);
            entity.Property(e => e.VisibleStatusId).HasColumnName("VisibleStatusID");

            entity.HasOne(d => d.VisibleStatus).WithMany(p => p.StockItemCategoryTables)
                .HasForeignKey(d => d.VisibleStatusId)
                .HasConstraintName("FK__StockItem__Visib__73BA3083");
        });

        modelBuilder.Entity<StockItemIngredientTable>(entity =>
        {
            entity.HasKey(e => e.StockItemIngredientId).HasName("PK__StockIte__8315F12151F25797");

            entity.ToTable("StockItemIngredientTable");

            entity.Property(e => e.StockItemIngredientId).HasColumnName("StockItemIngredientID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedBy_UserID");
            entity.Property(e => e.StockItemId).HasColumnName("StockItemID");
            entity.Property(e => e.StockItemIngredientPhotoPath).HasMaxLength(255);
            entity.Property(e => e.StockItemIngredientTitle).HasMaxLength(100);

            entity.HasOne(d => d.StockItem).WithMany(p => p.StockItemIngredientTables)
                .HasForeignKey(d => d.StockItemId)
                .HasConstraintName("FK_StockItemIngredientTable_StockItemTable");
        });

        modelBuilder.Entity<StockItemTable>(entity =>
        {
            entity.HasKey(e => e.StockItemId).HasName("PK__StockIte__454484DCDF5D08A1");

            entity.ToTable("StockItemTable");

            entity.Property(e => e.StockItemId).HasColumnName("StockItemID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedBy_UserID");
            entity.Property(e => e.ItemPhotoPath).HasMaxLength(255);
            entity.Property(e => e.ItemSize).HasMaxLength(50);
            entity.Property(e => e.OrderTypeId).HasColumnName("OrderTypeID");
            entity.Property(e => e.RegisterDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StockItemCategoryId).HasColumnName("StockItemCategoryID");
            entity.Property(e => e.StockItemTitle).HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VisibleStatusId).HasColumnName("VisibleStatusID");

            entity.HasOne(d => d.OrderType).WithMany(p => p.StockItemTables)
                .HasForeignKey(d => d.OrderTypeId)
                .HasConstraintName("FK_StockItemTable_OrderTypeTable");

            entity.HasOne(d => d.StockItemCategory).WithMany(p => p.StockItemTables)
                .HasForeignKey(d => d.StockItemCategoryId)
                .HasConstraintName("FK_StockItemTable_StockItemCategoryTable");

            entity.HasOne(d => d.VisibleStatus).WithMany(p => p.StockItemTables)
                .HasForeignKey(d => d.VisibleStatusId)
                .HasConstraintName("FK_StockItemTable_VisibleStatusTable");
        });

        modelBuilder.Entity<StockMenuCategoryTable>(entity =>
        {
            entity.HasKey(e => e.StockMenuCategoryId).HasName("PK__StockMen__0B77B03350AC499E");

            entity.ToTable("StockMenuCategoryTable");

            entity.Property(e => e.StockMenuCategoryId).HasColumnName("StockMenuCategoryID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedBy_UserID");
            entity.Property(e => e.StockMenuCategory).HasMaxLength(100);
        });

        modelBuilder.Entity<StockMenuItemTable>(entity =>
        {
            entity.HasKey(e => e.StockMenuItemId).HasName("PK__StockMen__858E5E943242CEA7");

            entity.ToTable("StockMenuItemTable");

            entity.Property(e => e.StockMenuItemId).HasColumnName("StockMenuItemID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedBy_UserID");
            entity.Property(e => e.StockItemId).HasColumnName("StockItemID");
            entity.Property(e => e.StockMenuCategoryId).HasColumnName("StockMenuCategoryID");
            entity.Property(e => e.VisibleStatusId).HasColumnName("VisibleStatusID");

            entity.HasOne(d => d.StockItem).WithMany(p => p.StockMenuItemTables)
                .HasForeignKey(d => d.StockItemId)
                .HasConstraintName("FK_StockMenuItemTable_StockItemTable");

            entity.HasOne(d => d.StockMenuCategory).WithMany(p => p.StockMenuItemTables)
                .HasForeignKey(d => d.StockMenuCategoryId)
                .HasConstraintName("FK__StockMenu__Stock__787EE5A0");

            entity.HasOne(d => d.VisibleStatus).WithMany(p => p.StockMenuItemTables)
                .HasForeignKey(d => d.VisibleStatusId)
                .HasConstraintName("FK__StockMenu__Visib__797309D9");
        });

        modelBuilder.Entity<TableReservationTable>(entity =>
        {
            entity.HasKey(e => e.TableReservationId).HasName("PK__TableRes__9269812FF9F52DC7");

            entity.ToTable("TableReservationTable");

            entity.Property(e => e.TableReservationId).HasColumnName("TableReservationID");
            entity.Property(e => e.EmailAddress).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.ProcessByUserId).HasColumnName("ProcessBy_UserID");
            entity.Property(e => e.ReservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReservationRequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReservationStatusId).HasColumnName("ReservationStatusID");
            entity.Property(e => e.ReservationUserId).HasColumnName("Reservation_UserID");

            entity.HasOne(d => d.ProcessByUser).WithMany(p => p.TableReservationTableProcessByUsers)
                .HasForeignKey(d => d.ProcessByUserId)
                .HasConstraintName("FK__TableRese__Proce__7B5B524B");

            entity.HasOne(d => d.ReservationStatus).WithMany(p => p.TableReservationTables)
                .HasForeignKey(d => d.ReservationStatusId)
                .HasConstraintName("FK__TableRese__Reser__7C4F7684");

            entity.HasOne(d => d.ReservationUser).WithMany(p => p.TableReservationTableReservationUsers)
                .HasForeignKey(d => d.ReservationUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TableRese__Reser__7D439ABD");
        });

        modelBuilder.Entity<UserAddressTable>(entity =>
        {
            entity.HasKey(e => e.UserAddressId).HasName("PK_UserAddr_5961BB97E67C30FA");

            entity.ToTable("UserAddressTable");

            entity.Property(e => e.UserAddressId).HasColumnName("UserAddressID");
            entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedBy_UserID");
            entity.Property(e => e.FullAddress).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VisibleStatusId).HasColumnName("VisibleStatusID");

            entity.HasOne(d => d.AddressType).WithMany(p => p.UserAddressTables)
                .HasForeignKey(d => d.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAddress_AddressType");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddressTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAddress_User");

            entity.HasOne(d => d.VisibleStatus).WithMany(p => p.UserAddressTables)
                .HasForeignKey(d => d.VisibleStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAddress_VisibleStatus");
        });

        modelBuilder.Entity<UserDetailTable>(entity =>
        {
            entity.HasKey(e => e.UserDetailId).HasName("PK_UserDeta_564F56D2A380DAFB");

            entity.ToTable("UserDetailTable");

            entity.Property(e => e.UserDetailId).HasColumnName("UserDetailID");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedBy_UserID");
            entity.Property(e => e.EducationLastDegreeScanPath).HasMaxLength(300);
            entity.Property(e => e.EducationLevel).HasMaxLength(100);
            entity.Property(e => e.LastExperienceScanPhotoPath).HasMaxLength(300);
            entity.Property(e => e.PhotoPath).HasMaxLength(300);
            entity.Property(e => e.UserDataProvidedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetailTables)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserDetail_User");
        });

        modelBuilder.Entity<UserStatusTable>(entity =>
        {
            entity.HasKey(e => e.UserStatusId).HasName("PK_UserStat_A33F541AA4719B70");

            entity.ToTable("UserStatusTable");

            entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");
            entity.Property(e => e.UserStatus).HasMaxLength(100);
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_UserTabl_1788CCAC3030ACF0");

            entity.ToTable("UserTable");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ContactNo).HasMaxLength(50);
            entity.Property(e => e.EmailAddress).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Gender).WithMany(p => p.UserTables)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_User_Gender");

            entity.HasOne(d => d.UserStatus).WithMany(p => p.UserTables)
                .HasForeignKey(d => d.UserStatusId)
                .HasConstraintName("FK_UserTable_UserStatusTable");

            entity.HasOne(d => d.UserType).WithMany(p => p.UserTables)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_User_UserType");
        });

        modelBuilder.Entity<UserTypeTable>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PK_UserType_40D2D8F6CBF79CEE");

            entity.ToTable("UserTypeTable");

            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
            entity.Property(e => e.UserType).HasMaxLength(100);
        });

        modelBuilder.Entity<VisibleStatusTable>(entity =>
        {
            entity.HasKey(e => e.VisibleStatusId).HasName("PK_VisibleS_61020CE03D8B838C");

            entity.ToTable("VisibleStatusTable");

            entity.Property(e => e.VisibleStatusId).HasColumnName("VisibleStatusID");
            entity.Property(e => e.VisibleStatus).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
