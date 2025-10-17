# Requisitos do Sistema

Este documento consolida os requisitos funcionais, não funcionais e regras de negócio do **Barbershop Manager**. Cada requisito está organizado por módulo para facilitar rastreabilidade com o código fonte.

## Requisitos Funcionais

### RF-01 — Gestão de Profissionais
- Cadastrar barbeiros com nome, e-mail, telefone e especialidade.
- Editar e remover barbeiros existentes.
- Listar barbeiros com busca por nome/especialidade.

### RF-02 — Catálogo de Serviços
- Cadastrar serviços com título, descrição, duração padrão e preço.
- Editar e desativar serviços que não são mais ofertados.
- Listar serviços disponíveis para seleção na agenda.

### RF-03 — Agenda de Atendimentos
- Criar atendimentos associando barbeiro, serviço, cliente, data/hora e duração.
- Atualizar atendimentos existentes ajustando horário, duração e observações.
- Cancelar atendimentos preservando histórico.
- Exibir agenda diária/semana por barbeiro.

### RF-04 — Autenticação e Autorização
- Realizar login com credenciais válidas e gerar token JWT.
- Proteger rotas sensíveis no frontend com guarda baseada em autenticação.
- Expirar sessão do usuário após período configurável.

### RF-05 — Experiência do Usuário
- Validar formulários no frontend antes do envio à API.
- Fornecer feedback visual de sucesso/erro em cada operação.
- Garantir responsividade em desktop, tablet e mobile.

## Regras de Negócio

### RN-01 — Dados Obrigatórios
- Nome e e-mail são obrigatórios no cadastro de barbeiro.
- Nome do cliente, barbeiro, serviço e duração são obrigatórios no agendamento.

### RN-02 — Conflito de Agenda
- Um barbeiro não pode possuir dois atendimentos sobrepostos no tempo.
- A duração mínima de um atendimento é maior que zero minutos.

### RN-03 — Consistência de Referências
- Não é permitido criar agendamento para barbeiro ou serviço inexistente.
- Exclusão de barbeiros ou serviços vinculados à agenda deve ser validada.

## Requisitos Não Funcionais

### RNF-01 — Arquitetura e Tecnologia
- Backend em **ASP.NET Core 7**, com camadas API, Application, Domain e Infrastructure separadas.
- Persistência de dados via **Entity Framework Core** e banco **SQL Server**.
- Frontend em **Angular 17**, utilizando **RxJS** e **Angular Material**.

### RNF-02 — Qualidade e Testes
- Testes unitários de regras críticas (ex.: conflitos de agenda) com **xUnit**.
- Testes de frontend com **Jasmine/Karma**.
- Cobertura mínima de 70% para módulos críticos (agenda e autenticação).

### RNF-03 — Segurança
- Utilizar tokens JWT para autenticação de API.
- Proteger dados sensíveis (senhas) com hashing e armazenamento seguro.
- Garantir comunicação HTTPS em ambientes de produção.

### RNF-04 — Observabilidade
- Logging estruturado no backend (ex.: Serilog) com correlação por request.
- Monitoramento básico (latência, throughput, erros) via Application Insights ou equivalente.
- Métricas de saúde expostas em endpoint `/health`.

### RNF-05 — Operação e Desempenho
- API deve responder em até 2s para operações síncronas sob carga moderada (50 req/min).
- Frontend precisa carregar bundle inicial abaixo de 1.5 MB em produção.
- Aplicação deve suportar crescimento linear de profissionais e atendimentos sem degradação acentuada.

## Dependências Externas

- **.NET 7 SDK** — compilação e execução do backend.
- **Node.js 18+** e **Angular CLI** — build e execução do frontend.
- **SQL Server** — banco relacional padrão do projeto.

## Rastreabilidade

| Requisito | Implementação no Código |
| --- | --- |
| RF-01 / RN-01 | `backend/src/BarbershopManager.Application/Services/BarberService.cs` — criação e atualização validam campos obrigatórios. |
| RF-03 / RN-02 | `backend/src/BarbershopManager.Application/Services/AppointmentService.cs` — impede sobreposição e duração inválida. |
| RF-04 / RNF-03 | `backend/src/BarbershopManager.Application/Services/AuthService.cs` e `frontend/src/app/core/services/auth.service.ts`. |
| RF-05 | `frontend/src/app/components/*` — formulários reativos e feedback visual em componentes como `appointment-scheduler`. |
