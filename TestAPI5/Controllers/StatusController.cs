using Microsoft.AspNetCore.Mvc;

namespace TestAPI5.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetStatus()
        {
            return "true";
        }
    }
}
