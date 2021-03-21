using Flunt.Notifications;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.DomainEvents;
using OrganizaDespensa.SharedKernel.Core.Enums;
using ProductOperations.Core.Commands;
using ProductOperations.Core.Events;
using ProductOperations.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ProductOperations.Core.Handlers
{
    public class DeleteProductHandler : Notifiable<Notification>, IRequestHandler<DeleteProductCommand, ICommandResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDomainEntityEventHandler<ProductOperationDomainEvent> _domainEntityEventHandler;

        public DeleteProductHandler(IProductRepository productRepository,
            IDomainEntityEventHandler<ProductOperationDomainEvent> domainEntityEventHandler)
        {
            _productRepository = productRepository;
            _domainEntityEventHandler = domainEntityEventHandler;
        }

        public async Task<ICommandResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (!request.IsValid)
                return new CommandResult(false, "Problemas ao cadastrar o produto..", request.Notifications);

            var deletedProduct = await _productRepository.GetProductAsync(request.Id);

            if (deletedProduct == null)
                return new CommandResult(false, "O produto não foi encontrado.", null);

            await _productRepository.DeleteProductAsync(deletedProduct);

            var buyingProduct = false;
            var productDomainEvent = new ProductOperationDomainEvent(EventAction.DELETE, deletedProduct, buyingProduct);

            await _domainEntityEventHandler.Raise(productDomainEvent);

            return new CommandResult(true, "Produto excluído com sucesso!", request);
        }
    }
}
