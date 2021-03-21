using CategoryOperations.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizaDespensa.SharedKernel.Core.Commands;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OrganizaDespensa.Api.Controllers.Categories
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoriesListController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesListController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("{userCode}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<ICommandResult>> GetCategoriesAsync([FromRoute] string userCode)
        {
            var commandResult = (CommandResult)await _categoryRepository.GetCategoriesAsync(userCode);

            if (!commandResult.Sucesso)
                return NotFound();

            return Ok(commandResult);
        }
    }
}
