using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;
using static Dapper.SqlMapper;

namespace Questao5.Infrastructure.Database
{
    public class IdempotenceRepository : IIdempotenceRepository
    {
        private readonly DatabaseConfig _conn;

        public IdempotenceRepository(DatabaseConfig conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Idempotence>> SearchAll()
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                return await connection.QueryAsync<Idempotence>("SELECT * FROM idempotencia");
            }
        }

        public async Task<IEnumerable<Idempotence>> SearchId(string id)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                return await connection.QueryAsync<Idempotence>("SELECT * FROM idempotencia WHERE requisicao = @Id", new { Id = id });
            }
        }

        public async Task<bool> Insert(Idempotence entity)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                var rows = await connection.ExecuteAsync($"INSERT INTO idempotencia VALUES (@Chave_Idempotencia, @Requisicao, @Resultado)", entity);
                return rows > 0;
            }
        }
    }
}
