using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageDistanceController : ControllerBase
    {
        private readonly IGarageDistanceService _garageDistanceService;

        public GarageDistanceController(IGarageDistanceService garageDistanceService)
        {
            _garageDistanceService = garageDistanceService;
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<GarageDistanceReturn>>> ListGarageDistances(int count = 25)
        {
            var garageDistances = await _garageDistanceService.ListAsync(count);
            return garageDistances;
        }

        [HttpGet("Latest")]
        public async Task<ActionResult<GarageDistanceReturn>> GetLatestGarageDistance()
        {
            var garageDistance = await _garageDistanceService.GetLatestAsync();
            return garageDistance;
        }
    }
}
