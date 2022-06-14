using System;
using System.Collections.Generic;

namespace PackingSlipApi.Models
{
    public partial class OrderShipToAddress
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ContactId { get; set; }
        public int OrderId { get; set; }
        public string? Company { get; set; }
        public string? Department { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string Country { get; set; } = null!;

        public virtual Contact Contact { get; set; } = null!;
        public virtual OrderHeader Order { get; set; } = null!;
    }
}
