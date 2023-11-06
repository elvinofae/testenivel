using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class IdempotenceRequest : IRequest<bool>
    {
        public string? KeyIdempotence { get; set; }
        public string? Requisition { get; set; }
        public string? Result { get; set; }
    }
}
