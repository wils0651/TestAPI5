using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitWatchController : ControllerBase
    {
        private readonly IPermitWatchService _permitWatchService;

        public PermitWatchController(IPermitWatchService permitWatchService)
        {
            _permitWatchService = permitWatchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermitWatchReturn>>> GetWatchConfig()
        {
            var watchConfig = await _permitWatchService.ListWatchConfigAsync();

            return watchConfig;
        }
    }
}
