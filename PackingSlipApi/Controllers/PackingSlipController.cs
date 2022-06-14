using Microsoft.AspNetCore.Mvc;
using PackingSlipApi.Dtos;
using PackingSlipApi.Interface;

namespace PackingSlipApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackingSlipController : ControllerBase
    {
        protected readonly IPackingSlipService _packingSlipService;
        public PackingSlipController(IPackingSlipService packingSlipService)
        {
            _packingSlipService = packingSlipService;
        }

        [HttpPost("GeneratePackingSlip")]
        public byte[] GeneratePackingSlip(PackingSlipInputDto packingSlipInputDto)
        {
            // Create a new PDF document
            return _packingSlipService.GeneratPDF(packingSlipInputDto);
        }
    }
}