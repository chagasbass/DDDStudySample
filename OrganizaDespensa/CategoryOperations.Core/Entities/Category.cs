using OrganizaDespensa.SharedKernel.Core.Entities;

namespace CategoryOperations.Core.Entities
{
    public class Category:Entity
    {
        public string Name { get; private set; }
        public string User { get; private set; }

        protected Category() { }

        public Category(string name, string user)
        {
            Name = name;
            User = user;
        }

        public void ChangeName(string name)
            => Name = name;
    }
}
