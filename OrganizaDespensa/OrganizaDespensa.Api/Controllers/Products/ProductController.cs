using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizaDespensa.SharedKernel.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.QueueHandlers;
using ProductOperations.Core.Commands;
using System.Threading.Tasks;

namespace OrganizaDespensa.Api.Controllers.Products
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IQueueHandler _queueHandler;

        public ProductController(IQueueHandler queueHandler)
        {
            _queueHandler = queueHandler;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<ICommandResult>> CreateProductAsync([FromBody] CreateProductCommand createProductCommand)
        {
            var commandResult = (CommandResult)await _queueHandler.SendCommand(createProductCommand);

            if (!commandResult.Sucesso)
                return BadRequest(commandResult);

            return Created("v1/products", commandResult);
        }

        [HttpPut]
        [Route("")]
        [Authorize]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<ICommandResult>> UpdateProductAsync([FromBody] UpdateProductCommand updateProductCommand)
        {
            var commandResult = (CommandResult)await _queueHandler.SendCommand(updateProductCommand);

            if (!commandResult.Sucesso)
                return BadRequest(commandResult);

            return NoContent();
        }

        [HttpDelete]
        [Route("")]
        [Authorize]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<ICommandResult>> DeleteProductAsync([FromBody] DeleteProductCommand deleteProductCommand)
        {
            var commandResult = (CommandResult)await _queueHandler.SendCommand(deleteProductCommand);

            if (!commandResult.Sucesso)
                return BadRequest(commandResult);

            return Ok();
        }
    }
}
