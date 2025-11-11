
# Sistema de Controle de Estoque de Insumos para Hamburgueria

Este é o repositório do projeto de **Controle de Estoque de Insumos** para Hamburgueria, desenvolvido com C# (WinForms) e MySQL. O escopo foi ajustado para focar exclusivamente na gestão de insumos, receitas e baixa automática de estoque, complementando um sistema de PDV já existente.

## Estrutura da Solução

A solução segue uma arquitetura em três camadas:

1.  **Hamburgueria.UI (User Interface):** Camada de apresentação (WinForms).
2.  **Hamburgueria.Domain (Domínio):** Camada de regras de negócio, serviços e modelos (Entidades: Ingrediente, Produto, SaidaProduto).
3.  **Hamburgueria.Data (Dados):** Camada de acesso ao banco de dados MySQL (utilizando ADO.NET com MySqlConnector).

## Guia de Build e Configuração

### Pré-requisitos

1.  **Visual Studio:** Versão 2019 ou superior.
2.  **.NET Framework:** Versão 4.7.2 ou superior.
3.  **MySQL Server:** Versão 8.0 ou superior.
4.  **NuGet Package:** Instalar **MySql.Data** (ou MySqlConnector) no projeto `Hamburgueria.Data`.

### Configuração do Banco de Dados

1.  **Criar Banco e Tabelas (DDL):** Execute o script `ddl_hamburgueria.sql`.
2.  **Popular Dados (DML):** Execute o script `dml_hamburgueria.sql` para inserir dados de teste (Usuários, Ingredientes, Estoque Inicial, Produtos e Receitas).
3.  **Regras de Negócio no BD:** Execute o script `triggers_procedures.sql` para criar a **Trigger de Baixa Automática de Estoque** e a Procedure de registro de saída.

### Configuração da Conexão C#

1.  Abra o arquivo `Hamburgueria.Data/DbConnection.cs`.
2.  **ATUALIZE** a `ConnectionString` com suas credenciais do MySQL:
    \`\`\`csharp
    private const string ConnectionString = "Server=localhost;Database=hamburgueria_db;Uid=root;Pwd=sua_senha_aqui;";
    \`\`\`

## Status de Entrega do Projeto (D0, D15, D30)

O projeto está com o desenvolvimento das entregas D0, D15 e D30 **concluído** e refatorado para o novo escopo.

### D0 (Kickoff + Base do Projeto) - Concluído

| Entrega | Critério de Aceitação | Status |
| :--- | :--- | :--- |
| **Documento de Requisitos (PDF)** | Requisitos priorizados (MoSCoW) e casos de uso confirmados. | **Concluído** (Documento externo atualizado) |
| **DER Final e Dicionário de Dados** | DER consistente (focado em Estoque, Insumos e Receitas). | **Concluído** (Modelagem atualizada) |
| **Estrutura C#** | Projeto compila "Hello World" com camadas vazias. | **Concluído** (Arquivos .sln e .csproj configurados) |

### D15 (Modelo Físico + DDL + Conexão C#↔MySQL) - Concluído

| Entrega | Critério de Aceitação | Arquivo(s) |
| :--- | :--- | :--- |
| **Script DDL completo** | Banco sobe "limpo" do zero. | `ddl_hamburgueria.sql` |
| **Semente inicial de dados** | Popula dados de exemplo. | `dml_hamburgueria.sql` |
| **Camada de acesso a dados** | App C# conecta no banco e executa 1 SELECT real. | `Hamburgueria.Data/DbConnection.cs`, `SaidaProdutoRepository.cs` |

### D30 (Regras de Negócio no BD + CRUDs Essenciais) - Concluído

| Entrega | Critério de Aceitação | Arquivo(s) |
| :--- | :--- | :--- |
| **CRUDs Essenciais** | CRUDs funcionais da entidade central (`SaidaProduto`). | `Hamburgueria.Data/SaidaProdutoRepository.cs` (CREATE, READ) |
| **Validação** | CRUDs com validação mínima (campos obrigatórios). | `Hamburgueria.Domain/SaidaProdutoService.cs` |
| **Triggers/Procedures** | Trigger/Procedure executa automaticamente a regra crítica do domínio. | `triggers_procedures.sql` (**Baixa Automática de Estoque**) |

## Próximos Passos (Desenvolvimento)

Implementação da interface gráfica (WinForms) para as telas de cadastro de Ingredientes, Produtos e para o registro de Saída de Produto.
