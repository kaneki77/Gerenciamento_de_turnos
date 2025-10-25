
# Sistema de Gerenciamento de Hamburgueria

Este é o repositório do projeto de Gerenciamento de Hamburgueria, desenvolvido com C# (WinForms) e MySQL.

## Estrutura da Solução

A solução segue uma arquitetura em três camadas:

1.  **Hamburgueria.UI (User Interface):** Camada de apresentação (WinForms).
2.  **Hamburgueria.Domain (Domínio):** Camada de regras de negócio e modelos (classes de entidade).
3.  **Hamburgueria.Data (Dados):** Camada de acesso ao banco de dados (MySQL).

## Guia de Build (Compilação)

O projeto está configurado para ser compilado em um ambiente com o .NET Framework ou .NET Core (dependendo da sua escolha, assumindo .NET Framework para WinForms tradicional).

### Pré-requisitos

1.  **Visual Studio:** Versão 2019 ou superior.
2.  **.NET Framework:** Versão 4.7.2 ou superior.
3.  **MySQL Server:** Para a fase D15 em diante.
4.  **MySQL Connector/NET:** Para a conexão C# ↔ MySQL.

### Passos para Compilação (Simulação de "Hello World")

Como este é um projeto em fase inicial (D0), as camadas estão vazias, mas a estrutura da solução deve compilar.

1.  **Clonar o Repositório:**
    \`\`\`bash
    git clone [LINK DO REPOSITÓRIO]
    \`\`\`
2.  **Abrir a Solução:**
    Abra o arquivo de solução (`.sln`) no Visual Studio.
3.  **Restaurar Pacotes (Opcional, mas recomendado):**
    No Visual Studio, vá em **Tools** > **NuGet Package Manager** > **Package Manager Console** e execute:
    \`\`\`powershell
    Update-Package -reinstall
    \`\`\`
4.  **Compilar:**
    No Visual Studio, pressione `F6` ou vá em **Build** > **Build Solution**.

**Critério de Aceitação D0:** O projeto deve compilar sem erros, mesmo com as camadas vazias, demonstrando a estrutura básica da solução.

## Próximas Etapas (D15)

Na próxima fase, focaremos na criação do script DDL (MySQL) e na implementação da conexão C# ao banco de dados.

