using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IComputerRepository _computerRepository;

        public MessageService(IMessageRepository messageRepository, IComputerRepository computerRepository)
        {
            _messageRepository = messageRepository;
            _computerRepository = computerRepository;
        }

        public async Task<List<MessageReturn>> GetMessagesAsync()
        {
            var messages = await _messageRepository.GetMessagesAsync();

            return messages
                .Select(MapToReturn)
                .OrderByDescending(m => m.CreatedDate)
                .ToList();
        }

        public async Task<MessageReturn> GetByIdAsync(long messageId)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);

            return MapToReturn(message);
        }

        private static MessageReturn MapToReturn(Message message)
        {
            if (message == null)
            {
                return null;
            }

            return new MessageReturn
            {
                MessageId = message.MessageId,
                ComputerId = message.ComputerId,
                ComputerName = message.Computer.Name,
                ComputerDescription = message.Computer.Description,
                ComputerTaskId = message.ComputerTaskId,
                ComputerTaskName = message.ComputerTask.Name,
                ComputerTaskDescription = message.ComputerTask.Description,
                CreatedDate = message.CreatedDate,
                Note = message.Note,
                IsStale = DateTime.Now.Subtract(message.CreatedDate).TotalHours > 24
            };
        }
    }
}
