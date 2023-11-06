using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IIdempotenceRepository
    {
        Task<IEnumerable<Idempotence>> SearchAll();
        Task<IEnumerable<Idempotence>> SearchId(string id);
        Task<bool> Insert(Idempotence entity);
    }
}
