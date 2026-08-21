using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnowForecastController : ControllerBase
    {
        private readonly ISnowForecastService _snowForecastService;

        public SnowForecastController(ISnowForecastService snowForecastService)
        {
            _snowForecastService = snowForecastService;
        }

        [HttpGet("Latest")]
        public async Task<ActionResult<IEnumerable<SnowForecastReturn>>> GetLatestForecast()
        {
            var forecasts = await _snowForecastService.GetLatestForecastAsync();
            return forecasts;
        }
    }
}
