using Microsoft.AspNetCore.Mvc;
using System;

namespace TestAPI5.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetStatus()
        {
            var time = DateTime.Now.ToShortDateString();
            return $"Success. {time}";
        }
    }
}
