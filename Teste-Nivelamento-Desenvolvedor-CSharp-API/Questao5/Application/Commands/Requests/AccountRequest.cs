using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class AccountRequest : IRequest<AccountResponse>
    {
        public string? AccountId { get; set; }
    }
}
