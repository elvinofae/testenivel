using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IMovementRepository
    {
        Task<IEnumerable<Movement>> SearchAll();
        Task<IEnumerable<Movement>> SearchAccountId(string id);
        Task<bool> Insert(Movement entity);
        Task<bool> Update(Movement entity);
        Task<bool> Delete(string id);
    }
}
