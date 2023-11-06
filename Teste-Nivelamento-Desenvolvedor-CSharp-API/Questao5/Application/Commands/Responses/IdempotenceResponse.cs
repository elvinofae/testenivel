
namespace Questao5.Application.Commands.Responses
{
    public class IdempotenceResponse
    {
        public string? KeyIdempotence { get; set; }
        public string? Requisition { get; set; }
        public string? Result { get; set; }
    }
}
