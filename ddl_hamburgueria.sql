
-- Arquivo: ddl_hamburgueria.sql
-- Script DDL para criação do banco de dados e tabelas do Sistema de Gerenciamento de Hamburgueria

-- 1. DROP DATABASE (Para garantir que o banco suba "limpo" do zero)
DROP DATABASE IF EXISTS hamburgueria_db;

-- 2. CREATE DATABASE
CREATE DATABASE hamburgueria_db;
USE hamburgueria_db;

-- 3. CREATE TABLE Cliente
CREATE TABLE Cliente (
    id_cliente INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    telefone VARCHAR(15) UNIQUE,
    email VARCHAR(100) UNIQUE,
    data_cadastro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 4. CREATE TABLE Usuario
CREATE TABLE Usuario (
    id_usuario INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) UNIQUE NOT NULL,
    senha_hash VARCHAR(255) NOT NULL,
    nivel_acesso ENUM('Atendente', 'Gerente') NOT NULL DEFAULT 'Atendente'
);

-- 5. CREATE TABLE Produto
CREATE TABLE Produto (
    id_produto INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) UNIQUE NOT NULL,
    descricao VARCHAR(255),
    preco_venda DECIMAL(10, 2) NOT NULL CHECK (preco_venda > 0),
    ativo BOOLEAN NOT NULL DEFAULT TRUE
);

-- 6. CREATE TABLE Ingrediente
CREATE TABLE Ingrediente (
    id_ingrediente INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) UNIQUE NOT NULL,
    unidade_medida VARCHAR(10) NOT NULL COMMENT 'Ex: g, ml, un',
    estoque_minimo DECIMAL(10, 2) NOT NULL DEFAULT 0
);

-- 7. CREATE TABLE Estoque (Relacionamento 1:1 com Ingrediente)
CREATE TABLE Estoque (
    id_estoque INT PRIMARY KEY AUTO_INCREMENT,
    id_ingrediente INT UNIQUE NOT NULL,
    quantidade_atual DECIMAL(10, 2) NOT NULL DEFAULT 0,
    data_ultima_atualizacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (id_ingrediente) REFERENCES Ingrediente(id_ingrediente)
);

-- 8. CREATE TABLE ProdutoIngrediente (N:M entre Produto e Ingrediente)
CREATE TABLE ProdutoIngrediente (
    id_produto INT NOT NULL,
    id_ingrediente INT NOT NULL,
    quantidade_necessaria DECIMAL(10, 2) NOT NULL CHECK (quantidade_necessaria > 0),
    PRIMARY KEY (id_produto, id_ingrediente),
    FOREIGN KEY (id_produto) REFERENCES Produto(id_produto),
    FOREIGN KEY (id_ingrediente) REFERENCES Ingrediente(id_ingrediente)
);

-- 9. CREATE TABLE Pedido
CREATE TABLE Pedido (
    id_pedido INT PRIMARY KEY AUTO_INCREMENT,
    id_cliente INT, -- Opcional
    data_hora_pedido DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status ENUM('Pendente', 'Em Preparação', 'Pronto', 'Finalizado', 'Cancelado') NOT NULL DEFAULT 'Pendente',
    total_pedido DECIMAL(10, 2) NOT NULL DEFAULT 0,
    FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente)
);

-- 10. CREATE TABLE ItemPedido
CREATE TABLE ItemPedido (
    id_item_pedido INT PRIMARY KEY AUTO_INCREMENT,
    id_pedido INT NOT NULL,
    id_produto INT NOT NULL,
    quantidade INT NOT NULL CHECK (quantidade > 0),
    preco_unitario DECIMAL(10, 2) NOT NULL,
    subtotal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (id_pedido) REFERENCES Pedido(id_pedido),
    FOREIGN KEY (id_produto) REFERENCES Produto(id_produto)
);

-- 11. CREATE TABLE Caixa
CREATE TABLE Caixa (
    id_caixa INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario INT NOT NULL,
    data_abertura DATETIME NOT NULL,
    data_fechamento DATETIME,
    saldo_inicial DECIMAL(10, 2) NOT NULL DEFAULT 0,
    saldo_final DECIMAL(10, 2),
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);

-- 12. CREATE TABLE Transacao
CREATE TABLE Transacao (
    id_transacao INT PRIMARY KEY AUTO_INCREMENT,
    id_caixa INT NOT NULL,
    tipo ENUM('Entrada', 'Saída') NOT NULL,
    descricao VARCHAR(255) NOT NULL,
    valor DECIMAL(10, 2) NOT NULL CHECK (valor > 0),
    data_hora DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_caixa) REFERENCES Caixa(id_caixa)
);

-- 13. CREATE INDEX (Opcional, mas recomendado para performance)
CREATE INDEX idx_pedido_cliente ON Pedido(id_cliente);
CREATE INDEX idx_itempedido_pedido ON ItemPedido(id_pedido);
CREATE INDEX idx_itempedido_produto ON ItemPedido(id_produto);
CREATE INDEX idx_estoque_ingrediente ON Estoque(id_ingrediente);
CREATE INDEX idx_transacao_caixa ON Transacao(id_caixa);

