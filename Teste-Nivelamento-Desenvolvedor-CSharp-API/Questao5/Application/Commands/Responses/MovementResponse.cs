using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class MovementResponse : BaseResponse
    {
        public string? MovementId { get; private set; }

        private MovementResponse(bool isSuccess, string movementId) : base(isSuccess)
        {
            MovementId = movementId;
        }

        private MovementResponse(bool isSuccess, string errorMessage, FailTypeEnum errorType)
            : base(isSuccess, errorMessage, errorType)
        {
        }

        public static MovementResponse Success(string movementId)
        {
            return new MovementResponse(true, movementId);
        }

        public new static MovementResponse Fail(string errorMessage, FailTypeEnum errorType)
        {
            return new MovementResponse(false, errorMessage, errorType);
        }
    }
}
