using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly IComputerService _computerService;

        public ComputerController(IComputerService computerService)
        {
            _computerService = computerService;
        }

        [HttpGet("ComputerInfo/List")]
        public async Task<ActionResult<IEnumerable<ComputerInfoReturn>>> ListComputerInfo()
        {
            var computerInfos = await _computerService.ListComputerInfoReturnsAsync();

            return computerInfos;
        }

        [HttpGet("ComputerDetail/{computerId}")]
        public async Task<ActionResult<ComputerDetailReturn>> GetComputerDetail(int computerId)
        {
            var computerDetail = await _computerService.GetComputerDetailAsync(computerId);
            return computerDetail;
        }

        [HttpPost]
        public async Task<ActionResult<ComputerDetailReturn>> CreateComputer(CreateComputerRequest request)
        {
            var computerDetail = await _computerService.CreateComputerAsync(request);
            return computerDetail;
        }
    }
}
