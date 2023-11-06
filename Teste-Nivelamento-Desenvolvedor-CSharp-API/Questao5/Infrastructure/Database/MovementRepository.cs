using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;
using static Dapper.SqlMapper;

namespace Questao5.Infrastructure.Database
{
    public class MovementRepository : IMovementRepository
    {
        private readonly DatabaseConfig _conn;

        public MovementRepository(DatabaseConfig conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Movement>> SearchAll()
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                return await connection.QueryAsync<Movement>("SELECT * FROM movimento");
            }
        }

        public async Task<IEnumerable<Movement>> SearchAccountId(string id)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                return await connection.QueryAsync<Movement>("SELECT * FROM movimento WHERE idcontacorrente = @Id", new { Id = id });
            }
        }

        public async Task<bool> Insert(Movement entity)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                var rows = await connection.ExecuteAsync($"INSERT INTO movimento VALUES (@IdMovimento, @IdConta, @DataMovimento, @TipoMovimento, @Valor)", entity);
                return rows > 0;
            }
        }

        public async Task<bool> Update(Movement entity)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                var rows = await connection.ExecuteAsync("UPDATE movimento SET idcontacorrente = @IdConta, datamovimento = @DataMovimento, tipomovimento = @TipoMovimento, valor = @Valor WHERE idmovimento = @IdMovimento", entity);
                return rows > 0;
            }
        }

        public async Task<bool> Delete(string id)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                var rows = await connection.ExecuteAsync("DELETE FROM movimento WHERE idmovimento = @Id", new { Id = id });
                return rows > 0;
            }
        }
    }
}
