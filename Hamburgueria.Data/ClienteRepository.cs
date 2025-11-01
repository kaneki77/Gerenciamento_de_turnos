
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DbConnection _dbConnection = new DbConnection();

        public ClienteRepository() { }

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
        // Implementação do CRUD: READ (GetById)
        public Cliente GetById(int id)
        {
            const string query = "SELECT id_cliente, nome, telefone, email, data_cadastro FROM Cliente WHERE id_cliente = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cliente
                            {
                                Id = reader.GetInt32("id_cliente"),
                                Nome = reader.GetString("nome"),
                                Telefone = reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                DataCadastro = reader.GetDateTime("data_cadastro")
                            };
                        }
                        return null;
                    }
                }
            }
        }
    }
}

