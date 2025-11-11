
-- Arquivo: ddl_hamburgueria.sql
-- Script DDL para criação do banco de dados e tabelas do Sistema de Controle de Estoque de Insumos

-- 1. DROP DATABASE (Para garantir que o banco suba "limpo" do zero)
DROP DATABASE IF EXISTS hamburgueria_db;

-- 2. CREATE DATABASE
CREATE DATABASE hamburgueria_db;
USE hamburgueria_db;

-- 3. CREATE TABLE Usuario
CREATE TABLE Usuario (
    id_usuario INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) UNIQUE NOT NULL,
    senha_hash VARCHAR(255) NOT NULL,
    nivel_acesso ENUM("Atendente", "Gerente") NOT NULL DEFAULT "Atendente"
);

-- 4. CREATE TABLE Produto (Itens que consomem estoque)
CREATE TABLE Produto (
    id_produto INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) UNIQUE NOT NULL,
    descricao VARCHAR(255),
    ativo BOOLEAN NOT NULL DEFAULT TRUE
);

-- 5. CREATE TABLE Ingrediente (Insumos)
CREATE TABLE Ingrediente (
    id_ingrediente INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) UNIQUE NOT NULL,
    unidade_medida VARCHAR(10) NOT NULL COMMENT "Ex: g, ml, un",
    estoque_minimo DECIMAL(10, 2) NOT NULL DEFAULT 0
);

-- 6. CREATE TABLE Estoque (Relacionamento 1:1 com Ingrediente)
CREATE TABLE Estoque (
    id_estoque INT PRIMARY KEY AUTO_INCREMENT,
    id_ingrediente INT UNIQUE NOT NULL,
    quantidade_atual DECIMAL(10, 2) NOT NULL DEFAULT 0,
    data_ultima_atualizacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (id_ingrediente) REFERENCES Ingrediente(id_ingrediente)
);

-- 7. CREATE TABLE ProdutoIngrediente (Receita - N:M entre Produto e Ingrediente)
CREATE TABLE ProdutoIngrediente (
    id_produto INT NOT NULL,
    id_ingrediente INT NOT NULL,
    quantidade_necessaria DECIMAL(10, 2) NOT NULL CHECK (quantidade_necessaria > 0),
    PRIMARY KEY (id_produto, id_ingrediente),
    FOREIGN KEY (id_produto) REFERENCES Produto(id_produto),
    FOREIGN KEY (id_ingrediente) REFERENCES Ingrediente(id_ingrediente)
);

-- 8. CREATE TABLE SaidaProduto (Registro de consumo/venda)
CREATE TABLE SaidaProduto (
    id_saida INT PRIMARY KEY AUTO_INCREMENT,
    id_produto INT NOT NULL,
    id_usuario INT NOT NULL,
    quantidade_saida INT NOT NULL CHECK (quantidade_saida > 0),
    data_hora DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_produto) REFERENCES Produto(id_produto),
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);

-- 9. CREATE INDEX (Opcional, mas recomendado para performance)
CREATE INDEX idx_saida_produto ON SaidaProduto(id_produto);
CREATE INDEX idx_saida_usuario ON SaidaProduto(id_usuario);
CREATE INDEX idx_estoque_ingrediente ON Estoque(id_ingrediente);
