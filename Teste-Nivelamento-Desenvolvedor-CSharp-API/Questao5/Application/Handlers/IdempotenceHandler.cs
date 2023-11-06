using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class IdempotenceHandler : IRequestHandler<IdempotenceRequest, bool>
    {
        private readonly IIdempotenceRepository _idempotenceRepository;

        public IdempotenceHandler(IIdempotenceRepository idempotenceRepository)
        {
            _idempotenceRepository = idempotenceRepository;
        }

        public async Task<bool> Handle(IdempotenceRequest request, CancellationToken cancellationToken)
        {
            return await _idempotenceRepository.Insert(new Idempotence {
                Chave_Idempotencia = request.KeyIdempotence,
                Requisicao = request.Requisition,
                Resultado = request.Result
            });
        }
    }
}
