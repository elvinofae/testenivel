using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class AccountResponse : BaseResponse
    {
        public string? Number { get; private set; }
        public string? Name { get; private set; }
        public string? Date { get; private set; }
        public decimal Balance { get; private set; }

        private AccountResponse(bool isSuccess, string number, string name, string date, decimal balance)
            : base(isSuccess)
        {
            Number = number;
            Name = name;
            Date = date;
            Balance = balance;
        }

        private AccountResponse(bool isSuccess, string errorMessage, FailTypeEnum errorType)
            : base(isSuccess, errorMessage, errorType)
        {
        }

        public static AccountResponse Success(string number, string name, string date, decimal balance)
        {
            return new AccountResponse(true, number, name, date, balance);
        }

        public new static AccountResponse Fail(string errorMessage, FailTypeEnum errorType)
        {
            return new AccountResponse(false, errorMessage, errorType);
        }
    }
}
