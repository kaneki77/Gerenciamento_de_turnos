# AutoBo Turno Reporter

Uma aplicação web para controle, registro e supervisão de turnos operacionais da Usina Coruripe, com funcionalidade de geração de relatórios em PDF para impressão.

## Funcionalidades

- **Registro de Turnos**: Interface intuitiva para registro de informações de turnos operacionais
- **Listagem de Relatórios**: Visualização de todos os relatórios criados com informações resumidas
- **Geração de PDF**: Criação automática de relatórios em PDF formatados para impressão
- **Interface Responsiva**: Design adaptável para desktop e dispositivos móveis

## Tecnologias Utilizadas

- **Backend**: Flask (Python)
- **Frontend**: HTML5, CSS3, JavaScript
- **Banco de Dados**: SQLite
- **Geração de PDF**: ReportLab
- **Estilo**: CSS customizado com design moderno

## Estrutura do Projeto

```
autobo-turno-reporter/
├── src/
│   ├── models/
│   │   ├── user.py          # Modelo de usuário (template)
│   │   └── report.py        # Modelo de relatório de turno
│   ├── routes/
│   │   ├── user.py          # Rotas de usuário (template)
│   │   └── report.py        # Rotas de relatórios
│   ├── static/
│   │   └── index.html       # Interface principal
│   ├── database/
│   │   └── app.db          # Banco de dados SQLite
│   └── main.py             # Aplicação principal Flask
├── venv/                   # Ambiente virtual Python
├── requirements.txt        # Dependências do projeto
└── README.md              # Este arquivo
```

## Instalação e Execução

### Pré-requisitos

- Python 3.11 ou superior
- pip (gerenciador de pacotes Python)

### Passos para Instalação

1. **Clone ou baixe o projeto**
   ```bash
   cd autobo-turno-reporter
   ```

2. **Ative o ambiente virtual**
   ```bash
   source venv/bin/activate
   ```

3. **Instale as dependências** (já instaladas no ambiente virtual)
   ```bash
   pip install -r requirements.txt
   ```

4. **Execute a aplicação**
   ```bash
   python src/main.py
   ```

5. **Acesse a aplicação**
   Abra seu navegador e acesse: `http://localhost:5000`

## Como Usar

### Criando um Novo Relatório

1. Na página principal, preencha os campos obrigatórios:
   - **Nome do Operador**: Nome completo do operador responsável
   - **Tipo de Turno**: Selecione entre Turno A, B, C ou Especial
   - **Data/Hora do Turno**: Data e hora do turno (preenchida automaticamente)
   - **Atividades**: Descrição detalhada das atividades realizadas

2. Opcionalmente, preencha:
   - **Ocorrências**: Registre incidentes, paradas, alarmes, etc.
   - **Observações**: Observações gerais sobre o turno

3. Clique em **"Enviar Relatório"** para salvar

### Visualizando Relatórios

1. Clique na aba **"Relatórios"** para ver todos os relatórios criados
2. Cada relatório mostra:
   - ID do relatório e nome do operador
   - Data de criação
   - Tipo de turno e data/hora do turno
   - Botões para ações

### Gerando PDF

1. Na lista de relatórios, clique em **"Baixar PDF"** no relatório desejado
2. O arquivo PDF será baixado automaticamente
3. O PDF contém todas as informações formatadas para impressão

## API Endpoints

A aplicação oferece os seguintes endpoints REST:

- `POST /api/reports/` - Criar novo relatório
- `GET /api/reports/` - Listar todos os relatórios (com paginação)
- `GET /api/reports/<id>` - Obter relatório específico
- `GET /api/reports/<id>/pdf` - Gerar e baixar PDF do relatório

## Estrutura do Banco de Dados

### Tabela: reports

| Campo | Tipo | Descrição |
|-------|------|-----------|
| id | Integer | ID único do relatório (chave primária) |
| operator_name | String(100) | Nome do operador |
| shift_type | String(50) | Tipo de turno |
| shift_date | DateTime | Data e hora do turno |
| activities | Text | Atividades realizadas |
| occurrences | Text | Ocorrências (opcional) |
| observations | Text | Observações (opcional) |
| created_at | DateTime | Data de criação do registro |

## Personalização

### Modificando Estilos

Os estilos CSS estão incorporados no arquivo `src/static/index.html`. Para personalizar:

1. Localize a seção `<style>` no início do arquivo
2. Modifique as variáveis CSS em `:root` para alterar cores:
   - `--accent`: Cor principal (laranja)
   - `--bg`: Cor de fundo
   - `--muted`: Cor de fundo secundária
   - `--ink`: Cor do texto

### Adicionando Novos Campos

Para adicionar novos campos ao relatório:

1. Modifique o modelo em `src/models/report.py`
2. Adicione os campos no formulário HTML em `src/static/index.html`
3. Atualize a lógica JavaScript para incluir os novos campos
4. Modifique a geração de PDF em `src/routes/report.py`

## Segurança

- A aplicação usa CORS habilitado para desenvolvimento
- Para produção, configure adequadamente as origens permitidas
- O banco SQLite é adequado para desenvolvimento e pequenas aplicações
- Para produção, considere migrar para PostgreSQL ou MySQL

## Suporte

Para dúvidas ou problemas:

1. Verifique se todas as dependências estão instaladas
2. Confirme que o Python 3.11+ está sendo usado
3. Verifique se a porta 5000 não está sendo usada por outro processo

## Licença

Este projeto foi desenvolvido para aprendizagem e conhecimento, podendo ser usado para fins educacionais.

