using Boss.Core.Models;
using Boss.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

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

        public List<Licenca> ObterTodas()
        {
            var licencas = new List<Licenca>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM Licencas ORDER BY DataAtivacao DESC";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                licencas.Add(new Licenca
                {
                    Id = reader.GetInt32(0),
                    Chave = reader.GetString(1),
                    DataAtivacao = reader.GetDateTime(2),
                    DataExpiracao = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                    Ativa = reader.GetInt32(4) == 1,
                    Tipo = reader.GetString(5)
                });
            }

            return licencas;
        }

        public void Desativar(int licencaId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = "UPDATE Licencas SET Ativa = 0 WHERE Id = @Id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", licencaId);
            command.ExecuteNonQuery();
        }

        public void Deletar(int licencaId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = "DELETE FROM Licencas WHERE Id = @Id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", licencaId);
            command.ExecuteNonQuery();
        }

        public void AdicionarTempo(int licencaId, int meses)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = @"UPDATE Licencas 
                           SET DataExpiracao = DATE(DataExpiracao, '+' || @Meses || ' months') 
                           WHERE Id = @Id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", licencaId);
            command.Parameters.AddWithValue("@Meses", meses);
            command.ExecuteNonQuery();
        }
    }
}
