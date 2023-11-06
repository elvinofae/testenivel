using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> SearchAll();
        Task<Account> SearchAccountId(string id);
        Task<bool> Insert(Account entity);
        Task<bool> Update(Account entity);
        Task<bool> Delete(string id);
    }
}
