using CategoryOperations.Core.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizaDespensa.SharedKernel.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.QueueHandlers;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OrganizaDespensa.Api.Controllers.Categories
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IQueueHandler _queueHandler;

        public CategoryController(IQueueHandler queueHandler)
        {
            _queueHandler = queueHandler;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<ICommandResult>> SalvarCategoria([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            createCategoryCommand.User = user;

            var retorno = (CommandResult)await _queueHandler.SendCommand(createCategoryCommand);

            if (!retorno.Sucesso)
                return BadRequest(retorno);

            return Created("v1/categories", retorno);
        }
    }
}
