using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class UnclassifiedMessageService : IUnclassifiedMessageService
    {
        private readonly IUnclassifiedMessageRepository _unclassifiedMessageRepository;

        public UnclassifiedMessageService(IUnclassifiedMessageRepository unclassifiedMessageRepository)
        {
            this._unclassifiedMessageRepository = unclassifiedMessageRepository;
        }

        public async Task<List<UnclassifiedMessageReturn>> ListAsync()
        {
            var unclassifiedMessages = await _unclassifiedMessageRepository.ListAsync();

            return unclassifiedMessages
                .Select(MapToReturn)
                .OrderByDescending(r => r.CreatedDate)
                .ToList();
        }

        public async Task<List<UnclassifiedMessageReturn>> ListLatestAsync(int numberOfDays)
        {
            var unclassifiedMessages = await _unclassifiedMessageRepository.ListLatestAsync(numberOfDays);

            return unclassifiedMessages
                .Select(MapToReturn)
                .OrderByDescending(r => r.CreatedDate)
                .ToList();
        }

        private UnclassifiedMessageReturn MapToReturn(UnclassifiedMessage unclassifiedMessage)
        {
            if (unclassifiedMessage == null)
            {
                return null;
            }

            return new UnclassifiedMessageReturn
            {
                UnclassifiedMessageId = unclassifiedMessage.UnclassifiedMessageId,
                MessageContent = unclassifiedMessage.MessageContent,
                CreatedDate = unclassifiedMessage.CreatedDate,
            };
        }
    }
}
