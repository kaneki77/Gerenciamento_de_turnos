
-- Arquivo: dml_hamburgueria.sql
-- Script DML para inserção de dados iniciais (semente) para testes do Sistema de Controle de Estoque

USE hamburgueria_db;

-- 1. Inserção de Usuários
INSERT INTO Usuario (nome, login, senha_hash, nivel_acesso) VALUES
("Gerente Master", "admin", "hash_seguro_admin", "Gerente"),
("Atendente 01", "atendente1", "hash_seguro_atendente", "Atendente");

-- 2. Inserção de Ingredientes
INSERT INTO Ingrediente (nome, unidade_medida, estoque_minimo) VALUES
("Pão Brioche", "un", 20),
("Carne Bovina 150g", "un", 30),
("Queijo Cheddar", "g", 500),
("Alface Americana", "un", 10),
("Tomate", "un", 10),
("Molho Especial", "ml", 1000),
("Batata Frita (Porção)", "un", 15),
("Coca-Cola Lata 350ml", "un", 50);

-- 3. Inserção de Estoque (Inicial)
-- Assumindo que o estoque inicial é 10x o estoque mínimo para os ingredientes
INSERT INTO Estoque (id_ingrediente, quantidade_atual)
SELECT id_ingrediente, estoque_minimo * 10
FROM Ingrediente;

-- 4. Inserção de Produtos
INSERT INTO Produto (nome, descricao) VALUES
("X-Burger Clássico", "Pão, carne, queijo, alface, tomate e molho especial."),
("Batata Frita", "Porção média de batata frita."),
("Coca-Cola Lata", "Lata de 350ml.");

-- 5. Inserção de ProdutoIngrediente (Receitas)
-- X-Burger Clássico (ID 1)
INSERT INTO ProdutoIngrediente (id_produto, id_ingrediente, quantidade_necessaria) VALUES
(1, 1, 1), -- Pão Brioche (1 un)
(1, 2, 1), -- Carne Bovina 150g (1 un)
(1, 3, 50), -- Queijo Cheddar (50g)
(1, 4, 0.5), -- Alface Americana (meia folha)
(1, 5, 0.5), -- Tomate (meio tomate)
(1, 6, 20); -- Molho Especial (20ml)

-- Batata Frita (ID 2)
INSERT INTO ProdutoIngrediente (id_produto, id_ingrediente, quantidade_necessaria) VALUES
(2, 7, 1); -- Batata Frita (Porção) (1 un)

-- Coca-Cola Lata (ID 3)
INSERT INTO ProdutoIngrediente (id_produto, id_ingrediente, quantidade_necessaria) VALUES
(3, 8, 1); -- Coca-Cola Lata 350ml (1 un)

-- 6. Inserção de SaidaProduto (Exemplo de consumo)
-- Simulando a saída de 2 X-Burgers e 1 Coca-Cola
INSERT INTO SaidaProduto (id_produto, id_usuario, quantidade_saida) VALUES
(1, 2, 2), -- 2 X-Burgers, registrado pelo Atendente 01
(3, 2, 1); -- 1 Coca-Cola, registrado pelo Atendente 01
