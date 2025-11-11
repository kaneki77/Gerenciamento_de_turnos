
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class SaidaProdutoRepository : RepositorioBase<SaidaProduto>
    {
        private readonly DbConnection _dbConnection;

        public SaidaProdutoRepository()
        {
            _dbConnection = new DbConnection();
        }

        // Implementação do CRUD: CREATE (Registro de Saída)
        public void Adicionar(SaidaProduto saida)
        {
            const string query = "INSERT INTO SaidaProduto (id_produto, id_usuario, quantidade_saida) VALUES (@id_produto, @id_usuario, @quantidade_saida)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_produto", saida.IdProduto);
                    command.Parameters.AddWithValue("@id_usuario", saida.IdUsuario);
                    command.Parameters.AddWithValue("@quantidade_saida", saida.QuantidadeSaida);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Implementação do CRUD: READ (GetAll) - Exemplo de leitura
        public List<SaidaProduto> GetAll()
        {
            var saidas = new List<SaidaProduto>();
            const string query = "SELECT id_saida, id_produto, id_usuario, quantidade_saida, data_hora FROM SaidaProduto";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var saida = new SaidaProduto
                            {
                                Id = reader.GetInt32("id_saida"),
                                IdProduto = reader.GetInt32("id_produto"),
                                IdUsuario = reader.GetInt32("id_usuario"),
                                QuantidadeSaida = reader.GetInt32("quantidade_saida"),
                                DataHora = reader.GetDateTime("data_hora")
                            };
                            saidas.Add(saida);
                        }
                    }
                }
            }
            return saidas;
        }
        
        // Outros métodos CRUD (Atualizar e Remover) não são essenciais para SaidaProduto, mas podem ser implementados se necessário.
    }
}
