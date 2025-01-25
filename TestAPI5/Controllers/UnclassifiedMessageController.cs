﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnclassifiedMessageController : ControllerBase
    {
        private readonly IUnclassifiedMessageService _unclassifiedMessageService;

        public UnclassifiedMessageController(IUnclassifiedMessageService unclassifiedMessageService)
        {
            this._unclassifiedMessageService = unclassifiedMessageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnclassifiedMessageReturn>>> GetMessages()
        {
            var unclassifiedMessages = await _unclassifiedMessageService.ListAsync();

            return unclassifiedMessages;
        }
    }
}
