# Sistema Gerenciador de Barbearia

Aplicação full stack construída com **ASP.NET Core 7** e **Angular 17** seguindo princípios de **Clean Architecture**, **Domain-Driven Design (DDD)** e práticas modernas de desenvolvimento. O objetivo é oferecer uma base sólida para gerenciar profissionais, serviços e agendamentos de uma barbearia.

## Visão Geral da Arquitetura

```
/workspace/SistemaGerenciadorBarbearia
├── backend
│   ├── BarbershopManager.sln
│   ├── src
│   │   ├── BarbershopManager.API           → Endpoints RESTful
│   │   ├── BarbershopManager.Application   → Casos de uso, DTOs e serviços
│   │   ├── BarbershopManager.Domain        → Entidades e regras de negócio
│   │   └── BarbershopManager.Infrastructure→ EF Core, repositórios e DI
│   └── tests
│       └── BarbershopManager.Application.Tests → Testes unitários com xUnit
└── frontend
    └── (Angular CLI project)               → Interface responsiva e testes com Jasmine/Karma
```

### Camadas principais
- **Domain**: Entidades ricas (`Barber`, `ServiceOffering`, `Appointment`) com validações encapsuladas.
- **Application**: Serviços de aplicação, DTOs e contratos de repositório que orquestram os casos de uso.
- **Infrastructure**: Implementações de repositório com Entity Framework Core e configuração de banco SQL Server.
- **API**: Controllers RESTful, Swagger/OpenAPI e configuração de DI.
- **Frontend (Angular)**: Dashboard com componentes modulares, consumo da API via `HttpClient`, formulários reativos e testes unitários.

## Pré-requisitos

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Node.js 18+](https://nodejs.org/) e [Angular CLI](https://angular.io/cli)
- SQL Server (local ou remoto). O `appsettings.json` utiliza `(localdb)\MSSQLLocalDB`, ajuste conforme necessário.

## Como executar o backend

```bash
cd backend
# Restaurar pacotes
 dotnet restore BarbershopManager.sln
# Aplicar migrações (crie-as conforme necessário)
 dotnet ef database update --project src/BarbershopManager.Infrastructure --startup-project src/BarbershopManager.API
# Executar a API
 dotnet run --project src/BarbershopManager.API
```

A API expõe endpoints sob `https://localhost:5001/api` com Swagger disponível em `/swagger` durante o desenvolvimento.

### Testes do backend

```bash
cd backend/tests/BarbershopManager.Application.Tests
 dotnet test
```

Os testes cobrem regras críticas como prevenção de conflitos de agenda.

## Como executar o frontend

```bash
cd frontend
npm install
npm run start
```

O Angular CLI servirá a aplicação em `http://localhost:4200`. Ajuste a URL da API em `src/environments/*.ts` caso execute o backend em outro host/porta.

### Testes do frontend

```bash
cd frontend
npm run test
```

Os testes usam Jasmine/Karma com `HttpClientTestingModule` para isolar chamadas HTTP e garantir a validação dos formulários.

## Endpoints principais da API

- `GET /api/barbers` – Lista barbeiros
- `POST /api/barbers` – Cria barbeiro
- `GET /api/serviceofferings` – Lista serviços
- `POST /api/appointments` – Agenda atendimento validando conflitos

## Próximos passos sugeridos

- Implementar autenticação (JWT) e autorização por perfil.
- Criar migrações e seeds automatizados para dados iniciais.
- Adicionar pipeline CI/CD com execução de testes e análise estática.

---
Feito com ♥️ utilizando Clean Architecture para acelerar a evolução do projeto.
