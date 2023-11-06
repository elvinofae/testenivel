using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class MovementHandler : IRequestHandler<MovementRequest, MovementResponse>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IAccountRepository _accountRepository;

        public MovementHandler(IMovementRepository movementRepository, IAccountRepository accountRepository)
        {
            _movementRepository = movementRepository;
            _accountRepository = accountRepository;
        }

        public async Task<MovementResponse> Handle(MovementRequest request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.SearchAccountId(request.AccountId??"");

            if (account == null)            
                return MovementResponse.Fail("Conta corrente não cadastrada.", FailTypeEnum.INVALID_ACCOUNT);
            
            if (account.Ativo == 0)            
                return MovementResponse.Fail("Conta corrente não está ativa.", FailTypeEnum.INACTIVE_ACCOUNT);
            
            if (request.Value <= 0)            
                return MovementResponse.Fail("O valor deve ser positivo.", FailTypeEnum.INVALID_VALUE);

            if (request.MovementType != MovementTypeEnum.CREDIT && 
                request.MovementType != MovementTypeEnum.DEBIT)            
                return MovementResponse.Fail("Tipo de movimento inválido.", FailTypeEnum.INVALID_TYPE);
            
            var movement = new Movement
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdConta = request.AccountId,
                DataMovimento = DateTime.Now.ToShortDateString(),
                TipoMovimento = request.MovementType == 0 ? "C" : "D",
                Valor = request.Value
            };

            var result = await _movementRepository.Insert(movement);

            if(!result)
                return MovementResponse.Fail("Tipo de movimento inválido.", FailTypeEnum.INVALID_TYPE);

            return MovementResponse.Success(movement.IdMovimento);
        }
    }
}
