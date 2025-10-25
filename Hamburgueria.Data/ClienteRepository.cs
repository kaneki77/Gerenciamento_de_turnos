
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;

namespace Hamburgueria.Data
{
    public class ClienteRepository : RepositorioBase<Cliente>
    {
        private readonly DbConnection _dbConnection;

        public ClienteRepository()
        {
            _dbConnection = new DbConnection();
        }

        // Método que implementa o SELECT real para atender ao Critério de Aceitação D15
        public List<Cliente> GetAll()
        {
            var clientes = new List<Cliente>();
            const string query = "SELECT id_cliente, nome, telefone, email, data_cadastro FROM Cliente";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente
                            {
                                Id = reader.GetInt32("id_cliente"),
                                Nome = reader.GetString("nome"),
                                Telefone = reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                DataCadastro = reader.GetDateTime("data_cadastro")
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }
            return clientes;
        }
    }
}

