using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public abstract class BaseResponse
    {
        public bool IsSuccess { get; protected set; }
        public string? ErrorMessage { get; protected set; }
        public FailTypeEnum ErrorType { get; protected set; }

        protected BaseResponse(bool isSuccess, string errorMessage, FailTypeEnum errorType)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            ErrorType = errorType;
        }

        protected BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static T Fail<T>(string errorMessage, FailTypeEnum errorType) where T : BaseResponse, new()
        {
            return new T { IsSuccess = false, ErrorMessage = errorMessage, ErrorType = errorType };
        }
    }
}
