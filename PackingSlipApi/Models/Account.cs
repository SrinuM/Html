using System;
using System.Collections.Generic;

namespace PackingSlipApi.Models
{
    public partial class Account
    {
        public Account()
        {
            Contacts = new HashSet<Contact>();
            OrderDetails = new HashSet<OrderDetail>();
            OrderHeaders = new HashSet<OrderHeader>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ContactId { get; set; }
        public int? GroupAccountId { get; set; }
        public bool IsHeadquarters { get; set; }
        public string? AccountOwnerCode { get; set; }
        public string StatusCode { get; set; } = null!;
        public string? StatusReasonCode { get; set; }
        public string? Company { get; set; }
        public string? Department { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Phone2 { get; set; }
        public string? Website { get; set; }
        public string? Comments { get; set; }
        public string? SourceCode { get; set; }
        public string DefaultPaymentTerms { get; set; } = null!;
        public int DefaultInvoiceType { get; set; }
        public int Beds { get; set; }
        public int Doctors { get; set; }
        public int CoveredLives { get; set; }
        public string? DefaultPaymentMethod { get; set; }
        public int CreditLimit { get; set; }
        public string? BankruptcyCaseNumber { get; set; }
        public DateTime? SuspendCallsUntilDate { get; set; }
        public string AccountTypeCode { get; set; } = null!;
        public string? SubAccountTypes { get; set; }
        public string? MarketCode { get; set; }
        public string? EnteredBy { get; set; }
        public bool AllowFax { get; set; }
        public bool AllowPhone { get; set; }
        public bool AllowMail { get; set; }
        public bool AllowMerge { get; set; }
        public bool AllowEmail { get; set; }
        public bool AllowPaymentPlans { get; set; }
        public bool DefaultSendRenewalNotices { get; set; }
        public bool DefaultSendUpdates { get; set; }
        public bool SendCCInvoices { get; set; }
        public bool IsPORequired { get; set; }
        public bool IsTaxExempt { get; set; }
        public bool LeaveCMOpen { get; set; }
        public bool IsWapReseller { get; set; }
        public string? SoftwareExecutiveCode { get; set; }

        public virtual Contact? Contact { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
