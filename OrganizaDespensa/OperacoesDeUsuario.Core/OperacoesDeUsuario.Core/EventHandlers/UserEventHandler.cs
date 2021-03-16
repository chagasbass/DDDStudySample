﻿using Despensa.Compartilhados.DomainEvents;
using MediatR;
using OperacoesDeUsuario.Core.Events;
using OrganizaDespensa.SharedKernel.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OperacoesDeUsuario.Core.EventHandlers
{
    public class UserEventHandler : IDomainEntityEvent, INotificationHandler<UserOperationDomainEvent>
    {
        private readonly IEmailService _emailService;

        public UserEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(UserOperationDomainEvent notification, CancellationToken cancellationToken)
        {
            var emailText = $"Seja bem vindo {notification.User.Name}";

            await _emailService.SendEmail(notification.User.Email.Endereco, notification.User.Name, emailText);
        }
    }
}
