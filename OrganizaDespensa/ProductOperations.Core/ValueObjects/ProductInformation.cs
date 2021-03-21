using OrganizaDespensa.SharedKernel.Core.ValueObjects;
using System.Collections.Generic;

namespace ProductOperations.Core.ValueObjects
{
    public class ProductInformation : ValueObject
    {
        public int Quantity { get; set; }
        public string MeasurementUnit { get; set; }

        public ProductInformation(int quantity, string measurementUnit)
        {
            Quantity = quantity;
            MeasurementUnit = measurementUnit;
        }

        public void ChangeQuantity(int quantity) => new ProductInformation(quantity, MeasurementUnit);
        public void ChangeMeasurementUnit(string measurementUnit) => new ProductInformation(Quantity, measurementUnit);

        public bool VerifyQuantity() => Quantity > 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}
