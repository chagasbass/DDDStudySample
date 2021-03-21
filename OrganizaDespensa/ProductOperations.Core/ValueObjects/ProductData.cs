using OrganizaDespensa.SharedKernel.Core.ValueObjects;
using System.Collections.Generic;

namespace ProductOperations.Core.ValueObjects
{
    public class ProductData : ValueObject
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public byte[] Image { get; private set; }

        public ProductData(string name, string descritption)
        {
            Name = name;
            Description = descritption;
        }

        public void ChangeImage(byte[] image) => Image = image;
        public void ChangeName(string name) => new ProductData(name, Description);
        public void ChangeDescription(string description) => new ProductData(Name, description);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}
