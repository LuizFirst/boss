using Microsoft.Data.Sqlite;
using System.IO;

namespace Boss.Infrastructure.Data
{
    public static class DatabaseContext
    {
        private static readonly string DbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Boss", "Boss.db");

        public static string ConnectionString => $"Data Source={DbPath}";

        public static void Initialize()
        {
            string directory = Path.GetDirectoryName(DbPath)!;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            // Tabela Usuarios
            string createUsuarios = @"
                CREATE TABLE IF NOT EXISTS Usuarios (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Login TEXT NOT NULL UNIQUE,
                    SenhaHash TEXT NOT NULL,
                    Nome TEXT NOT NULL,
                    NivelAcesso TEXT NOT NULL,
                    Ativo INTEGER NOT NULL DEFAULT 1
                );";

            // Tabela Procedimentos
            string createProcedimentos = @"
                CREATE TABLE IF NOT EXISTS Procedimentos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Marca TEXT NOT NULL,
                    Modelo TEXT NOT NULL,
                    NumeroModelo TEXT,
                    TipoBloqueio TEXT NOT NULL,
                    ProcedimentoTexto TEXT NOT NULL,
                    Observacoes TEXT,
                    Autor TEXT NOT NULL,
                    DataCadastro DATETIME NOT NULL,
                    UltimaEdicao DATETIME
                );";

            // Tabela Licencas
            string createLicencas = @"
                CREATE TABLE IF NOT EXISTS Licencas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Chave TEXT NOT NULL UNIQUE,
                    DataAtivacao DATETIME NOT NULL,
                    DataExpiracao DATETIME,
                    Ativa INTEGER NOT NULL DEFAULT 1,
                    Tipo TEXT NOT NULL
                );";

            using var cmd1 = new SqliteCommand(createUsuarios, connection);
            cmd1.ExecuteNonQuery();

            using var cmd2 = new SqliteCommand(createProcedimentos, connection);
            cmd2.ExecuteNonQuery();

            using var cmd3 = new SqliteCommand(createLicencas, connection);
            cmd3.ExecuteNonQuery();
        }
    }
}