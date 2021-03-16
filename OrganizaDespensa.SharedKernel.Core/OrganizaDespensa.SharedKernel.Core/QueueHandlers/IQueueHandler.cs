using OrganizaDespensa.SharedKernel.Core.Commands;
using System.Threading.Tasks;

namespace OrganizaDespensa.SharedKernel.Core.QueueHandlers
{
    public interface IQueueHandler
    {
        Task<ICommandResult> SendCommand<T>(T command) where T : ICommand;
    }
}
