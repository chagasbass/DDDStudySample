using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;

namespace OperacoesDeUsuario.Core.Commands
{
    public class UpdateUserCommad: Notifiable<Notification>, ICommand, IRequest<ICommandResult>
    {
        public string UserCode { get; set; }
        public int ShoppingDay { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(UserCode, nameof(UserCode), "O código do usuário é obrigatório")
                .IsLowerOrEqualsThan(ShoppingDay, 0, nameof(ShoppingDay), "O dia de compras é inválido."));
        }
    }
}
