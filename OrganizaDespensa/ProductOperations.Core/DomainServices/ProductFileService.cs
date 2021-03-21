using System;

namespace ProductOperations.Core.DomainServices
{
    public class ProductFileService : IProductFileService
    {
        public byte[] ProcessProductImage(string imageBase64)
        {
            if ((!string.IsNullOrEmpty(imageBase64) && imageBase64.Contains(",")))
            {
                imageBase64 = imageBase64.Substring(imageBase64.IndexOf(",") + 1);

                return Convert.FromBase64String(imageBase64);
            }

            return default;
        }
    }
}
