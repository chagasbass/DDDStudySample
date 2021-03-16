using Despensa.Compartilhados.DomainEvents;
using MediatR;
using OperacoesDeUsuario.Core.Entities;
using OrganizaDespensa.SharedKernel.Core.Enums;

namespace OperacoesDeUsuario.Core.Events
{
    public class UserOperationDomainEvent : INotification, IDomainEntityEvent
    {
        public EventAction EventAction { get; set; }
        public User User { get; set; }

        public UserOperationDomainEvent(EventAction eventAction,User user)
        {
            EventAction = eventAction;
            User = user;
        }
    }
}
