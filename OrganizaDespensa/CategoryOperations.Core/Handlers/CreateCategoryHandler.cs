using CategoryOperations.Core.Commands;
using CategoryOperations.Core.Entities;
using CategoryOperations.Core.Repositories;
using Flunt.Notifications;
using MediatR;
using OrganizaDespensa.SharedKernel.Core.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace CategoryOperations.Core.Handlers
{
    public class CreateCategoryHandler : Notifiable<Notification>, IRequestHandler<CreateCategoryCommand, ICommandResult>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ICommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (!request.IsValid)
                return new CommandResult(false, "Problemas ao cadastrar a categoria.", request.Notifications);

            var categoriaExiste = await _categoryRepository.VerifyCategoryAsync(request.Name, request.User);

            if (categoriaExiste)
                return new CommandResult(false, "A categoria ja esta cadastrada.", null);

            var newCategory = new Category(request.Name, request.User);

            var createdCategory = await _categoryRepository.CreateCategoryAsync(newCategory);

            if (createdCategory == null)
                return new CommandResult(false, "Problemas ao cadastrar a categoria.", null);

            return new CommandResult(true, "Categoria Cadastrada com sucesso", createdCategory);
        }
    }
}
