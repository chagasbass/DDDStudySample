using OrganizaDespensa.SharedKernel.Core.Commands;
using System.Threading.Tasks;

namespace OrganizaDespensa.SharedKernel.Core.Fluxos
{
    public interface IHandler
    {
        Task<ICommandResult> SendComand<T>(T command) where T : ICommand;
    }
}
