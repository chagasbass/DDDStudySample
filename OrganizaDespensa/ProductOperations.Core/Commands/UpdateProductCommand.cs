using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;
using System;

namespace ProductOperations.Core.Commands
{
    public class UpdateProductCommand : Notifiable<Notification>, ICommand, IRequest<ICommandResult>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public byte?[] Image { get; set; }
        public string ImageBase64 { get; set; }
        public int Quantity { get; set; }
        public string MeasurementUnit { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ProductStatus { get; set; }
        public string User { get; set; }

        public UpdateProductCommand(string id, string name, string category, string description,
            byte?[] image, string imageBase64, int quantity, string measurementUnit, DateTime purchaseDate,
            DateTime expirationDate, string productStatus, string user)
        {
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            Image = image;
            ImageBase64 = imageBase64;
            Quantity = quantity;
            MeasurementUnit = measurementUnit;
            PurchaseDate = purchaseDate;
            ExpirationDate = expirationDate;
            ProductStatus = productStatus;
            User = user;
        }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
             .Requires()
             .IsNotNullOrEmpty(Id, nameof(Id), "O Id  é inválido.")
             .IsNotNullOrEmpty(ProductStatus, nameof(ProductStatus), "O Status do produto  é inválido.")
             .IsNotNullOrEmpty(Category, nameof(Category), "A categoria  é inválida.")
             .IsGreaterOrEqualsThan(Quantity, 0, nameof(Quantity), "A Quantidade é inválida.")
             .IsNotNullOrEmpty(MeasurementUnit, nameof(MeasurementUnit), "A unidade de medida é inválida.")
             .AreNotEquals(PurchaseDate, DateTime.MinValue, nameof(PurchaseDate), "A data de compra é inválida.")
             .AreNotEquals(ExpirationDate, DateTime.MinValue, nameof(ExpirationDate), "A data de validade é inválida.")
             .IsNotNullOrEmpty(Name, nameof(Name), "O nome é obrigatório.")
             .IsGreaterThan(Name, 4, nameof(Name), "O nome deve conter no mínimo 4 caracteres."));
        }
    }
}
