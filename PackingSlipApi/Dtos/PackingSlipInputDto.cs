namespace PackingSlipApi.Dtos
{
    public class PackingSlipInputDto
    {
        public int OrderId { get; set; }
        public List<PackingSlipDetailDto> PackingSlipDetailDtos { get; set; } = new List<PackingSlipDetailDto>();
    }
}
