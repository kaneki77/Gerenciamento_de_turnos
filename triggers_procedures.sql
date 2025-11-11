
-- Arquivo: triggers_procedures.sql
-- Triggers e Procedures para Regras de Negócio no BD (Baixa de Estoque)

USE hamburgueria_db;

-- =================================================================
-- 1. TRIGGER: tg_saida_produto_after_insert
-- Regra Crítica do Domínio: Baixa de Estoque
-- Executa a baixa de estoque quando um registro é inserido na tabela SaidaProduto
-- =================================================================
DROP TRIGGER IF EXISTS tg_saida_produto_after_insert;

DELIMITER //
CREATE TRIGGER tg_saida_produto_after_insert
AFTER INSERT ON SaidaProduto
FOR EACH ROW
BEGIN
    -- Atualiza a tabela Estoque (quantidade_atual)
    -- com base na Receita (ProdutoIngrediente) e na quantidade de Saída (NEW.quantidade_saida)

    UPDATE Estoque e
    JOIN Ingrediente i ON e.id_ingrediente = i.id_ingrediente
    JOIN ProdutoIngrediente pi ON i.id_ingrediente = pi.id_ingrediente
    SET e.quantidade_atual = e.quantidade_atual - (NEW.quantidade_saida * pi.quantidade_necessaria)
    WHERE pi.id_produto = NEW.id_produto;

    -- **NOTA:** Em um sistema de produção, seria necessário adicionar uma verificação
    -- para garantir que a quantidade_atual não se torne negativa (estoque insuficiente)
    -- e lançar um erro antes da inserção, ou usar uma Stored Procedure para gerenciar a transação.
END //
DELIMITER ;

-- =================================================================
-- 2. PROCEDURE: RegistrarSaidaProduto
-- Procedure para registrar a saída de um produto e acionar a baixa de estoque
-- (Pode ser usada pelo sistema C# para garantir a execução da trigger)
-- =================================================================
DROP PROCEDURE IF EXISTS RegistrarSaidaProduto;

DELIMITER //
CREATE PROCEDURE RegistrarSaidaProduto (
    IN p_id_produto INT,
    IN p_id_usuario INT,
    IN p_quantidade INT
)
BEGIN
    -- Verifica se a quantidade é válida
    IF p_quantidade <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'A quantidade de saída deve ser maior que zero.';
    END IF;

    -- Insere o registro na tabela SaidaProduto, o que acionará a trigger de baixa de estoque
    INSERT INTO SaidaProduto (id_produto, id_usuario, quantidade_saida)
    VALUES (p_id_produto, p_id_usuario, p_quantidade);
END //
DELIMITER ;
