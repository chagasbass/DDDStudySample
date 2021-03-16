using Flunt.Notifications;
using MediatR;
using OperacoesDeUsuario.Core.Commands;
using OperacoesDeUsuario.Core.Entities;
using OperacoesDeUsuario.Core.Events;
using OperacoesDeUsuario.Core.Repositories;
using OperacoesDeUsuario.Core.ValueObjects;
using OrganizaDespensa.SharedKernel.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.DomainEvents;
using OrganizaDespensa.SharedKernel.Core.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Despensa.Dominio.Core.Contextos.Usuarios.Fluxos
{
    public class CreateUserHandler : Notifiable<Notification>, IRequestHandler<CreateUserCommand, ICommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDomainEntityEventHandler<UserOperationDomainEvent> _domainEntityEventHandler;

        public CreateUserHandler(IUserRepository userRepository, IDomainEntityEventHandler<UserOperationDomainEvent> domainEntityEventHandler)
        {
            _userRepository = userRepository;
            _domainEntityEventHandler = domainEntityEventHandler;
        }

        public async Task<ICommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (!request.IsValid)
                return new CommandResult(false, "Os dados do usuário estão incorretos.", request.Notifications);

            var usuarioJaCadastrado = await _userRepository.VerifyUserAsync(request.UserCode);

            if (usuarioJaCadastrado)
                return new CommandResult(true, "Usuario Já cadastrado","cadastrado");

            var provedor = request.Email.Split("@");

            var email = new Email(request.Email,provedor[1]);

            var novoUsuario = new User(request.Name,email, request.UserCode, request.ShoppingDay);

            var usuarioCadastrado = await _userRepository.CreateUserAsync(novoUsuario);

            if (usuarioCadastrado == null)
                return new CommandResult(false, "Problemas ao efetuar o cadastro do usuário.", null);

            var createUserDomainEvent = new UserOperationDomainEvent(EventAction.INSERT, novoUsuario);
            await _domainEntityEventHandler.Raise(createUserDomainEvent);

            return new CommandResult(true, "Usuário cadastrado com sucesso!", request);
        }
    }
}
