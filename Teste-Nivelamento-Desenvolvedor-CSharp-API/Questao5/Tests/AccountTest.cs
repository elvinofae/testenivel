using Moq;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Xunit;

namespace Questao5.Tests
{
    public class AccountTest
    {
        [Fact]
        public async Task HandleAccountCheckBalanceAndSuccess()
        {
            var repositoryAccountMock = new Mock<IAccountRepository>();
            repositoryAccountMock.Setup(repo => repo.SearchAccountId("B6BAFC09-6967-ED11-A567-055DFA4A16C9")).Returns(Task.FromResult(new Account()
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Numero = 123,
                Nome = "Katherine Sanchez",
                Ativo = 1
            }));

            var movement1 = new Movement()
            {
                IdMovimento = "AA123456",
                IdConta = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                DataMovimento = "03/11/2023",
                TipoMovimento = "C",
                Valor = 300
            };

            var listMovement1 = new List<Movement>
            {
                movement1
            };
            var ienumerableMovement1 = (IEnumerable<Movement>)listMovement1;

            var repositoryMovementMock = new Mock<IMovementRepository>();
            repositoryMovementMock.Setup(repo => repo.SearchAccountId("B6BAFC09-6967-ED11-A567-055DFA4A16C9")).Returns(Task.FromResult(ienumerableMovement1));

            var command = new AccountRequest { AccountId = "B6BAFC09-6967-ED11-A567-055DFA4A16C9" };
            var cancelattionToken = new CancellationToken();

            var handler = new AccountHandler(repositoryAccountMock.Object, repositoryMovementMock.Object);
            var result = await handler.Handle(command, cancelattionToken);

            repositoryAccountMock.Verify(repo => repo.SearchAccountId("B6BAFC09-6967-ED11-A567-055DFA4A16C9"), Times.Once);
            repositoryMovementMock.Verify(repo => repo.SearchAccountId("B6BAFC09-6967-ED11-A567-055DFA4A16C9"), Times.Once);

            Assert.True(result.Balance > 0);
            Assert.True(result.IsSuccess);
        }
    }
}
