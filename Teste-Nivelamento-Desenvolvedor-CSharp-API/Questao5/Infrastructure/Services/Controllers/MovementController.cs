using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovementController : ControllerBase
    {
        private readonly IMediator _mediat;
        private readonly IMemoryCache _cache;

        public MovementController(IMediator mediat, IMemoryCache cache)
        {
            _mediat = mediat;
            _cache = cache;
        }

        [HttpPost]
        public async Task<ActionResult<MovementResponse>> MovimentarConta([FromBody] MovementRequest request)
        {
            try
            {
                string ? data = _cache.Get<string>(request.RequisitionId);

                if (data is not null && data.Any())
                {
                    request = JsonConvert.DeserializeObject<MovementRequest>(data)??new MovementRequest();
                }

                var result = await _mediat.Send(request);

                if (!result.IsSuccess)
                {
                    return BadRequest(new { message = result.ErrorMessage, type = result.ErrorType });
                }

                return Ok(new { movimentoId = result.MovementId });
            }
            catch
            {
                var movement = new
                {
                    IdConta = request?.AccountId,
                    DataMovimento = DateTime.Now.ToShortDateString(),
                    TipoMovimento = request?.MovementType == 0 ? "C" : "D",
                    Valor = request?.Value
                };

                string movementString = JsonConvert.SerializeObject(movement);
                _cache.Set(request?.RequisitionId, movement, TimeSpan.FromMinutes(30));

                await _mediat.Send(new IdempotenceRequest
                {
                    KeyIdempotence = Guid.NewGuid().ToString(),
                    Requisition = request?.RequisitionId,
                    Result = movementString
                });

                return BadRequest("Falha no sistema.");
            }
        }
    }
}
