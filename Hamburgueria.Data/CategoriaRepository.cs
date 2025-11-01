using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DbConnection _dbConnection = new DbConnection();

        public void Adicionar(Categoria categoria)
        {
            const string query = "INSERT INTO Categoria (Nome, Descricao) VALUES (@Nome, @Descricao)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", categoria.Nome);
                    command.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Categoria categoria)
        {
            const string query = "UPDATE Categoria SET Nome = @Nome, Descricao = @Descricao WHERE ID_Categoria = @ID";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", categoria.Nome);
                    command.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ID", categoria.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remover(int id)
        {
            const string query = "DELETE FROM Categoria WHERE ID_Categoria = @ID";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Categoria GetById(int id)
        {
            const string query = "SELECT ID_Categoria, Nome, Descricao FROM Categoria WHERE ID_Categoria = @ID";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Categoria
                            {
                                Id = reader.GetInt32("ID_Categoria"),
                                Nome = reader.GetString("Nome"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao")
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public List<Categoria> GetAll()
        {
            var categorias = new List<Categoria>();
            const string query = "SELECT ID_Categoria, Nome, Descricao FROM Categoria";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorias.Add(new Categoria
                            {
                                Id = reader.GetInt32("ID_Categoria"),
                                Nome = reader.GetString("Nome"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao")
                            });
                        }
                    }
                }
            }
            return categorias;
        }
    }
}
