using System;
using System.Collections.Generic;

namespace PackingSlipApi.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            InverseOriginalOrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int AccountId { get; set; }
        public int OrderId { get; set; }
        public int? SubscriptionId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int? ParentOrderDetailId { get; set; }
        public int? OriginalOrderDetailId { get; set; }
        public string OrderDetailStatus { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public decimal Discount { get; set; }
        public decimal PrintVendorCost { get; set; }
        public decimal ListPrice { get; set; }
        public decimal FloorPrice { get; set; }
        public int OpenQuantity { get; set; }
        public int ShippedQuantity { get; set; }
        public int VoidedQuantity { get; set; }
        public int? OriginalQuantity { get; set; }
        public decimal ExtendedPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public DateTime EnteredDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public int? BatchId { get; set; }
        public string? AccountOwnerCode { get; set; }
        public string? EnteredBy { get; set; }
        public string? PendingComponents { get; set; }
        public int? PendingQuantity { get; set; }
        public DateTime? PendingDate { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime? VoidDate { get; set; }
        public string? VoidReasonCode { get; set; }
        public string? SalespersonCode { get; set; }
        public bool IsWebFulfilled { get; set; }
        public string ClassCode { get; set; } = null!;
        public string? MedicaidState { get; set; }
        public string? Oti { get; set; }
        public string GLCode { get; set; } = null!;
        public string? ReferenceNumber { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual OrderHeader Order { get; set; } = null!;
        public virtual OrderDetail? OriginalOrderDetail { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<OrderDetail> InverseOriginalOrderDetail { get; set; }
    }
}
