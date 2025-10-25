
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class ClienteRepository : RepositorioBase<Cliente>
    {
        private readonly DbConnection _dbConnection;

        public ClienteRepository()
        {
            _dbConnection = new DbConnection();
        }

        // Implementação do CRUD: CREATE
        public void Adicionar(Cliente cliente)
        {
            const string query = "INSERT INTO Cliente (nome, telefone, email) VALUES (@nome, @telefone, @email)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@email", cliente.Email ?? (object)DBNull.Value); // Trata NULL
                    command.ExecuteNonQuery();
                }
            }
        }

        // Implementação do CRUD: READ (GetAll)
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

        // Implementação do CRUD: UPDATE
        public void Atualizar(Cliente cliente)
        {
            const string query = "UPDATE Cliente SET nome = @nome, telefone = @telefone, email = @email WHERE id_cliente = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@email", cliente.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@id", cliente.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Implementação do CRUD: DELETE
        public void Remover(int id)
        {
            const string query = "DELETE FROM Cliente WHERE id_cliente = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

