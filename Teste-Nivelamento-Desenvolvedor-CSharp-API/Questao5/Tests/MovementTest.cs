using Moq;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Xunit;

namespace Questao5.Tests
{
    public class MovementTest
    {
        [Fact]
        public async Task HandleMovementCheckGenerateIdAndSuccess()
        {
            var repositoryAccountMock = new Mock<IAccountRepository>();
            repositoryAccountMock.Setup(repo => repo.SearchAccountId("B6BAFC09-6967-ED11-A567-055DFA4A16C9")).Returns(Task.FromResult(new Account()
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Numero = 123,
                Nome = "Katherine Sanchez",
                Ativo = 1
            }));

            var movementCredit = new Movement()
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdConta = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                DataMovimento = DateTime.Now.ToShortDateString(),
                TipoMovimento = "C",
                Valor = 15000
            };

            var movementDebit = new Movement()
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdConta = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                DataMovimento = DateTime.Now.ToShortDateString(),
                TipoMovimento = "D",
                Valor = 200
            };

            var repositoryMovementMock = new Mock<IMovementRepository>();
            repositoryMovementMock.Setup(repo => repo.Insert(It.IsAny<Movement>())).ReturnsAsync(true);

            var command = new MovementRequest { RequisitionId = "123", AccountId = "B6BAFC09-6967-ED11-A567-055DFA4A16C9", MovementType = Domain.Enumerators.MovementTypeEnum.CREDIT, Value = 450 };
            var cancelattionToken = new CancellationToken();

            var handler = new MovementHandler(repositoryMovementMock.Object, repositoryAccountMock.Object);
            var result = await handler.Handle(command, cancelattionToken);

            repositoryAccountMock.Verify(repo => repo.SearchAccountId("B6BAFC09-6967-ED11-A567-055DFA4A16C9"), Times.Once);
            repositoryMovementMock.Verify(repo => repo.Insert(It.Is<Movement>(acc => acc.IdConta == "B6BAFC09-6967-ED11-A567-055DFA4A16C9")), Times.Once);

            Assert.True(result.MovementId is not null);
            Assert.True(result.IsSuccess);
        }
    }
}
