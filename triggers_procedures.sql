
-- Arquivo: triggers_procedures.sql
-- Triggers e Procedures para Regras de Negócio no BD (D30)

USE hamburgueria_db;

-- =================================================================
-- 1. PROCEDURE: CalcularTotalPedido
-- Atualiza o campo total_pedido na tabela Pedido
-- =================================================================
DROP PROCEDURE IF EXISTS CalcularTotalPedido;

DELIMITER //
CREATE PROCEDURE CalcularTotalPedido (IN p_id_pedido INT)
BEGIN
    DECLARE v_total DECIMAL(10, 2);

    SELECT SUM(subtotal) INTO v_total
    FROM ItemPedido
    WHERE id_pedido = p_id_pedido;

    UPDATE Pedido
    SET total_pedido = COALESCE(v_total, 0)
    WHERE id_pedido = p_id_pedido;
END //
DELIMITER ;

-- =================================================================
-- 2. TRIGGER: tg_itempedido_after_insert
-- Garante que o total do pedido seja recalculado após inserir um item
-- =================================================================
DROP TRIGGER IF EXISTS tg_itempedido_after_insert;

DELIMITER //
CREATE TRIGGER tg_itempedido_after_insert
AFTER INSERT ON ItemPedido
FOR EACH ROW
BEGIN
    CALL CalcularTotalPedido(NEW.id_pedido);
END //
DELIMITER ;

-- =================================================================
-- 3. TRIGGER: tg_itempedido_after_update
-- Garante que o total do pedido seja recalculado após atualizar um item
-- =================================================================
DROP TRIGGER IF EXISTS tg_itempedido_after_update;

DELIMITER //
CREATE TRIGGER tg_itempedido_after_update
AFTER UPDATE ON ItemPedido
FOR EACH ROW
BEGIN
    IF NEW.subtotal <> OLD.subtotal OR NEW.quantidade <> OLD.quantidade THEN
        CALL CalcularTotalPedido(NEW.id_pedido);
    END IF;
END //
DELIMITER ;

-- =================================================================
-- 4. TRIGGER: tg_pedido_after_update_estoque
-- Regra Crítica do Domínio: Baixa de Estoque
-- Executa a baixa de estoque quando o status do pedido muda para 'Em Preparação'
-- =================================================================
DROP TRIGGER IF EXISTS tg_pedido_after_update_estoque;

DELIMITER //
CREATE TRIGGER tg_pedido_after_update_estoque
AFTER UPDATE ON Pedido
FOR EACH ROW
BEGIN
    -- Verifica se o status mudou para 'Em Preparação' (ou 'Pronto', dependendo da regra de negócio)
    -- Assumindo que a baixa ocorre ao iniciar a preparação
    IF NEW.status = 'Em Preparação' AND OLD.status <> 'Em Preparação' THEN
        -- Para cada item no pedido
        INSERT INTO Transacao (id_caixa, tipo, descricao, valor) VALUES (1, 'Entrada', 'teste', 1);

        -- Loop para iterar sobre os itens do pedido e seus ingredientes
        -- (Em MySQL, isso exigiria um cursor ou uma abordagem mais complexa,
        -- mas para fins de demonstração, vamos simular a lógica principal
        -- que seria feita em uma procedure mais robusta.)

        -- Lógica:
        -- 1. Selecionar todos os itens do pedido (NEW.id_pedido)
        -- 2. Para cada item, selecionar a "receita" (ProdutoIngrediente)
        -- 3. Para cada ingrediente, calcular a quantidade total a ser baixada (quantidade_necessaria * quantidade_item)
        -- 4. Atualizar a tabela Estoque (quantidade_atual = quantidade_atual - quantidade_a_baixar)

        -- **Aviso:** A baixa de estoque é uma operação complexa que, em um projeto real,
        -- seria feita em uma Stored Procedure para garantir atomicidade e tratamento de erros (estoque insuficiente).
        -- O trigger abaixo é um exemplo simplificado da lógica de negócio:

        UPDATE Estoque e
        JOIN Ingrediente i ON e.id_ingrediente = i.id_ingrediente
        JOIN ProdutoIngrediente pi ON i.id_ingrediente = pi.id_ingrediente
        JOIN ItemPedido ip ON pi.id_produto = ip.id_produto
        SET e.quantidade_atual = e.quantidade_atual - (ip.quantidade * pi.quantidade_necessaria)
        WHERE ip.id_pedido = NEW.id_pedido;

    END IF;
END //
DELIMITER ;

-- =================================================================
-- 5. PROCEDURE: RegistrarTransacaoCaixa
-- Procedure para registrar transações de entrada/saída no caixa
-- (Pode ser usada pelo sistema C# ao finalizar um pagamento)
-- =================================================================
DROP PROCEDURE IF EXISTS RegistrarTransacaoCaixa;

DELIMITER //
CREATE PROCEDURE RegistrarTransacaoCaixa (
    IN p_id_caixa INT,
    IN p_tipo ENUM('Entrada', 'Saída'),
    IN p_descricao VARCHAR(255),
    IN p_valor DECIMAL(10, 2)
)
BEGIN
    INSERT INTO Transacao (id_caixa, tipo, descricao, valor)
    VALUES (p_id_caixa, p_tipo, p_descricao, p_valor);
END //
DELIMITER ;

