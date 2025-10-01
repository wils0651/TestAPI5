using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureStatisticController : ControllerBase
    {
        private readonly ITemperatureStatisticService _temperatureStatisticService;

        public TemperatureStatisticController(ITemperatureStatisticService temperatureStatisticService)
        {
            _temperatureStatisticService = temperatureStatisticService;
        }

        [HttpGet("List/{probeId}")]
        public async Task<ActionResult<IEnumerable<TemperatureStatisticReturn>>> GetTemperatureStatistics(int probeId)
        {
            var temperatureStatisticReturns = await _temperatureStatisticService.ListAsync(probeId);

            return temperatureStatisticReturns;
        }
    }
}
