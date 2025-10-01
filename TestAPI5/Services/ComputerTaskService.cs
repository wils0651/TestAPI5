using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class ComputerTaskService : IComputerTaskService
    {
        private readonly IComputerTaskRepository _computerTaskRepository;

        public ComputerTaskService(IComputerTaskRepository computerTaskRepository)
        {
            this._computerTaskRepository = computerTaskRepository;
        }

        public async Task<List<ComputerTaskReturn>> ListAsync()
        {
            var computerTasks = await _computerTaskRepository.ListAsync();
            return computerTasks
                .Select(MapToReturn)
                .ToList();
        }

        private ComputerTaskReturn MapToReturn(ComputerTask computerTask)
        {
            if (computerTask == null)
            {
                return null;
            }

            return new ComputerTaskReturn
            {
                ComputerTaskId = computerTask.ComputerTaskId,
                Name = computerTask.Name,
                Description = computerTask.Description,
            };
        }
    }
}
