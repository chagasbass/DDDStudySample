using Despensa.Compartilhados.DomainEvents;
using MediatR;
using ProductOperations.Core.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductOperations.Core.Eventhandlers
{
    public class ProductEventHandler : IDomainEntityEvent, INotificationHandler<ProductOperationDomainEvent>
    {
        public async Task Handle(ProductOperationDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
