using Boss.Core.Models;
using Boss.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using System;

namespace Boss.Infrastructure.Repositories
{
    public class LicencaRepository
    {
        private readonly string _connectionString = DatabaseContext.ConnectionString;

        public void Adicionar(Licenca licenca)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = @"INSERT INTO Licencas (Chave, DataAtivacao, DataExpiracao, Ativa, Tipo) 
                           VALUES (@Chave, @DataAtivacao, @DataExpiracao, @Ativa, @Tipo);";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Chave", licenca.Chave);
            command.Parameters.AddWithValue("@DataAtivacao", licenca.DataAtivacao);
            command.Parameters.AddWithValue("@DataExpiracao", licenca.DataExpiracao ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Ativa", licenca.Ativa ? 1 : 0);
            command.Parameters.AddWithValue("@Tipo", licenca.Tipo);

            command.ExecuteNonQuery();
        }

        public Licenca? ObterLicencaAtiva()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM Licencas WHERE Ativa = 1 LIMIT 1";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Licenca
                {
                    Id = reader.GetInt32(0),
                    Chave = reader.GetString(1),
                    DataAtivacao = reader.GetDateTime(2),
                    DataExpiracao = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                    Ativa = reader.GetInt32(4) == 1,
                    Tipo = reader.GetString(5)
                };
            }

            return null;
        }
    }
}