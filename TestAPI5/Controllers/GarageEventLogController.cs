using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageEventLogController : ControllerBase
    {
        private readonly IGarageEventLogService _garageEventLogService;

        public GarageEventLogController(IGarageEventLogService garageEventLogService)
        {
            _garageEventLogService = garageEventLogService;
        }

        [HttpGet("Latest")]
        public async Task<ActionResult<GarageEventLogReturn>> GetLatestGarageEventLog()
        {
            var garageEventLog = await _garageEventLogService.GetLatestAsync();
            return garageEventLog;
        }

        [HttpGet("ListLatest")]
        public async Task<ActionResult<List<GarageEventLogReturn>>> GetLatestGarageEventLogs(int count = 25)
        {
            var garageEventLogs = await _garageEventLogService.ListLatestAsync(count);
            return garageEventLogs;
        }
    }
}
