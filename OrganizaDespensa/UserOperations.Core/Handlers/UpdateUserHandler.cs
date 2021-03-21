using Flunt.Notifications;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.DomainEvents;
using OrganizaDespensa.SharedKernel.Core.Enums;
using System.Threading;
using System.Threading.Tasks;
using UserOperations.Core.Commands;
using UserOperations.Core.Events;
using UserOperations.Core.Repositories;

namespace UserOperations.Core.Handlers
{
    public  class UpdateUserHandler : Notifiable<Notification>, IRequestHandler<UpdateUserCommad, ICommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDomainEntityEventHandler<UserOperationDomainEvent> _domainEntityEventHandler;

        public UpdateUserHandler(IUserRepository userRepository, IDomainEntityEventHandler<UserOperationDomainEvent> domainEntityEventHandler)
        {
            _userRepository = userRepository;
            _domainEntityEventHandler = domainEntityEventHandler;
        }

        public async Task<ICommandResult> Handle(UpdateUserCommad request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (!request.IsValid)
                return new CommandResult(false, "Os dados do usuário estão incorretos.", request.Notifications);

            var usuarioEncontrado = await _userRepository.GetUserByUserCodeAsync(request.UserCode);

            if (usuarioEncontrado == null)
                return new CommandResult(true, "Usuario não encontrado", null);

            usuarioEncontrado.ChangeShoppingDay(request.ShoppingDay);

            var usuarioCadastrado = await _userRepository.UpdateUserAsync(usuarioEncontrado);

            if (usuarioCadastrado == null)
                return new CommandResult(false, "Problemas ao efetuar a atualização do usuário.", null);

            var userOperationDomainEvent = new UserOperationDomainEvent(EventAction.UPDATE, usuarioCadastrado);

            await _domainEntityEventHandler.Raise(userOperationDomainEvent);

            return new CommandResult(true, "Usuário atualizado com sucesso!", request);
        }
    }
}