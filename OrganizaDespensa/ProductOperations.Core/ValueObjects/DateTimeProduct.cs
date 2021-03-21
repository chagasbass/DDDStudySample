using OrganizaDespensa.SharedKernel.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProductOperations.Core.ValueObjects
{
    public class DateTimeProduct:ValueObject
    {
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public DateTimeProduct(DateTime purchaseDate, DateTime expirationDate)
        {
            PurchaseDate = purchaseDate;
            ExpirationDate = expirationDate;
        }

        public void ChangePurchaseDate(DateTime purchaseDate) => new DateTimeProduct(purchaseDate, ExpirationDate);
        public void ChangeExpirationDate(DateTime expirationDate) => new DateTimeProduct(PurchaseDate, expirationDate);


        public bool VerifyExpirationDate() => ExpirationDate > DateTime.Now;


        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}
