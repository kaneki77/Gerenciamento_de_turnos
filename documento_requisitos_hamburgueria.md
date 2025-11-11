# Documento de Requisitos do Sistema de Gerenciamento de Hamburgueria

## 1. Introdução

Este documento descreve os requisitos funcionais e não funcionais para o desenvolvimento de um Sistema de Gerenciamento de Hamburgueria. O sistema visa otimizar os processos de registro de pedidos, controle de estoque, cadastro de clientes e gestão financeira básica.

## 2. Requisitos Funcionais (RF)

| ID | Descrição do Requisito |
| :--- | :--- |
| RF01 | O sistema deve permitir o cadastro e gerenciamento de clientes. |
| RF02 | O sistema deve permitir o cadastro e gerenciamento de produtos (hambúrgueres, acompanhamentos, bebidas). |
| RF03 | O sistema deve permitir o registro de pedidos, associando-os a um cliente (opcional) e a uma mesa (opcional). |
| RF04 | O sistema deve permitir a gestão e baixa automática de estoque de ingredientes após a finalização de um pedido. |
| RF05 | O sistema deve permitir o registro de fornecedores e a gestão de compras de insumos. |
| RF06 | O sistema deve permitir o fechamento de caixa diário. |
| RF07 | O sistema deve gerar relatórios de vendas por período e por produto. |
| RF08 | O sistema deve permitir o cadastro e gerenciamento de usuários (funcionários) com diferentes níveis de acesso (ex: Atendente, Gerente). |
| RF09 | O sistema deve permitir o registro de despesas operacionais. |

## 3. Priorização de Requisitos (MoSCoW)

A priorização dos requisitos foi realizada utilizando a técnica MoSCoW (Must have, Should have, Could have, Won't have).

| ID | Descrição do Requisito | Prioridade | Justificativa |
| :--- | :--- | :--- | :--- |
| RF01 | Cadastro e gerenciamento de clientes. | Should have | Importante para fidelização e pedidos futuros, mas o core do sistema não depende disso para funcionar. |
| RF02 | Cadastro e gerenciamento de produtos. | Must have | Essencial para o registro de pedidos e controle de vendas. |
| RF03 | Registro de pedidos. | Must have | Função central do sistema de uma hamburgueria. |
| RF04 | Gestão e baixa automática de estoque. | Must have | Crítico para o controle de custos e produção. |
| RF05 | Registro de fornecedores e gestão de compras. | Should have | Necessário para a gestão completa, mas pode ser manual inicialmente. |
| RF06 | Fechamento de caixa diário. | Must have | Essencial para o controle financeiro e prestação de contas. |
| RF07 | Relatórios de vendas. | Should have | Ajuda na tomada de decisão, mas não é o requisito mínimo para a operação. |
| RF08 | Cadastro e gerenciamento de usuários. | Must have | Necessário para segurança e rastreabilidade das operações. |
| RF09 | Registro de despesas operacionais. | Could have | Funcionalidade de valor agregado, mas não prioritária. |

## 4. Casos de Uso Principais

### 4.1. Registrar um Novo Pedido

| Campo | Descrição |
| :--- | :--- |
| **Ator Principal** | Atendente |
| **Objetivo** | Registrar um pedido de cliente no sistema. |
| **Pré-condição** | O atendente está logado no sistema. |
| **Pós-condição** | O pedido é registrado, o estoque é reservado (ou baixado) e o status do pedido é "Em Preparação". |
| **Fluxo Principal** | 1. O atendente inicia um novo pedido. 2. O atendente seleciona os produtos e quantidades. 3. O atendente informa a forma de consumo (Mesa/Viagem/Delivery). 4. O atendente finaliza o pedido. 5. O sistema registra o pedido e atualiza o estoque. |

### 4.2. Finalizar Pagamento e Fechar Pedido

| Campo | Descrição |
| :--- | :--- |
| **Ator Principal** | Atendente |
| **Objetivo** | Registrar o pagamento e concluir o pedido. |
| **Pré-condição** | O pedido está registrado e pronto para pagamento. |
| **Pós-condição** | O pedido tem o status alterado para "Finalizado" e o valor é contabilizado no caixa. |
| **Fluxo Principal** | 1. O atendente seleciona o pedido a ser pago. 2. O atendente informa o valor e a forma de pagamento. 3. O sistema registra a transação. 4. O sistema altera o status do pedido para "Finalizado". |

## 5. Modelagem de Dados (DER e Dicionário de Dados)

A modelagem de dados será detalhada na próxima seção, mas as principais entidades envolvidas são: `Cliente`, `Produto`, `Ingrediente`, `Pedido`, `ItemPedido`, `Estoque`, `Usuario`, `Caixa`.

---

**Próxima Etapa:** Detalhamento do Diagrama Entidade-Relacionamento (DER) e Dicionário de Dados.


## 6. Diagrama Entidade-Relacionamento (DER)

O modelo de dados a seguir representa as principais entidades e seus relacionamentos:

| Entidade | Atributos (Chave Primária - PK, Chave Estrangeira - FK) | Relacionamentos |
| :--- | :--- | :--- |
| **Cliente** | `id_cliente` (PK), `nome`, `telefone`, `email`, `data_cadastro` | 1:N com Pedido |
| **Produto** | `id_produto` (PK), `nome`, `descricao`, `preco_venda`, `ativo` | 1:N com ItemPedido, N:M com Ingrediente (via ProdutoIngrediente) |
| **Ingrediente** | `id_ingrediente` (PK), `nome`, `unidade_medida` (ex: g, ml, un), `estoque_minimo` | N:M com Produto (via ProdutoIngrediente), 1:N com Estoque |
| **ProdutoIngrediente** | `id_produto` (FK, PK), `id_ingrediente` (FK, PK), `quantidade_necessaria` | N:M entre Produto e Ingrediente |
| **Pedido** | `id_pedido` (PK), `id_cliente` (FK, opcional), `data_hora_pedido`, `status` (ex: Em Preparação, Finalizado), `total_pedido` | N:1 com Cliente, 1:N com ItemPedido |
| **ItemPedido** | `id_item_pedido` (PK), `id_pedido` (FK), `id_produto` (FK), `quantidade`, `preco_unitario`, `subtotal` | N:1 com Pedido, N:1 com Produto |
| **Estoque** | `id_estoque` (PK), `id_ingrediente` (FK), `quantidade_atual`, `data_ultima_atualizacao` | N:1 com Ingrediente |
| **Usuario** | `id_usuario` (PK), `nome`, `login`, `senha_hash`, `nivel_acesso` | |
| **Caixa** | `id_caixa` (PK), `id_usuario` (FK - quem abriu/fechou), `data_abertura`, `data_fechamento`, `saldo_inicial`, `saldo_final` | N:1 com Usuario |
| **Transacao** | `id_transacao` (PK), `id_caixa` (FK), `tipo` (Entrada/Saída), `descricao`, `valor`, `data_hora` | N:1 com Caixa |

## 7. Dicionário de Dados

| Entidade | Atributo | Tipo de Dado (MySQL) | Restrições | Descrição |
| :--- | :--- | :--- | :--- | :--- |
| **Cliente** | `id_cliente` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do cliente. |
| | `nome` | VARCHAR(100) | NOT NULL | Nome completo do cliente. |
| | `telefone` | VARCHAR(15) | UNIQUE | Telefone de contato. |
| | `email` | VARCHAR(100) | UNIQUE, NULL | Endereço de e-mail. |
| | `data_cadastro` | DATETIME | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data e hora do cadastro. |
| **Produto** | `id_produto` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do produto. |
| | `nome` | VARCHAR(100) | NOT NULL, UNIQUE | Nome do item (ex: X-Bacon, Coca-Cola). |
| | `descricao` | VARCHAR(255) | NULL | Descrição detalhada do produto. |
| | `preco_venda` | DECIMAL(10, 2) | NOT NULL, CHECK (> 0) | Preço de venda ao consumidor. |
| | `ativo` | BOOLEAN | NOT NULL, DEFAULT TRUE | Indica se o produto está disponível para venda. |
| **Ingrediente** | `id_ingrediente` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do ingrediente. |
| | `nome` | VARCHAR(100) | NOT NULL, UNIQUE | Nome do ingrediente (ex: Pão Brioche, Queijo Cheddar). |
| | `unidade_medida` | VARCHAR(10) | NOT NULL | Unidade de medida (ex: g, ml, un). |
| | `estoque_minimo` | DECIMAL(10, 2) | NOT NULL, DEFAULT 0 | Quantidade mínima para alerta de estoque. |
| **ProdutoIngrediente** | `id_produto` | INT | PK, FK (Produto) | Chave estrangeira para Produto. |
| | `id_ingrediente` | INT | PK, FK (Ingrediente) | Chave estrangeira para Ingrediente. |
| | `quantidade_necessaria` | DECIMAL(10, 2) | NOT NULL, CHECK (> 0) | Quantidade do ingrediente necessária para o produto. |
| **Pedido** | `id_pedido` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do pedido. |
| | `id_cliente` | INT | FK (Cliente), NULL | Cliente associado ao pedido (opcional). |
| | `data_hora_pedido` | DATETIME | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data e hora em que o pedido foi registrado. |
| | `status` | ENUM | NOT NULL, DEFAULT 'Pendente' | Status do pedido (Pendente, Em Preparação, Pronto, Finalizado, Cancelado). |
| | `total_pedido` | DECIMAL(10, 2) | NOT NULL, DEFAULT 0 | Valor total do pedido. |
| **ItemPedido** | `id_item_pedido` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do item no pedido. |
| | `id_pedido` | INT | FK (Pedido), NOT NULL | Chave estrangeira para Pedido. |
| | `id_produto` | INT | FK (Produto), NOT NULL | Chave estrangeira para Produto. |
| | `quantidade` | INT | NOT NULL, CHECK (> 0) | Quantidade do produto no pedido. |
| | `preco_unitario` | DECIMAL(10, 2) | NOT NULL | Preço do produto no momento do pedido. |
| | `subtotal` | DECIMAL(10, 2) | NOT NULL | Subtotal do item (quantidade * preco_unitario). |
| **Estoque** | `id_estoque` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do registro de estoque. |
| | `id_ingrediente` | INT | FK (Ingrediente), UNIQUE | Chave estrangeira para Ingrediente. |
| | `quantidade_atual` | DECIMAL(10, 2) | NOT NULL, DEFAULT 0 | Quantidade atual do ingrediente em estoque. |
| | `data_ultima_atualizacao` | DATETIME | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data e hora da última atualização. |
| **Usuario** | `id_usuario` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do usuário. |
| | `nome` | VARCHAR(100) | NOT NULL | Nome do usuário/funcionário. |
| | `login` | VARCHAR(50) | NOT NULL, UNIQUE | Login de acesso ao sistema. |
| | `senha_hash` | VARCHAR(255) | NOT NULL | Senha criptografada (hash). |
| | `nivel_acesso` | ENUM | NOT NULL, DEFAULT 'Atendente' | Nível de acesso (Atendente, Gerente). |
| **Caixa** | `id_caixa` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único do caixa. |
| | `id_usuario` | INT | FK (Usuario), NOT NULL | Usuário responsável pela abertura/fechamento. |
| | `data_abertura` | DATETIME | NOT NULL | Data e hora da abertura do caixa. |
| | `data_fechamento` | DATETIME | NULL | Data e hora do fechamento do caixa. |
| | `saldo_inicial` | DECIMAL(10, 2) | NOT NULL, DEFAULT 0 | Saldo inicial do caixa. |
| | `saldo_final` | DECIMAL(10, 2) | NULL | Saldo final do caixa após o fechamento. |
| **Transacao** | `id_transacao` | INT | PK, NOT NULL, AUTO_INCREMENT | Identificador único da transação. |
| | `id_caixa` | INT | FK (Caixa), NOT NULL | Caixa ao qual a transação pertence. |
| | `tipo` | ENUM | NOT NULL | Tipo de transação (Entrada, Saída). |
| | `descricao` | VARCHAR(255) | NOT NULL | Descrição da transação (ex: Pagamento Pedido #123, Compra de Pão). |
| | `valor` | DECIMAL(10, 2) | NOT NULL, CHECK (> 0) | Valor da transação. |
| | `data_hora` | DATETIME | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data e hora da transação. |



## 8. Evidências do D0 (Kickoff + Base do Projeto)

- **Documento Revisado de Requisitos:** Este documento.
- **DER Final e Dicionário de Dados:** Seções 6 e 7 e **Figura 1**.
- **Repositório Criado:** https://github.com/kaneki77/System_Hamburger
- **Guia de Build:** https://github.com/kaneki77/System_Hamburger
- **Print da Compilação:** Ver arquivo anexo: compilacao_d0.txt

**Figura 1:** Diagrama Entidade-Relacionamento (DER) do Sistema de Gerenciamento de Hamburgueria.

![Diagrama Entidade-Relacionamento (DER)](/home/ubuntu/der_hamburgueria.png)

