
-- Arquivo: dml_hamburgueria.sql
-- Script DML para inserção de dados iniciais (semente) para testes

USE hamburgueria_db;

-- 1. Inserção de Usuários
INSERT INTO Usuario (nome, login, senha_hash, nivel_acesso) VALUES
('Gerente Master', 'admin', 'hash_seguro_admin', 'Gerente'),
('Atendente 01', 'atendente1', 'hash_seguro_atendente', 'Atendente');

-- 2. Inserção de Clientes
INSERT INTO Cliente (nome, telefone, email) VALUES
('João Silva', '11987654321', 'joao.silva@teste.com'),
('Maria Souza', '11998765432', 'maria.souza@teste.com');

-- 3. Inserção de Ingredientes
INSERT INTO Ingrediente (nome, unidade_medida, estoque_minimo) VALUES
('Pão Brioche', 'un', 20),
('Carne Bovina 150g', 'un', 30),
('Queijo Cheddar', 'g', 500),
('Alface Americana', 'un', 10),
('Tomate', 'un', 10),
('Molho Especial', 'ml', 1000),
('Batata Frita (Porção)', 'un', 15),
('Coca-Cola Lata 350ml', 'un', 50);

-- 4. Inserção de Estoque (Inicial)
-- Assumindo que o estoque inicial é 10x o estoque mínimo para os ingredientes
INSERT INTO Estoque (id_ingrediente, quantidade_atual)
SELECT id_ingrediente, estoque_minimo * 10
FROM Ingrediente;

-- 5. Inserção de Produtos
INSERT INTO Produto (nome, descricao, preco_venda) VALUES
('X-Burger Clássico', 'Pão, carne, queijo, alface, tomate e molho especial.', 25.00),
('Batata Frita', 'Porção média de batata frita.', 10.00),
('Coca-Cola Lata', 'Lata de 350ml.', 6.00);

-- 6. Inserção de ProdutoIngrediente (Receitas)
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

-- 7. Inserção de Pedido (Exemplo de um pedido completo)
INSERT INTO Pedido (id_cliente, total_pedido, status) VALUES
(1, 41.00, 'Finalizado'); -- Pedido para João Silva

-- 8. Inserção de Itens do Pedido
-- Pedido 1: 1 X-Burger (25.00) + 1 Coca-Cola (6.00) + 1 Batata Frita (10.00) = 41.00
INSERT INTO ItemPedido (id_pedido, id_produto, quantidade, preco_unitario, subtotal) VALUES
(1, 1, 1, 25.00, 25.00),
(1, 3, 1, 6.00, 6.00),
(1, 2, 1, 10.00, 10.00);

-- 9. Inserção de Caixa e Transação (Simulação de Fechamento)
INSERT INTO Caixa (id_usuario, data_abertura, data_fechamento, saldo_inicial, saldo_final) VALUES
(2, NOW() - INTERVAL 5 HOUR, NOW(), 50.00, 91.00); -- Atendente 01

-- Transação do Pedido 1
INSERT INTO Transacao (id_caixa, tipo, descricao, valor) VALUES
(1, 'Entrada', 'Pagamento Pedido #1', 41.00);

-- Transação de Saldo Inicial
INSERT INTO Transacao (id_caixa, tipo, descricao, valor) VALUES
(1, 'Entrada', 'Saldo Inicial', 50.00);

