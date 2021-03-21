using OrganizaDespensa.SharedKernel.Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ProductOperations.Core.Enums
{
    public class ProductStatus:Enumeration
    {
        public static ProductStatus IDEAL { get; } = new ProductStatus(1, nameof(IDEAL));
        public static ProductStatus BUY { get; }  = new ProductStatus(2, nameof(BUY));

        private static List<ProductStatus> Status { get; } = new List<ProductStatus>()
        {
            IDEAL,BUY
        };

        public ProductStatus(int id, string name) : base(id, name) { }

        public static ProductStatus GetProductStatusByName(string name) => Status.FirstOrDefault(x => x.Name.Equals(name));
    }
}
