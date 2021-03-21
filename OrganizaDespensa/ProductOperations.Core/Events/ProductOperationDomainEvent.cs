using Despensa.Compartilhados.DomainEvents;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Enums;
using ProductOperations.Core.Entities;

namespace ProductOperations.Core.Events
{
    public class ProductOperationDomainEvent: INotification, IDomainEntityEvent
    {
        public EventAction EventAction { get; set; }
        public Product Product { get; set; }
        public bool BuyingProduct { get; set; }

        public ProductOperationDomainEvent(EventAction eventAction, Product product, bool buyingProduct)
        {
            EventAction = eventAction;
            Product = product;
            BuyingProduct = buyingProduct;
        }
    }
}
