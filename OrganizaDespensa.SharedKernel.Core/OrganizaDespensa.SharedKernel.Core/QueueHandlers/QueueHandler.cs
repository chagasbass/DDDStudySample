using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;
using System.Threading.Tasks;

namespace OrganizaDespensa.SharedKernel.Core.QueueHandlers
{
    public class QueueHandler : IQueueHandler
    {
        private readonly Mediator _mediator;

        public QueueHandler(Mediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ICommandResult> SendCommand<T>(T command) where T : ICommand
        {
            var commandResult = (CommandResult)await _mediator.Send(command);

            return commandResult;
        }
    }
}
