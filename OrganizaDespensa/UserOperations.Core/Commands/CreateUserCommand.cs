using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;

namespace UserOperations.Core.Commands
{
    public class CreateUserCommand: Notifiable<Notification>, ICommand, IRequest<ICommandResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserCode { get; set; }
        public int ShoppingDay { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, nameof(Name), "O nome é obrigatório")
                .IsEmailOrEmpty(Email, nameof(Email), "O email é obrigatório")
                .IsNotNullOrEmpty(UserCode, nameof(UserCode), "O código do usuário é obrigatório")
                .IsGreaterOrEqualsThan(ShoppingDay, 0, nameof(ShoppingDay), "O dia de compras é inválido."));
        }
    }
}
