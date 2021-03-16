using System.Threading.Tasks;

namespace OrganizaDespensa.SharedKernel.Core.DomainEvents
{
    public interface IDomainEntityEventHandler<T>
    {
        Task Raise(T domainEvent);
    }
}
