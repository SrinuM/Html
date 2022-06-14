using PackingSlipApi.Dtos;

namespace PackingSlipApi.Interface
{
    public interface IPackingSlipService
    {
        byte[] GeneratPDF(PackingSlipInputDto order);
    }
}
