using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseConfig _conn;

        public AccountRepository(DatabaseConfig conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Account>> SearchAll()
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                return await connection.QueryAsync<Account>("SELECT * FROM contacorrente");
            }
        }

        public async Task<Account> SearchAccountId(string id)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Account>("SELECT * FROM contacorrente WHERE idcontacorrente = @Id", new { Id = id });
            }
        }

        public async Task<bool> Insert(Account entity)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                var rows = await connection.ExecuteAsync($"INSERT INTO contacorrente VALUES (@IdContaCorrente, @Numero, @Nome, @Ativo)", entity);
                return rows > 0;
            }
        }

        public async Task<bool> Update(Account entity)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                var rows = await connection.ExecuteAsync("UPDATE contacorrente SET numero = @Numero, nome = @Nome, ativo = @Ativo WHERE idcontacorrente = @IdContaCorrente", entity);
                return rows > 0;
            }
        }

        public async Task<bool> Delete(string id)
        {
            using (var connection = new SqliteConnection(_conn.Name))
            {
                connection.Open();
                var rows = await connection.ExecuteAsync("DELETE FROM contacorrente WHERE idcontacorrente = @Id", new { Id = id });
                return rows > 0;
            }
        }
    }
}
