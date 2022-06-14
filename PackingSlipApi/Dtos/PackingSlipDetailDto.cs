namespace PackingSlipApi.Dtos
{
    public class PackingSlipDetailDto
    {
        public int OrderDetailId { get; set; }
        public int ShipQuantity { get; set; }
        public int BackorderQuantity { get; set; }
    }
}