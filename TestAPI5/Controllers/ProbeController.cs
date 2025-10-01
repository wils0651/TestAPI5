using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProbeController : ControllerBase
    {
        IProbeService _probeService;
        public ProbeController(IProbeService probeService)
        {
            _probeService = probeService;
        }

        [HttpGet("{probeId}")]
        public async Task<ActionResult<ProbeReturn>> GetById(int probeId)
        {
            var probeReturn = await _probeService.GetByIdAsync(probeId);

            return probeReturn;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProbeReturn>>> List()
        {
            var probeReturns = await _probeService.ListAsync();

            return probeReturns;
        }
    }
}
