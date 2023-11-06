using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class AccountHandler : IRequestHandler<AccountRequest, AccountResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMovementRepository _movementRepository;

        public AccountHandler(IAccountRepository accountRepository, IMovementRepository movementRepository)
        {
            _accountRepository = accountRepository;
            _movementRepository = movementRepository;
        }

        public async Task<AccountResponse> Handle(AccountRequest request, CancellationToken cancellationToken)
        {

            var movement = await _movementRepository.SearchAccountId(request.AccountId??"");
            var account = await _accountRepository.SearchAccountId(request.AccountId??"");

            if (account == null || account.IdContaCorrente is null)
                return AccountResponse.Fail("Conta corrente não cadastrada.", FailTypeEnum.INVALID_ACCOUNT);

            if (account.Ativo == 0)
                return AccountResponse.Fail("Conta corrente não está ativa.", FailTypeEnum.INACTIVE_ACCOUNT);

            decimal credit = 0.0M;
            decimal debit = 0.0M;

            foreach (var item in movement)
            {
                if (item is null || item.TipoMovimento is null) continue;

                switch (item.TipoMovimento)
                {
                    case "C":
                        credit += item.Valor;
                        break;
                    case "D":
                        debit += item.Valor;
                        break;
                }
            }

            var balance = credit - debit;

            return AccountResponse.Success(account.IdContaCorrente, account.Nome??"", DateTime.Now.ToShortDateString(), balance);
        }
    }
}
