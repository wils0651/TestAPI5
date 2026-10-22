using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("HindcastSeasons")]
        public async Task<ActionResult<SnowHindcastSeasonsReturn>> GetHindcastSeasons()
        {
            return await _snowForecastService.GetHindcastSeasonsAsync();
        }

        // leads is a comma-separated list of lead days, e.g. "7,14".
        [HttpGet("Hindcast")]
        public async Task<ActionResult<SnowHindcastReturn>> GetHindcast([FromQuery] int season, [FromQuery] string leads = "7,14")
        {
            var parsedLeads = new List<int>();
            foreach (var part in (leads ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                if (!int.TryParse(part, out var lead) || lead < 1)
                {
                    return BadRequest($"Invalid lead '{part}'; leads must be positive integers.");
                }
                parsedLeads.Add(lead);
            }

            if (parsedLeads.Count == 0)
            {
                return BadRequest("At least one lead is required.");
            }

            if (season < 1900 || season > 2200)
            {
                return BadRequest("season must be a four-digit start year, e.g. 2019 for the 2019–20 season.");
            }

            return await _snowForecastService.GetHindcastAsync(season, parsedLeads.Distinct().ToList());
        }
    }
}
