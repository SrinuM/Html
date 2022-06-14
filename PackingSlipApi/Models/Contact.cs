using System;
using System.Collections.Generic;

namespace PackingSlipApi.Models
{
    public partial class Contact
    {
        public Contact()
        {
            Accounts = new HashSet<Account>();
            OrderHeaders = new HashSet<OrderHeader>();
            OrderShipToAddresses = new HashSet<OrderShipToAddress>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int AccountId { get; set; }
        public string? Salutation { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleInitial { get; set; }
        public string? LastName { get; set; }
        public string? Degree { get; set; }
        public string StatusCode { get; set; } = null!;
        public string? StatusReasonCode { get; set; }
        public string? Salesperson { get; set; }
        public string? ContactType { get; set; }
        public string? EnteredBy { get; set; }
        public string? SourceCode { get; set; }
        public string? Title { get; set; }
        public string? TitleCode { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? Fax { get; set; }
        public bool AllowMail { get; set; }
        public bool AllowEmail { get; set; }
        public bool AllowFax { get; set; }
        public bool AllowPhone { get; set; }
        public bool DefaultSendRenewalNotices { get; set; }
        public bool AllowInformationalEmails { get; set; }
        public bool AllowPromotionalEmails { get; set; }
        public bool AllowFaxPromotions { get; set; }
        public bool SendCCInvoice { get; set; }
        public bool ElectronicUpdatesOnly { get; set; }
        public DateTime? NoEmailAvailableDate { get; set; }
        public DateTime? SuspendCallsUntilDate { get; set; }
        public string? WebRegistrationId { get; set; }
        public string? AffiliationCodes { get; set; }
        public string? DefaultPaymentMethod { get; set; }
        public string? Comments { get; set; }
        public string? PurchasingRole { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
        public virtual ICollection<OrderShipToAddress> OrderShipToAddresses { get; set; }
    }
}
