using System;
using System.Collections.Generic;

namespace PackingSlipApi.Models
{
    public partial class OrderHeader
    {
        public OrderHeader()
        {
            InverseOriginalOrder = new HashSet<OrderHeader>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int AccountId { get; set; }
        public int ContactId { get; set; }
        public int WarehouseId { get; set; }
        public int? OriginalOrderId { get; set; }
        public string? AccountOwnerCode { get; set; }
        public string? ModifiedBy { get; set; }
        public string EnteredBy { get; set; } = null!;
        public DateTime EnteredDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string? PONumber { get; set; }
        public bool IsRush { get; set; }
        public string ShipmentMethod { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string PaymentTerms { get; set; } = null!;
        public string? PaymentPlan { get; set; }
        public string OrderStatus { get; set; } = null!;
        public string OrderType { get; set; } = null!;
        public string? InUseBy { get; set; }
        public string? PromotionCode { get; set; }
        public string? SourceCode { get; set; }
        public string? ListCode { get; set; }
        public DateTime? VoidDate { get; set; }
        public DateTime? PendingDate { get; set; }
        public string? PendingBy { get; set; }
        public string? HoldReasonCode { get; set; }
        public string? QuoteReasonCode { get; set; }
        public string? VoidReasonCode { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? ReviewedBy { get; set; }
        public string? WebReferenceNumber { get; set; }
        public string GLCode { get; set; } = null!;
        public string? CCAuthorizationCode { get; set; }
        public string? CCApprovalCode { get; set; }
        public DateTime? CCApprovalDate { get; set; }
        public string? CCSecurityCode { get; set; }
        public string? CCExpirationDate { get; set; }
        public string? CCLastFour { get; set; }
        public DateTime? CCLockDate { get; set; }
        public string? CCOldPaymentMethod { get; set; }
        public string? CCToken { get; set; }
        public int? ApplyToInvoiceId { get; set; }
        public string? SystemComments { get; set; }
        public string? FreeFormComments { get; set; }
        public decimal OpenAmount { get; set; }
        public decimal ShippedAmount { get; set; }
        public DateTime? CCLastProcessedDate { get; set; }
        public string? AmaKey { get; set; }
        public string? ArsLabel { get; set; }
        public DateTime? InUseDate { get; set; }
        public int? LeadId { get; set; }
        public int? OpportunityId { get; set; }
        public string? ReturnCode { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Contact Contact { get; set; } = null!;
        public virtual OrderHeader? OriginalOrder { get; set; }
        public virtual OrderShipToAddress OrderShipToAddress { get; set; } = null!;
        public virtual ICollection<OrderHeader> InverseOriginalOrder { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
