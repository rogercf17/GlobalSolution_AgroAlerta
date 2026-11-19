# 🌱 AgroAlerta + AgroVigia
### Monitoramento Inteligente do Campo

## 👨‍💻 Desenvolvedores

**Artur Alves Tenca - RM555271**
**João Pedro Signor Avelar - RM558375**
**Roger Cardoso Ferreira - RM557230**

**Turma: 3ESPW**

Projeto acadêmico desenvolvido para a FIAP — Global Solution 2026.

**Tema:** AgroAlerta + AgroVigia — Monitoramento Inteligente do Campo.

## 📌 Sobre o Projeto

O **AgroAlerta + AgroVigia** é uma solução de monitoramento inteligente voltada ao agronegócio, desenvolvida com o objetivo de auxiliar produtores rurais na identificação preventiva de condições ambientais que possam prejudicar suas plantações.

O sistema recebe dados de sensores simulados, armazena as informações e analisa automaticamente as condições registradas para identificar possíveis situações de risco.

A solução é dividida em dois conceitos principais:

- **AgroVigia:** responsável pelo recebimento e armazenamento das leituras dos sensores.
- **AgroAlerta:** responsável pela análise das leituras e geração automática de alertas preventivos.

## 🎯 Problema Identificado

A falta de monitoramento contínuo das condições ambientais pode dificultar a identificação antecipada de situações que afetam o desenvolvimento das plantações.

Com isso, o produtor pode demorar a perceber alterações importantes, como temperaturas elevadas e baixa umidade do solo.

A proposta busca centralizar essas informações e disponibilizar alertas que auxiliem na tomada de decisão.

## 💡 Solução Proposta

O sistema recebe informações de sensores, processa os dados e identifica situações que exigem atenção.

Atualmente, são monitoradas três variáveis:

- Temperatura ambiente.
- Umidade do ar.
- Umidade do solo.

A aplicação possui regras de análise configuradas para gerar alertas quando determinadas condições são identificadas.

**Regras demonstrativas implementadas:**

| Condição | Alerta gerado |
|---|---|
| Temperatura acima de 35 °C | Temperatura Elevada |
| Umidade do solo abaixo de 25% | Baixa umidade do solo |

*Observação: os limites são parâmetros demonstrativos do protótipo e não representam recomendações agronômicas.*

## 🏗️ Arquitetura da Solução

O fluxo principal da aplicação funciona da seguinte maneira:

1. Uma leitura de sensor é enviada à API.
2. O AgroVigia recebe os dados e verifica a existência da fazenda associada.
3. O Entity Framework Core realiza a persistência da leitura no banco de dados.
4. O serviço AgroAlerta analisa as informações recebidas.
5. Caso alguma condição de risco seja identificada, os alertas são gerados automaticamente.
6. Os alertas são armazenados e disponibilizados para consulta por meio da API REST.

### Fluxo simplificado

```text
Sensores simulados
        |
        v
   ASP.NET Core API
        |
        v
      AgroVigia
        |
        v
   Banco de Dados
     (SQLite)
        |
        v
     AgroAlerta
        |
        v
 Análise das leituras
        |
        v
 Geração de alertas
        |
        v
 Consulta via API REST
```

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Aplicação |
|---|---|
| C# | Linguagem principal |
| .NET 10 | Plataforma de desenvolvimento |
| ASP.NET Core Web API | Construção da API REST |
| Entity Framework Core | ORM e persistência de dados |
| SQLite | Banco de dados relacional |
| LINQ | Consultas e manipulação de dados |
| Swagger / OpenAPI | Documentação dos endpoints |
| xUnit | Testes unitários |
| Git e GitHub | Versionamento e hospedagem do código |

## 📂 Estrutura do Projeto

```text
AgroAlerta/
│
├── Controllers/
│   ├── FarmController.cs
│   ├── ReadingController.cs
│   └── AlertController.cs
│
├── Models/
│   ├── Farm.cs
│   ├── SensorReading.cs
│   └── Alert.cs
│
├── Interfaces/
│   └── IAlertService.cs
│
├── Services/
│   └── AlertService.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Migrations/
│
├── tests/
│   └── AgroAlerta.Tests/
│       └── UnitTest1.cs
│
├── Program.cs
├── AgroAlerta.csproj
├── .gitignore
└── README.md
```

## 🗄️ Modelagem de Dados

A aplicação trabalha com três entidades principais:

### Farm
Representa uma propriedade rural cadastrada no sistema.

Principais atributos:
- Id
- Name
- Location

### SensorReading
Representa uma leitura realizada pelos sensores.

Principais atributos:
- Id
- FarmId
- Temperature
- AirHumidity
- SoilHumidity
- RecordedAt

### Alert
Representa um alerta gerado a partir da análise das leituras.

Principais atributos:
- Id
- FarmId
- Type
- Description
- Severity
- CreatedAt

### Relacionamentos

- Uma fazenda pode possuir várias leituras de sensores.
- Uma fazenda pode possuir vários alertas.
- Cada leitura pertence a uma fazenda.
- Cada alerta pertence a uma fazenda.

## 🔌 Endpoints da API

A aplicação disponibiliza endpoints REST para gerenciamento das fazendas, leituras e alertas.

### Fazendas — `/api/farms`

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/farms` | Lista todas as fazendas |
| GET | `/api/farms/{id}` | Consulta uma fazenda pelo ID |
| POST | `/api/farms` | Cadastra uma nova fazenda |

### Leituras — `/api/readings`

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/readings` | Lista todas as leituras |
| GET | `/api/readings/{id}` | Consulta uma leitura pelo ID |
| POST | `/api/readings` | Registra uma nova leitura e executa a análise automática |

### Alertas — `/api/alerts`

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/alerts` | Lista os alertas registrados |
| GET | `/api/alerts/{id}` | Consulta um alerta pelo ID |

## 📥 Exemplo de Requisição

### Registrar leitura de sensor

**POST** `/api/readings`

```json
{
  "farmId": 1,
  "temperature": 36.5,
  "airHumidity": 25,
  "soilHumidity": 18
}
```

Nesse exemplo, a aplicação identifica duas condições:

- Temperatura acima do limite configurado.
- Umidade do solo abaixo do limite configurado.

Consequentemente, dois alertas são gerados e armazenados no banco de dados.

## 🧪 Testes Automatizados

O projeto utiliza **xUnit** para validar as regras de negócio implementadas no serviço de alertas.

Os testes unitários contemplam três cenários:

| Cenário | Resultado esperado |
|---|---|
| Temperatura elevada | Geração de alerta de temperatura |
| Baixa umidade do solo | Geração de alerta de umidade |
| Condições normais | Nenhum alerta gerado |

### Executar os testes

Na raiz do projeto, execute:

```bash
dotnet test tests/AgroAlerta.Tests/AgroAlerta.Tests.csproj
```

**Resultado validado:** 3 testes executados e 3 aprovados.

## ⚙️ Como Executar o Projeto

### Pré-requisitos

- .NET 10 SDK
- Git
- Editor de código, como Visual Studio ou Visual Studio Code

### 1. Clonar o repositório

```bash
git clone URL_DO_REPOSITORIO
```

### 2. Entrar na pasta do projeto

```bash
cd AgroAlerta
```

### 3. Restaurar as dependências

```bash
dotnet restore
```

### 4. Aplicar as migrations

```bash
dotnet ef database update
```

### 5. Executar a aplicação

```bash
dotnet run
```

### 6. Acessar a documentação

Com a aplicação em execução, acesse a interface Swagger:

```text
https://localhost:PORTA/swagger
```

Substitua `PORTA` pela porta HTTPS apresentada no terminal.

## 🗃️ Banco de Dados

O projeto utiliza SQLite para armazenar as informações de fazendas, leituras e alertas.

O Entity Framework Core é responsável pelo mapeamento objeto-relacional e gerenciamento das operações de persistência.

As migrations permitem controlar a evolução da estrutura do banco de dados.

## 📸 Evidências de Funcionamento

As evidências do projeto incluem:

- Documentação dos endpoints no Swagger.
- Cadastro de fazendas.
- Registro de leituras dos sensores.
- Geração automática de alertas.
- Consulta dos alertas registrados.
- Persistência dos dados após reinicialização da aplicação.
- Execução dos testes automatizados.

As capturas de tela podem ser consultadas na pasta de evidências do projeto.

## 🚀 Objetivos do Projeto

O AgroAlerta + AgroVigia busca demonstrar como a integração entre desenvolvimento de software, APIs REST, persistência de dados e monitoramento pode contribuir para soluções tecnológicas aplicadas ao agronegócio.

O projeto também demonstra a aplicação prática de conceitos de:

- Programação Orientada a Objetos.
- Arquitetura de serviços.
- Injeção de dependência.
- Entity Framework Core.
- Consultas com LINQ.
- Programação assíncrona.
- Testes automatizados.
- Documentação de APIs.
