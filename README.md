# Vale Monitoramento – API de Gestão de Ativos

API desenvolvida em **ASP.NET** para o controle e monitoramento de equipamentos pesados
(caminhões, escavadeiras, perfuratrizes, entre outros).

---

## Estrutura do Repositório

```
├── ValeMonitoramento/        # Projeto principal da API
├── ValeMonitoramento.test/   # Projeto de testes automatizados
└── projeto_final.sln         # Solution do projeto
```

### ValeMonitoramento
Pasta que contém o **projeto principal da API**, incluindo:
- Controllers
- Models
- DTOs
- Data (Entity Framework)
- Regras de negócio
- Integração com banco de dados

### ValeMonitoramento.test
Pasta destinada aos **testes automatizados** da aplicação, contendo:
- Testes unitários
- Testes de controllers
- Validação das regras de negócio

---

## Pré-requisitos

Antes de iniciar, certifique-se de ter instalado:

- Docker Desktop – execução do banco de dados  
- .NET SDK – execução da API  
- DBeaver – visualização do banco PostgreSQL  
- Insomnia – testes dos endpoints da API  

Verifique se o .NET está instalado:

```bash
dotnet --version
```

---

## Instalação dos Pacotes (.NET)

Na raiz do projeto, execute:

```bash
dotnet restore
```

Para validar o ambiente:

```bash
dotnet build
```

---

## Como Rodar o Projeto via Docker Compose

### 1. Subir o Banco de Dados (PostgreSQL)

```bash
docker-compose up -d
```

Verificar se o container está ativo:

```bash
docker ps
```

---

## Configuração da Conexão no DBeaver

- Host: localhost  
- Porta: 5432  
- Banco: vale_mineracao  
- Usuário: vale_user  
- Senha: vale_pass  

---

## Execução da Aplicação

### 2. Criar as Tabelas no Banco (Entity Framework)

```bash
dotnet ef database update
```

### 3. Iniciar a API

```bash
dotnet run
```

- API: http://localhost:5000  
- Swagger: http://localhost:5000/swagger  

---

## Testando a API

### Criar Equipamento (POST)

**URL**
```
http://localhost:5000/api/equipamentos
```

**Body**
```json
{
  "codigo": "VALE-ESC-797",
  "tipo": "Escavadeira",
  "modelo": "Caterpillar 374F",
  "horimetro": 1250.75,
  "statusOperacional": "Operacional",
  "dataAquisicao": "2026-02-20",
  "localizacaoAtual": "Mina de Carajás - Setor Sul"
}
```

Resposta esperada: **201 Created**

---

## Listagem de Equipamentos (GET)

```
http://localhost:5000/api/equipamentos
```

Filtros disponíveis:
- `?codigo=VALE`
- `?tipo=Escavadeira`
- `?status=Operacional`

---

## Diferenciais Técnicos

- Enums armazenados como texto
- Índice único para evitar duplicidade
- Validações de negócio
- Uso de IQueryable para melhor performance
- Separação clara entre API e testes automatizados

---

## Desenvolvedor

**Lincon**

Projeto Final – **Bootcamp Deloitte / Vale**
