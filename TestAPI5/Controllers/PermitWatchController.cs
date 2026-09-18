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

        [HttpPost]
        public async Task<ActionResult<PermitWatchReturn>> SaveWatchWindow(SaveWatchWindowRequest request)
        {
            var permitWatch = await _permitWatchService.SaveWatchWindowAsync(request);

            return permitWatch;
        }

        [HttpPost("Exception")]
        public async Task<ActionResult<PermitWatchReturn>> SaveWatchDateException(SaveWatchDateExceptionRequest request)
        {
            var permitWatch = await _permitWatchService.SaveWatchDateExceptionAsync(request);

            return permitWatch;
        }
    }
}
