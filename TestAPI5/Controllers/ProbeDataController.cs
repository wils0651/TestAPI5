using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProbeDataController : ControllerBase
    {
        private readonly IProbeDataService _probeDataService;

        public ProbeDataController(IProbeDataService probeDataService)
        {
            _probeDataService = probeDataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProbeDataReturn>>> GetProbeData(DateTime? startDate)
        {
            var probeData = await _probeDataService.ListAsync(startDate);

            return probeData;
        }
    }
}
