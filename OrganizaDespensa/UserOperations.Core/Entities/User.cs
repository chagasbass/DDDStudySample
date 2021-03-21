using UserOperations.Core.ValueObjects;
using OrganizaDespensa.SharedKernel.Core.Entities;

namespace UserOperations.Core.Entities
{
    public class User:Entity
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public string UserCode { get; private set; }
        public int ShoppingDay { get; private set; }

        protected User()
        {

        }

        public User(string name,Email email, string userCode, int shoppingDay)
        {
            Name = name;
            Email = email;
            UserCode = userCode;
            ShoppingDay = shoppingDay;
        }

        public void ChangeShoppingDay(int diaDeCompras)
        {

        }

    }
}
