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
    public class ComputerService : IComputerService
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IMessageRepository _messageRepository;

        public ComputerService(IComputerRepository computerRepository, IMessageRepository messageRepository)
        {
            _computerRepository = computerRepository;
            _messageRepository = messageRepository;
        }

        public async Task<List<ComputerInfoReturn>> ListComputerInfoReturnsAsync()
        {
            var computers = await _computerRepository.ListComputersAsync();
            var computerInfos = new List<ComputerInfoReturn>();

            foreach (var computer in computers)
            {
                var lastMessage = await _messageRepository.LastMessageByComputerIdAsync(computer.ComputerId);

                var computerInfo = new ComputerInfoReturn
                {
                    ComputerId = computer.ComputerId,
                    ComputerName = computer.Name,
                    ComputerDescription = computer.Description,
                    ComputerTaskName = lastMessage.ComputerTask.Name,
                    MessageDate = lastMessage.CreatedDate,
                    IpAddress = lastMessage.Computer.IpAddress,
                    IsStale = DateTime.Now.Subtract(lastMessage.CreatedDate).TotalHours > 24
                };

                computerInfos.Add(computerInfo);
            }

            return computerInfos;
        }

        public async Task<ComputerDetailReturn> GetComputerDetailAsync(int computerId)
        {
            var computer = await _computerRepository.GetComputerByIdAsync(computerId);

            if (computer == null)
            {
                return null;
            }

            var messages = await _messageRepository.ListMessageByComputerIdAsync(computerId, page: 1, pageSize: 10);

            var computerTaskReturns = messages
                .Select(m => new ComputerTaskReturn
                {
                    ComputerTaskId = m.ComputerTaskId,
                    Name = m.ComputerTask.Name,
                    Description = m.ComputerTask.Description,
                    MessageDate = m.CreatedDate,
                })
                .ToList();

            return new ComputerDetailReturn
            {
                ComputerId = computer.ComputerId,
                ComputerName = computer.Name,
                ComputerDescription = computer.Description,
                IpAddress = computer.IpAddress,
                ComputerTasks = computerTaskReturns
            };
        }

        public async Task<ComputerDetailReturn> CreateComputerAsync(CreateComputerRequest request)
        {
            var computer = new Computer
            {
                Name = request.Name,
                Description = request.Description,
            };

            _computerRepository.Add(computer);
            await _computerRepository.SaveChangesAsync();

            return new ComputerDetailReturn()
            {
                ComputerId = computer.ComputerId,
                ComputerName = computer.Name,
                ComputerDescription = computer.Description,
            };
        }
    }
}
