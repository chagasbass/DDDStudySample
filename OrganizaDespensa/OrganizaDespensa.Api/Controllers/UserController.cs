using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperacoesDeUsuario.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.Authetications;
using OrganizaDespensa.SharedKernel.Core.Commands;
using OrganizaDespensa.SharedKernel.Core.QueueHandlers;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OrganizaDespensa.Api.Controllers
{
    [ApiController]
    [Route("v1/usuarios")]
    public class UserController : ControllerBase
    {
        private readonly IQueueHandler _queueHandler;
        private readonly ITokenService _tokenService;

        public UserController(IQueueHandler queueHandler, ITokenService tokenService)
        {
            _queueHandler = queueHandler;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICommandResult>> CreateUserAsync([FromBody] CreateUserCommand cadastrarUsuarioComando)
        {
            var userCode = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            var tokenEmail = User.Claims.FirstOrDefault(x => x.Type == "firebase")?.Value;
            var username = User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;

            var userEmail = _tokenService.RetrieveUserEmail(tokenEmail);

            cadastrarUsuarioComando.UserCode = userCode;
            cadastrarUsuarioComando.Email = userEmail;
            cadastrarUsuarioComando.Name = username;

            var retorno = (CommandResult)await _queueHandler.SendCommand(cadastrarUsuarioComando);

            if (!retorno.Sucesso)
                return BadRequest(retorno);

            return Created("v1/usuarios", retorno);
        }

        [HttpPut]
        [Route("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICommandResult>> UpdateUserAsync([FromBody] UpdateUserCommad updateUserCommad)
        {
            var userCode = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            updateUserCommad.UserCode = userCode;

            var retorno = (CommandResult)await _queueHandler.SendCommand(updateUserCommad);

            if (!retorno.Sucesso)
                return BadRequest(retorno);

            return NoContent();
        }
    }
}
