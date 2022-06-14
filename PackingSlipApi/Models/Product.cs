using System;
using System.Collections.Generic;

namespace PackingSlipApi.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ProductCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Title { get; set; }
        public string? ProductManagerCode { get; set; }
        public string StatusCode { get; set; } = null!;
        public string MiscCode { get; set; } = null!;
        public decimal PrintVendorCost { get; set; }
        public decimal SalesCost { get; set; }
        public decimal ListPrice { get; set; }
        public decimal FloorPrice { get; set; }
        public string ClassCode { get; set; } = null!;
        public string? InventoryType { get; set; }
        public string? Acronym { get; set; }
        public string? FulfillmentTypeCode { get; set; }
        public string? BrandCode { get; set; }
        public int? EditionYear { get; set; }
        public decimal? UnitWeight { get; set; }
        public string? Format { get; set; }
        public string? Isbn { get; set; }
        public int? CartonQuantity { get; set; }
        public string ProductGroupCode { get; set; } = null!;
        public string? ProductSubGroupCode { get; set; }
        public string? ItemTypeCode { get; set; }
        public string? ProductTypeCode { get; set; }
        public string? PriceListCategory { get; set; }
        public string SecondaryCategoryCode { get; set; } = null!;
        public DateTime? ExpectedShipDate { get; set; }
        public DateTime? ObsoleteDate { get; set; }
        public bool? HasElectronicUpdates { get; set; }
        public bool? IsOutOfStock { get; set; }
        public bool EmailAlert { get; set; }
        public bool IsStockProduct { get; set; }
        public bool IsRatable { get; set; }
        public bool AllowMultiYear { get; set; }
        public bool IsSubscription { get; set; }
        public bool? AllowTrial { get; set; }
        public bool RequiresProfileForm { get; set; }
        public bool IsCustomProduct { get; set; }
        public bool ExemptShipping { get; set; }
        public bool ShowSalesOrderNote { get; set; }
        public bool RequiresReview { get; set; }
        public bool IsEFulfilled { get; set; }
        public bool HasMultiUserPricing { get; set; }
        public string? OutOfStockNote { get; set; }
        public string? SalesOrderNote { get; set; }
        public string? VendorCode { get; set; }
        public string? OnlineType { get; set; }
        public int? DefaultSubscriptionTerm { get; set; }
        public string? EmailTypeCode { get; set; }
        public int? WapId { get; set; }
        public string? TaxClassCode { get; set; }
        public string? Ige { get; set; }
        public decimal MaxDiscountPercent { get; set; }
        public int SubscriptionMultiplier { get; set; }
        public string GLCode { get; set; } = null!;
        public bool BlockOnline { get; set; }
        public bool RequiresAmaKey { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
