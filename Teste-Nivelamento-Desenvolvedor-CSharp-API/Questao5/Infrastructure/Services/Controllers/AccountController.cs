using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediat;

        public AccountController(IMediator mediat)
        {
            _mediat = mediat;
        }

        [HttpPost]
        public async Task<ActionResult<AccountResponse>> MovimentarConta([FromBody] AccountRequest request)
        {
            var result = await _mediat.Send(request);
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.ErrorMessage, type = result.ErrorType });
            }

            return Ok(new { number = result.Number, name = result.Name, date = result.Date, balance = result.Balance });
        }
    }
}
