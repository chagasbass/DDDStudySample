using Flunt.Notifications;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.DomainEvents;
using OrganizaDespensa.SharedKernel.Core.Enums;
using ProductOperations.Core.Commands;
using ProductOperations.Core.DomainServices;
using ProductOperations.Core.Events;
using ProductOperations.Core.Repositories;
using ProductOperations.Core.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ProductOperations.Core.Handlers
{
    public class UpdateProductHandler : Notifiable<Notification>, IRequestHandler<UpdateProductCommand, ICommandResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFileService _productFileService;
        private readonly IDomainEntityEventHandler<ProductOperationDomainEvent> _domainEntityEventHandler;

        public UpdateProductHandler(IProductRepository productRepository, IProductFileService productFileService,
            IDomainEntityEventHandler<ProductOperationDomainEvent> domainEntityEventHandler)
        {
            _productRepository = productRepository;
            _productFileService = productFileService;
            _domainEntityEventHandler = domainEntityEventHandler;
        }

        public async Task<ICommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (!request.IsValid)
                return new CommandResult(false, "Problemas ao atualizar o produto..", request.Notifications);

            var requestedProduct = await _productRepository.GetProductAsync(request.Id);

            if (requestedProduct is null)
                return new CommandResult(false, "O Produto não foi encontrado", null);

            var productData = new ProductData(request.Name, request.Description);

            var productImage = _productFileService.ProcessProductImage(request.ImageBase64);

            productData.ChangeImage(productImage);

            var datetimeProduct = new DateTimeProduct(request.PurchaseDate, requestedProduct.DateTimeProduct.ExpirationDate);
            var productInformation = new ProductInformation(request.Quantity, request.MeasurementUnit);

            requestedProduct.ChangeProductData(productData);
            requestedProduct.ChangeDateTimeProduct(datetimeProduct);
            requestedProduct.ChangeProductInformation(productInformation);
            requestedProduct.ChangeStatus(request.ProductStatus);
            requestedProduct.ChangeCategory(request.Category);

            var updatedProduct = await _productRepository.UpdateProductAsync(requestedProduct);

            if (updatedProduct is null)
                return new CommandResult(false, "Problemas ao atualizar o produto..", null);

            if (!updatedProduct.VerifyQuantity())
            {
                var buyingProduct = true;
                var productDomainEvent = new ProductOperationDomainEvent(EventAction.UPDATE, updatedProduct, buyingProduct);

                await _domainEntityEventHandler.Raise(productDomainEvent);
            }

            return new CommandResult(true, "Produto atualizado com sucesso!", request);
        }
    }
}
