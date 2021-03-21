using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;

namespace CategoryOperations.Core.Commands
{
    public class CreateCategoryCommand : Notifiable<Notification>, ICommand, IRequest<ICommandResult>
    {
        public string Name { get; set; }
        public string User { get; set; }

        public CreateCategoryCommand() { }

        public CreateCategoryCommand(string name, string user)
        {
            Name = name.ToUpper().Trim();
            User = user;
        }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
             .Requires()
             .IsNotNullOrEmpty(Name, nameof(Name), "O nome  é inválido.")
             .IsNotNullOrEmpty(User, nameof(User), "O usuário  é inválido.")
             .IsGreaterThan(Name, 140, nameof(Name), "O nome da categoria deve conter no máximo 140 caracteres"));
        }
    }
}
