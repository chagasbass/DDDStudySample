using Flunt.Notifications;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.DomainEvents;
using OrganizaDespensa.SharedKernel.Core.Enums;
using ProductOperations.Core.Commands;
using ProductOperations.Core.DomainServices;
using ProductOperations.Core.Entities;
using ProductOperations.Core.Events;
using ProductOperations.Core.Repositories;
using ProductOperations.Core.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ProductOperations.Core.Handlers
{
    public class CreateProductHandler : Notifiable<Notification>, IRequestHandler<CreateProductCommand, ICommandResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFileService _productFileService;
        private readonly IDomainEntityEventHandler<ProductOperationDomainEvent> _domainEntityEventHandler;

        public CreateProductHandler(IProductRepository productRepository, IProductFileService productFileService,
            IDomainEntityEventHandler<ProductOperationDomainEvent> domainEntityEventHandler)
        {
            _productRepository = productRepository;
            _productFileService = productFileService;
            _domainEntityEventHandler = domainEntityEventHandler;
        }

        public async Task<ICommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (!request.IsValid)
                return new CommandResult(false, "Problemas ao cadastrar o produto..", request.Notifications);

            var productExists = await _productRepository.VerifyProductoAsync(request.Name, request.User);

            if (productExists)
                return new CommandResult(false, "O produto já esta cadastrado.", null);

            var productData = new ProductData(request.Name, request.Description);
            var datetimeProduct = new DateTimeProduct(request.PurchaseDate, request.ExpirationDate);
            var produtctInformation = new ProductInformation(request.Quantity, request.MeasurementUnit);

            var productImage = _productFileService.ProcessProductImage(request.ImageBase64);

            productData.ChangeImage(productImage);

            var newProduct = new Product(productData, datetimeProduct, produtctInformation, request.Category, request.User);

            var createdProduct = await _productRepository.CreateProductAsync(newProduct);

            if (createdProduct is null)
                return new CommandResult(false, "O produto não foi cadastrado.", null);

            var buyingProduct = false;
            var productDomainEvent = new ProductOperationDomainEvent(EventAction.INSERT, createdProduct, buyingProduct);

            await _domainEntityEventHandler.Raise(productDomainEvent);

            return new CommandResult(true, "Produto cadastrado com sucesso!", request);
        }
    }
}
