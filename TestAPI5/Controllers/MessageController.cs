using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: api/Message
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageReturn>>> GetMessages()
        {
            var messages = await _messageService.GetMessagesAsync();

            return messages;
        }

        [HttpGet("{messageId}")]
        public async Task<ActionResult<MessageReturn>> GetMessage(long messageId)
        {
            var message = await _messageService.GetByIdAsync(messageId);

            return message;
        }
    }
}
