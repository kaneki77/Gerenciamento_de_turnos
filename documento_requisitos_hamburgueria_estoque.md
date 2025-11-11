
# Documento de Requisitos do Sistema de Controle de Estoque de Insumos

## 1. Introdução

Este documento descreve os requisitos funcionais e não funcionais para o desenvolvimento de um Sistema de **Controle de Estoque de Insumos** para Hamburgueria. O sistema visa gerenciar o cadastro de produtos, a composição de receitas e a baixa automática de estoque de ingredientes.

## 2. Requisitos Funcionais (RF)

| ID | Descrição do Requisito |
| :--- | :--- |
| RF01 | O sistema deve permitir o cadastro e gerenciamento de **Ingredientes** (insumos). |
| RF02 | O sistema deve permitir o cadastro e gerenciamento de **Produtos** (itens do menu) que consomem ingredientes. |
| RF03 | O sistema deve permitir a definição da **Receita** (composição) de cada Produto, associando Ingredientes e suas quantidades necessárias. |
| RF04 | O sistema deve permitir a **gestão do Estoque** de cada Ingrediente, registrando a quantidade atual e o estoque mínimo. |
| RF05 | O sistema deve permitir o registro de uma **Saída de Produto** (simulando uma venda) para acionar a baixa de estoque. |
| RF06 | O sistema deve realizar a **baixa automática** de Ingredientes do Estoque com base na Receita do Produto e na quantidade de Saída registrada. |
| RF07 | O sistema deve permitir o cadastro e gerenciamento de **Usuários** com diferentes níveis de acesso. |

## 3. Priorização de Requisitos (MoSCoW)

A priorização dos requisitos foi realizada utilizando a técnica MoSCoW (Must have, Should have, Could have, Won't have).

| ID | Descrição do Requisito | Prioridade | Justificativa |
| :--- | :--- | :--- | :--- |
| RF01 | Cadastro e gerenciamento de Ingredientes. | Must have | Essencial para o controle de insumos. |
| RF02 | Cadastro e gerenciamento de Produtos. | Must have | Necessário para definir o que será consumido. |
| RF03 | Definição da Receita (ProdutoIngrediente). | Must have | Crítico para o cálculo da baixa de estoque. |
| RF04 | Gestão do Estoque (quantidade atual e mínimo). | Must have | Função central do sistema. |
| RF05 | Registro de Saída de Produto. | Must have | Gatilho para a baixa de estoque. |
| RF06 | Baixa automática de Ingredientes. | Must have | Regra de negócio crítica do domínio. |
| RF07 | Cadastro e gerenciamento de Usuários. | Should have | Necessário para segurança e rastreabilidade das operações. |

## 4. Casos de Uso Principais

### 4.1. Registrar Saída de Produto e Baixa de Estoque

| Campo | Descrição |
| :--- | :--- |
| **Ator Principal** | Usuário (Atendente/Gerente) |
| **Objetivo** | Registrar a saída de um produto e atualizar o estoque de insumos. |
| **Pré-condição** | O Produto e seus Ingredientes estão cadastrados, e o Estoque tem saldo. |
| **Pós-condição** | O registro de saída é efetuado e a quantidade de Ingredientes no Estoque é reduzida. |
| **Fluxo Principal** | 1. O Usuário registra a saída de um Produto e a quantidade. 2. O sistema consulta a Receita do Produto. 3. O sistema calcula a quantidade total de cada Ingrediente a ser baixada. 4. O sistema atualiza o Estoque. |

## 5. Modelagem de Dados (DER e Dicionário de Dados)

As principais entidades envolvidas são: `Produto`, `Ingrediente`, `ProdutoIngrediente`, `Estoque`, `Usuario`, e uma nova entidade `SaidaProduto` para registrar o consumo.

| Entidade | Atributos (Chave Primária - PK, Chave Estrangeira - FK) | Relacionamentos |
| :--- | :--- | :--- |
| **Produto** | `id_produto` (PK), `nome`, `descricao`, `ativo` | N:M com Ingrediente (via ProdutoIngrediente), 1:N com SaidaProduto |
| **Ingrediente** | `id_ingrediente` (PK), `nome`, `unidade_medida`, `estoque_minimo` | N:M com Produto (via ProdutoIngrediente), 1:1 com Estoque |
| **ProdutoIngrediente** | `id_produto` (FK, PK), `id_ingrediente` (FK, PK), `quantidade_necessaria` | N:M entre Produto e Ingrediente |
| **Estoque** | `id_estoque` (PK), `id_ingrediente` (FK), `quantidade_atual`, `data_ultima_atualizacao` | 1:1 com Ingrediente |
| **Usuario** | `id_usuario` (PK), `nome`, `login`, `senha_hash`, `nivel_acesso` | 1:N com SaidaProduto |
| **SaidaProduto** | `id_saida` (PK), `id_produto` (FK), `id_usuario` (FK), `quantidade_saida`, `data_hora` | N:1 com Produto, N:1 com Usuario |

## 6. Dicionário de Dados

| Entidade | Atributo | Tipo de Dado (MySQL) | Restrições | Descrição |
| :--- | :--- | :--- | :--- | :--- |
| **Produto** | `id_produto` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do produto. |
| | `nome` | VARCHAR(100) | NOT NULL, UNIQUE | Nome do item (ex: X-Bacon, Coca-Cola). |
| | `descricao` | VARCHAR(255) | NULL | Descrição detalhada do produto. |
| | `ativo` | BOOLEAN | NOT NULL, DEFAULT TRUE | Indica se o produto está ativo. |
| **Ingrediente** | `id_ingrediente` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do ingrediente. |
| | `nome` | VARCHAR(100) | NOT NULL, UNIQUE | Nome do ingrediente (ex: Pão Brioche, Queijo Cheddar). |
| | `unidade_medida` | VARCHAR(10) | NOT NULL | Unidade de medida (ex: g, ml, un). |
| | `estoque_minimo` | DECIMAL(10, 2) | NOT NULL, DEFAULT 0 | Quantidade mínima para alerta de estoque. |
| **ProdutoIngrediente** | `id_produto` | INT | PK, FK (Produto) | Chave estrangeira para Produto. |
| | `id_ingrediente` | INT | PK, FK (Ingrediente) | Chave estrangeira para Ingrediente. |
| | `quantidade_necessaria` | DECIMAL(10, 2) | NOT NULL, CHECK (> 0) | Quantidade do ingrediente necessária para o produto. |
| **Estoque** | `id_estoque` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do registro de estoque. |
| | `id_ingrediente` | INT | FK (Ingrediente), UNIQUE | Chave estrangeira para Ingrediente. |
| | `quantidade_atual` | DECIMAL(10, 2) | NOT NULL, DEFAULT 0 | Quantidade atual do ingrediente em estoque. |
| | `data_ultima_atualizacao` | DATETIME | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data e hora da última atualização. |
| **Usuario** | `id_usuario` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do usuário. |
| | `nome` | VARCHAR(100) | NOT NULL | Nome do usuário/funcionário. |
| | `login` | VARCHAR(50) | NOT NULL, UNIQUE | Login de acesso ao sistema. |
| | `senha_hash` | VARCHAR(255) | NOT NULL | Senha criptografada (hash). |
| | `nivel_acesso` | ENUM | NOT NULL, DEFAULT 'Atendente' | Nível de acesso (Atendente, Gerente). |
| **SaidaProduto** | `id_saida` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único da saída. |
| | `id_produto` | INT | FK (Produto), NOT NULL | Produto que saiu do estoque. |
| | `id_usuario` | INT | FK (Usuario), NOT NULL | Usuário que registrou a saída. |
| | `quantidade_saida` | INT | NOT NULL, CHECK (> 0) | Quantidade do produto que saiu. |
| | `data_hora` | DATETIME | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data e hora da saída. |

## 7. Evidências do D0 (Kickoff + Base do Projeto)

- **Documento Revisado de Requisitos:** Este documento.
- **DER Final e Dicionário de Dados:** Seções 5 e 6.
- **Repositório Criado:** https://github.com/kaneki77/System_Hamburger
- **Guia de Build:** No README.md do repositório.
- **Print da Compilação:** Ver arquivo anexo: compilacao_d0.txt
