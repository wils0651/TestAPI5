using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerTaskController : ControllerBase
    {
        private readonly IComputerTaskService _computerTaskService;

        public ComputerTaskController(IComputerTaskService computerTaskService)
        {
            this._computerTaskService = computerTaskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerTaskReturn>>> GetTasks()
        {
            var computerTasks = await _computerTaskService.ListAsync();
            return computerTasks;
        }
    }
}
