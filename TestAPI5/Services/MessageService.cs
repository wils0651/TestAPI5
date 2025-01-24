using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
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
            this._messageRepository = messageRepository;
            this._computerRepository = computerRepository;
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

        public async Task<List<ComputerInfoReturn>> ListComputerInfoReturnsAsync()
        {
            var computers = await _computerRepository.ListComputersAsync();

            var computerInfos = new List<ComputerInfoReturn>();

            foreach (var computer in computers)
            {
                var lastMessage = await _messageRepository.LastMessageByComputerIdAsync(computer.ComputerId);

                var computerInfo = new ComputerInfoReturn()
                {
                    ComputerId = computer.ComputerId,
                    ComputerName = computer.Name,
                    ComputerTaskName = lastMessage.ComputerTask.Name,
                    MessageDate = lastMessage.CreatedDate,
                    IpAddress = lastMessage.Computer.IpAddress
                };

                computerInfos.Add(computerInfo);
            }

            return computerInfos;
        }

        private static MessageReturn MapToReturn(Message message)
        {
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
                Note = message.Note
            };
        }
    }
}
