using Boss.Core.Models;
using Boss.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;

namespace Boss.Infrastructure.Repositories
{
    public class ProcedimentoRepository
    {
        private readonly string _connectionString = DatabaseContext.ConnectionString;

        public void Adicionar(Procedimento p)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = @"INSERT INTO Procedimentos (Marca, Modelo, NumeroModelo, TipoBloqueio, ProcedimentoTexto, Observacoes, Autor, DataCadastro) 
                           VALUES (@Marca, @Modelo, @NumeroModelo, @TipoBloqueio, @ProcedimentoTexto, @Observacoes, @Autor, @DataCadastro);";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Marca", p.Marca);
            command.Parameters.AddWithValue("@Modelo", p.Modelo);
            command.Parameters.AddWithValue("@NumeroModelo", p.NumeroModelo);
            command.Parameters.AddWithValue("@TipoBloqueio", p.TipoBloqueio);
            command.Parameters.AddWithValue("@ProcedimentoTexto", p.ProcedimentoTexto);
            command.Parameters.AddWithValue("@Observacoes", p.Observacoes);
            command.Parameters.AddWithValue("@Autor", p.Autor);
            command.Parameters.AddWithValue("@DataCadastro", p.DataCadastro);

            command.ExecuteNonQuery();
        }

        public void Atualizar(Procedimento p)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = @"UPDATE Procedimentos 
                           SET Marca = @Marca, 
                               Modelo = @Modelo, 
                               NumeroModelo = @NumeroModelo, 
                               TipoBloqueio = @TipoBloqueio, 
                               ProcedimentoTexto = @ProcedimentoTexto, 
                               Observacoes = @Observacoes, 
                               UltimaEdicao = @UltimaEdicao 
                           WHERE Id = @Id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", p.Id);
            command.Parameters.AddWithValue("@Marca", p.Marca);
            command.Parameters.AddWithValue("@Modelo", p.Modelo);
            command.Parameters.AddWithValue("@NumeroModelo", p.NumeroModelo);
            command.Parameters.AddWithValue("@TipoBloqueio", p.TipoBloqueio);
            command.Parameters.AddWithValue("@ProcedimentoTexto", p.ProcedimentoTexto);
            command.Parameters.AddWithValue("@Observacoes", p.Observacoes);
            command.Parameters.AddWithValue("@UltimaEdicao", p.UltimaEdicao ?? DateTime.Now);

            command.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = "DELETE FROM Procedimentos WHERE Id = @Id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public List<Procedimento> ListarTodos()
        {
            var lista = new List<Procedimento>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = "SELECT Id, Marca, Modelo, NumeroModelo, TipoBloqueio, ProcedimentoTexto, Observacoes, Autor, DataCadastro, UltimaEdicao FROM Procedimentos ORDER BY DataCadastro DESC";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Procedimento
                {
                    Id = reader.GetInt32(0),
                    Marca = reader.GetString(1),
                    Modelo = reader.GetString(2),
                    NumeroModelo = reader.GetString(3),
                    TipoBloqueio = reader.GetString(4),
                    ProcedimentoTexto = reader.GetString(5),
                    Observacoes = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    Autor = reader.GetString(7),
                    DataCadastro = reader.GetDateTime(8),
                    UltimaEdicao = reader.IsDBNull(9) ? null : reader.GetDateTime(9)
                });
            }

            return lista;
        }
    }
}