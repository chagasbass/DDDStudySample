using Despensa.Compartilhados.DomainEvents;
using MediatR;
using UserOperations.Core.Entities;
using OrganizaDespensa.SharedKernel.Core.Enums;

namespace UserOperations.Core.Events
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
