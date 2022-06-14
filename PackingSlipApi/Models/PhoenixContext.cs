using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PackingSlipApi.Models
{
    public partial class PhoenixContext : DbContext
    {
        public PhoenixContext()
        {
        }

        public PhoenixContext(DbContextOptions<PhoenixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; } = null!;
        public virtual DbSet<OrderShipToAddress> OrderShipToAddresses { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:PhoenixDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.AccountTypeCode, "IX_Account_AccountTypeCode");

                entity.HasIndex(e => e.City, "IX_Account_City");

                entity.HasIndex(e => e.Company, "IX_Account_Company");

                entity.HasIndex(e => e.ContactId, "IX_Account_ContactId");

                entity.HasIndex(e => e.Country, "IX_Account_Country");

                entity.HasIndex(e => e.CreatedDate, "IX_Account_CreatedDate");

                entity.HasIndex(e => e.Department, "IX_Account_Department");

                entity.HasIndex(e => e.GroupAccountId, "IX_Account_GroupAccountId");

                entity.HasIndex(e => e.ModifiedDate, "IX_Account_ModifiedDate");

                entity.HasIndex(e => e.Phone, "IX_Account_Phone");

                entity.HasIndex(e => e.Phone2, "IX_Account_Phone2");

                entity.HasIndex(e => e.State, "IX_Account_State");

                entity.HasIndex(e => e.StatusCode, "IX_Account_StatusCode");

                entity.HasIndex(e => e.Street, "IX_Account_Street");

                entity.HasIndex(e => e.Zip, "IX_Account_Zip");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountOwnerCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AccountTypeCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.BankruptcyCaseNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(65)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DefaultPaymentMethod)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DefaultPaymentTerms)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnteredBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MarketCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SoftwareExecutiveCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SourceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.StatusReasonCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubAccountTypes)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuspendCallsUntilDate).HasColumnType("datetime");

                entity.Property(e => e.Website)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Contact_Account_ContactId");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.HasIndex(e => e.AccountId, "IX_Contact_AccountId");

                entity.HasIndex(e => e.CellPhone, "IX_Contact_CellPhone");

                entity.HasIndex(e => e.CreatedDate, "IX_Contact_CreatedDate");

                entity.HasIndex(e => e.Email, "IX_Contact_Email");

                entity.HasIndex(e => e.Fax, "IX_Contact_Fax");

                entity.HasIndex(e => e.FirstName, "IX_Contact_FirstName");

                entity.HasIndex(e => e.LastName, "IX_Contact_LastName");

                entity.HasIndex(e => e.ModifiedDate, "IX_Contact_ModifiedDate");

                entity.HasIndex(e => e.Phone, "IX_Contact_Phone");

                entity.HasIndex(e => e.Salesperson, "IX_Contact_Salesperson");

                entity.HasIndex(e => e.StatusCode, "IX_Contact_StatusCode");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AffiliationCodes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(65)
                    .IsUnicode(false);

                entity.Property(e => e.ContactType)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DefaultPaymentMethod)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Degree)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EnteredBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoEmailAvailableDate).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasingRole)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Salesperson)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Salutation)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SourceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.StatusReasonCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SuspendCallsUntilDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TitleCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.WebRegistrationId)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Contact_AccountId");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.HasIndex(e => e.AccountId, "IX_OrderDetail_AccountId");

                entity.HasIndex(e => e.AccountOwnerCode, "IX_OrderDetail_AccountOwnerCode");

                entity.HasIndex(e => e.BatchId, "IX_OrderDetail_BatchId");

                entity.HasIndex(e => e.CreatedDate, "IX_OrderDetail_CreatedDate");

                entity.HasIndex(e => e.EnteredDate, "IX_OrderDetail_EnteredDate");

                entity.HasIndex(e => e.ModifiedDate, "IX_OrderDetail_ModifiedDate");

                entity.HasIndex(e => e.OpenQuantity, "IX_OrderDetail_OpenQuantity");

                entity.HasIndex(e => e.OrderDetailStatus, "IX_OrderDetail_OrderDetailStatus");

                entity.HasIndex(e => e.OrderId, "IX_OrderDetail_OrderId");

                entity.HasIndex(e => e.OriginalOrderDetailId, "IX_OrderDetail_OriginalOrderDetailId");

                entity.HasIndex(e => e.ParentOrderDetailId, "IX_OrderDetail_ParentOrderDetailId");

                entity.HasIndex(e => e.PendingDate, "IX_OrderDetail_PendingDate");

                entity.HasIndex(e => e.ProductCode, "IX_OrderDetail_ProductCode");

                entity.HasIndex(e => e.ProductId, "IX_OrderDetail_ProductId");

                entity.HasIndex(e => e.RequiredDate, "IX_OrderDetail_RequiredDate");

                entity.HasIndex(e => e.SalespersonCode, "IX_OrderDetail_SalespersonCode");

                entity.HasIndex(e => e.ShipDate, "IX_OrderDetail_ShipDate");

                entity.HasIndex(e => e.ShippedQuantity, "IX_OrderDetail_ShippedQuantity");

                entity.HasIndex(e => e.SubscriptionId, "IX_OrderDetail_SubscriptionId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountOwnerCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Discount).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.EnteredBy)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.ExtendedPrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.FloorPrice).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.GLCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ListPrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.MedicaidState)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderDetailStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Oti)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.PendingComponents)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.PendingDate).HasColumnType("datetime");

                entity.Property(e => e.PrintVendorCost).HasColumnType("decimal(12, 3)");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RequiredDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar](30),getdate(),(101)),0))");

                entity.Property(e => e.SalespersonCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TaxRate).HasColumnType("decimal(7, 3)");

                entity.Property(e => e.TrackingNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VoidDate).HasColumnType("datetime");

                entity.Property(e => e.VoidReasonCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_OrderDetail_AccountId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderHeader_OrderDetail_OrderId");

                entity.HasOne(d => d.OriginalOrderDetail)
                    .WithMany(p => p.InverseOriginalOrderDetail)
                    .HasForeignKey(d => d.OriginalOrderDetailId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_OrderDetail_ProductId");
            });

            modelBuilder.Entity<OrderHeader>(entity =>
            {
                entity.ToTable("OrderHeader");

                entity.HasIndex(e => e.AccountId, "IX_OrderHeader_AccountId");

                entity.HasIndex(e => e.AccountOwnerCode, "IX_OrderHeader_AccountOwnerCode");

                entity.HasIndex(e => e.CCAuthorizationCode, "IX_OrderHeader_CCAuthorizationCode");

                entity.HasIndex(e => e.CCLastFour, "IX_OrderHeader_CCLastFour");

                entity.HasIndex(e => e.ContactId, "IX_OrderHeader_ContactId");

                entity.HasIndex(e => e.CreatedDate, "IX_OrderHeader_CreatedDate");

                entity.HasIndex(e => e.EnteredDate, "IX_OrderHeader_EnteredDate");

                entity.HasIndex(e => e.GLCode, "IX_OrderHeader_GLCode");

                entity.HasIndex(e => e.InUseBy, "IX_OrderHeader_InUseBy");

                entity.HasIndex(e => e.IsRush, "IX_OrderHeader_IsRush");

                entity.HasIndex(e => e.LeadId, "IX_OrderHeader_LeadId");

                entity.HasIndex(e => e.ListCode, "IX_OrderHeader_ListCode");

                entity.HasIndex(e => e.ModifiedDate, "IX_OrderHeader_ModifiedDate");

                entity.HasIndex(e => e.OpportunityId, "IX_OrderHeader_OpportunityId");

                entity.HasIndex(e => e.OrderDate, "IX_OrderHeader_OrderDate");

                entity.HasIndex(e => e.OrderStatus, "IX_OrderHeader_OrderStatus");

                entity.HasIndex(e => e.OrderType, "IX_OrderHeader_OrderType");

                entity.HasIndex(e => e.OriginalOrderId, "IX_OrderHeader_OriginalOrderId");

                entity.HasIndex(e => e.PONumber, "IX_OrderHeader_PONumber");

                entity.HasIndex(e => e.PaymentMethod, "IX_OrderHeader_PaymentMethod");

                entity.HasIndex(e => e.PendingDate, "IX_OrderHeader_PendingDate");

                entity.HasIndex(e => e.PromotionCode, "IX_OrderHeader_PromotionCode");

                entity.HasIndex(e => e.ReturnCode, "IX_OrderHeader_ReturnCode");

                entity.HasIndex(e => e.ReviewedBy, "IX_OrderHeader_ReviewedBy");

                entity.HasIndex(e => e.ReviewedDate, "IX_OrderHeader_ReviewedDate");

                entity.HasIndex(e => e.SourceCode, "IX_OrderHeader_SourceCode");

                entity.HasIndex(e => e.WebReferenceNumber, "IX_OrderHeader_WebReferenceNumber");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountOwnerCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AmaKey)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ArsLabel)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CCApprovalCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CCApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.CCAuthorizationCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CCExpirationDate)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CCLastFour)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CCLastProcessedDate).HasColumnType("datetime");

                entity.Property(e => e.CCLockDate).HasColumnType("datetime");

                entity.Property(e => e.CCOldPaymentMethod)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CCSecurityCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CCToken)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnteredBy)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FreeFormComments)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GLCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HoldReasonCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.InUseBy)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.InUseDate).HasColumnType("datetime");

                entity.Property(e => e.ListCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OpenAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.OrderType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PONumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PaymentPlan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentTerms)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PendingBy)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PendingDate).HasColumnType("datetime");

                entity.Property(e => e.PromotionCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.QuoteReasonCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ReturnCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewedDate).HasColumnType("datetime");

                entity.Property(e => e.ShipmentMethod)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.SourceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SystemComments)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VoidDate).HasColumnType("datetime");

                entity.Property(e => e.VoidReasonCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.WebReferenceNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.OrderHeaders)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_OrderHeader_AccountId");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.OrderHeaders)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_OrderHeader_ContactId");

                entity.HasOne(d => d.OriginalOrder)
                    .WithMany(p => p.InverseOriginalOrder)
                    .HasForeignKey(d => d.OriginalOrderId);
            });

            modelBuilder.Entity<OrderShipToAddress>(entity =>
            {
                entity.ToTable("OrderShipToAddress");

                entity.HasIndex(e => e.Country, "IX_OrderByToAddress_Country");

                entity.HasIndex(e => e.City, "IX_OrderShipToAddress_City");

                entity.HasIndex(e => e.Company, "IX_OrderShipToAddress_Company");

                entity.HasIndex(e => e.ContactId, "IX_OrderShipToAddress_ContactId");

                entity.HasIndex(e => e.Country, "IX_OrderShipToAddress_Country");

                entity.HasIndex(e => e.CreatedDate, "IX_OrderShipToAddress_CreatedDate");

                entity.HasIndex(e => e.ModifiedDate, "IX_OrderShipToAddress_ModifiedDate");

                entity.HasIndex(e => e.State, "IX_OrderShipToAddress_State");

                entity.HasIndex(e => e.Street, "IX_OrderShipToAddress_Street");

                entity.HasIndex(e => e.Zip, "IX_OrderShipToAddress_Zip");

                entity.HasIndex(e => e.OrderId, "UIX_OrderShipToAddress_OrderId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.State)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.OrderShipToAddresses)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_OrderShipToAddress_ContactId");

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.OrderShipToAddress)
                    .HasForeignKey<OrderShipToAddress>(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderHeader_OrderShipToAddress_OrderId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.Acronym, "IX_Product_Acronym");

                entity.HasIndex(e => e.BrandCode, "IX_Product_BrandCode");

                entity.HasIndex(e => e.ClassCode, "IX_Product_ClassCode");

                entity.HasIndex(e => e.CreatedDate, "IX_Product_CreatedDate");

                entity.HasIndex(e => e.Description, "IX_Product_Description");

                entity.HasIndex(e => e.EditionYear, "IX_Product_EditionYear");

                entity.HasIndex(e => e.FulfillmentTypeCode, "IX_Product_FulfillmentTypeCode");

                entity.HasIndex(e => e.HasElectronicUpdates, "IX_Product_HasElectronicUpdates");

                entity.HasIndex(e => e.InventoryType, "IX_Product_InventoryType");

                entity.HasIndex(e => e.IsEFulfilled, "IX_Product_IsEFulfilled");

                entity.HasIndex(e => e.Isbn, "IX_Product_Isbn");

                entity.HasIndex(e => e.MiscCode, "IX_Product_MiscCode");

                entity.HasIndex(e => e.ModifiedDate, "IX_Product_ModifiedDate");

                entity.HasIndex(e => e.StatusCode, "IX_Product_StatusCode");

                entity.HasIndex(e => e.Title, "IX_Product_Title");

                entity.HasIndex(e => e.WapId, "IX_Product_WapId");

                entity.HasIndex(e => e.ProductCode, "UIX_Product_ProductCode")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Acronym)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.BrandCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTypeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpectedShipDate).HasColumnType("datetime");

                entity.Property(e => e.FloorPrice).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.Format)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FulfillmentTypeCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GLCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ige)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InventoryType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTypeCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ListPrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.MaxDiscountPercent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.MiscCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ObsoleteDate).HasColumnType("datetime");

                entity.Property(e => e.OnlineType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OutOfStockNote).IsUnicode(false);

                entity.Property(e => e.PriceListCategory)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PrintVendorCost).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProductGroupCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProductManagerCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ProductSubGroupCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SalesCost).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.SalesOrderNote).IsUnicode(false);

                entity.Property(e => e.SecondaryCategoryCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TaxClassCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.UnitWeight).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.VendorCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
