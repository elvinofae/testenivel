using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovementRequest : IRequest<MovementResponse>
    {
        public string? RequisitionId { get; set; }
        public string? AccountId { get; set; }
        public decimal Value { get; set; }
        public MovementTypeEnum MovementType { get; set; }
    }
}
