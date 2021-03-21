using OrganizaDespensa.SharedKernel.Core.Entities;
using ProductOperations.Core.Enums;
using ProductOperations.Core.ValueObjects;

namespace ProductOperations.Core.Entities
{
    public  class Product:Entity
    {
        public ProductData ProductData { get; private set; }
        public DateTimeProduct DateTimeProduct { get; private set; }
        public ProductInformation ProductInformation { get; private set; }
        public string Category { get; private set; }
        public string Status { get; private set; }
        public string UserId { get; private set; }

        protected Product() { }

        public Product(ProductData productData, DateTimeProduct dateTimeProduct, ProductInformation productInformation,
           string category, string userId)
        {
            ProductData = productData;
            DateTimeProduct = dateTimeProduct;
            ProductInformation = productInformation;
            Category = category;
            Status = ProductStatus.IDEAL.Name;
            UserId = userId;
        }

        public void AlterarStatus(string status) => Status = status;

        public void ChangeCategory(string category) => Category = category;

        public void ChangeDateTimeProduct(DateTimeProduct dateTimeProduct) => DateTimeProduct = dateTimeProduct;

        public void ChangeProductData(ProductData productData) => ProductData = productData;

        public void ChangeProductInformation(ProductInformation productInformation)
        {
            ProductInformation = productInformation;

            if (!productInformation.VerifyQuantity())
                Status = ProductStatus.BUY.Name;
        }

        public bool VerifyQuantity() => ProductInformation.VerifyQuantity();

        public void ChangeStatus(string status)
        {
            var productStatus = ProductStatus.GetProductStatusByName(status);
            Status = productStatus.Name;
        }
    }
}
