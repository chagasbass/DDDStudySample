using Despensa.Compartilhados.DomainEvents;
using MediatR;
using System.Threading.Tasks;

namespace OrganizaDespensa.SharedKernel.Core.DomainEvents
{
    public class DomainEntityEventHandler : IDomainEntityEventHandler<IDomainEntityEvent>
    {
        private readonly Mediator _mediator;

        public DomainEntityEventHandler(Mediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Raise(IDomainEntityEvent domainEvent) => await _mediator.Publish(domainEvent);
    }
}
