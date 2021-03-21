using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;

namespace ProductOperations.Core.Commands
{
    public class DeleteProductCommand : Notifiable<Notification>, ICommand, IRequest<ICommandResult>
    {
        public string Id { get; set; }
        public string User { get; set; }

        public DeleteProductCommand(string id, string user)
        {
            Id = id;
            User = user;
        }


        public void Validate()
        {

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Id, nameof(Id), "O produto é inválido")
                .IsNotNullOrEmpty(User, nameof(User), "O usuário é inválido"));
        }
    }
}
