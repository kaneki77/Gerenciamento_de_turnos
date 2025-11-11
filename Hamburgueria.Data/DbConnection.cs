
using System.Data;
using MySql.Data.MySqlClient;

namespace Hamburgueria.Data
{
    // Classe para gerenciar a conexão com o banco de dados MySQL
    public class DbConnection
    {
        // **ATENÇÃO:** Em um projeto real, a string de conexão NUNCA deve ser hardcoded.
        // Deve ser lida de um arquivo de configuração (ex: App.config ou appsettings.json).
        private const string ConnectionString = "Server=localhost;Database=hamburgueria_db;Uid=root;Pwd=sua_senha_aqui;";

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(ConnectionString);
            return connection;
        }
    }
}

