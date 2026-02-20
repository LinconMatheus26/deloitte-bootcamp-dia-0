# Vale Monitoramento – API de Gestão de Ativos

API desenvolvida em ASP.NET para o controle e monitoramento de equipamentos pesados (caminhões, escavadeiras, perfuratrizes e etc.).

O sistema foi projetado com foco em:
- **Integridade de dados**
- **Alta performance**
- **Facilidade de uso para gestores de frota**

---

## Pré-requisitos

Antes de iniciar, certifique-se de ter instalado:

- **Docker Desktop** – execução do banco de dados  
- **.NET SDK** – execução da API  
- **DBeaver** – visualização do banco PostgreSQL  
- **Insomnia** – testes dos endpoints da API  

Verificar se o .NET está instalado:

```bash
dotnet --version
```

---

## Instalação dos Pacotes (.NET)

Na raiz do projeto, execute:

```bash
dotnet restore
```

Esse comando instala automaticamente todas as dependências do projeto.

Para validar o ambiente:

```bash
dotnet build
```

---

## Como Rodar o Projeto via Docker Compose

### 1 - Subir o Banco de Dados (PostgreSQL)

Na raiz do projeto, execute:

```bash
docker-compose up -d
```

Verificar se o container está ativo:

```bash
docker ps
```

### Configuração da Conexão no DBeaver

- **Host:** localhost  
- **Porta:** 5432  
- **Banco:** vale_mineracao  
- **Usuário:** vale_user  
- **Senha:** vale_pass  

---

## Execução da Aplicação

### 2️ - Criar as Tabelas no Banco (Entity Framework)

```bash
dotnet ef database update
```

---

### 3️ - Iniciar a API

```bash
dotnet run
```

- **API:** http://localhost:5000  
- **Swagger:** http://localhost:5000/swagger  

---

## Testando a API no Insomnia e Validando no DBeaver

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

Resposta esperada: `201 Created`

---

### Conferindo no DBeaver

```
Schemas → public → Tables → Equipamentos
```

O registro enviado deve aparecer na tabela.

---

## Listagem de Equipamentos (GET)

```
http://localhost:5000/api/equipamentos
```

Filtros disponíveis:

```
?codigo=VALE
?tipo=Escavadeira
?status=Operacional
```

---

## Coleção do Insomnia – Equipamentos (CRUD)

Importar no Insomnia:

1. Import / Export  
2. Import Data → From File  
3. Selecionar `insomnia-vale-equipamentos.json`

Inclui:
- POST (Create)
- GET (Read)
- PUT (Update)
- DELETE (Delete)

---

## Diferenciais Técnicos

- Enums armazenados como texto  
- Índice único para evitar duplicidade  
- Validações de negócio  
- Uso de IQueryable para performance  

---

## Estrutura do Projeto

```
Controllers/
Dtos/
Models/
Data/
```

---

## Desenvolvedor

**Lincon**  
Projeto Final – **Bootcamp Deloitte / Vale**
