using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitFindingController : ControllerBase
    {
        private readonly IPermitWatchService _permitWatchService;

        public PermitFindingController(IPermitWatchService permitWatchService)
        {
            _permitWatchService = permitWatchService;
        }

        [HttpGet("Recent")]
        public async Task<ActionResult<IEnumerable<PermitFindingReturn>>> GetRecentFindings(DateTime? startDate)
        {
            var findings = await _permitWatchService.ListRecentFindingsAsync(startDate);

            return findings;
        }
    }
}
