# Visão Geral do Produto

O **Barbershop Manager** é um sistema web full stack que digitaliza o atendimento de barbearias, garantindo controle sobre profissionais, serviços e agenda. A solução foi construída com **ASP.NET Core 7** no backend e **Angular 17** no frontend, seguindo princípios de Clean Architecture e Domain-Driven Design (DDD).

## Personas

- **Proprietário da barbearia**: acompanha indicadores e configura políticas (horários, duração padrão, serviços).
- **Recepcionista**: realiza cadastros, agenda atendimentos e atende os clientes presencialmente ou por telefone.
- **Profissional (barbeiro)**: consulta a própria agenda, confirma presença e registra observações pós-atendimento.
- **Cliente final**: recebe confirmações e lembretes automáticos, podendo futuramente autoagendar atendimentos.

## Objetivos Estratégicos

1. **Eliminação de agendas manuais**: centralizar todos os atendimentos na plataforma, reduzindo conflitos e falhas de comunicação.
2. **Visibilidade operacional**: fornecer dashboards e relatórios para decisões sobre escala, capacidade e faturamento.
3. **Escalabilidade tecnológica**: permitir evolução incremental (ex.: autenticação, pagamentos, fidelidade) sem reescrita de código.
4. **Experiência do usuário consistente**: interface responsiva, com feedback imediato e validações preventivas.

## Escopo Atual

- Cadastro e gestão de barbeiros, serviços e atendimentos.
- Regras de negócio para prevenir conflitos de agenda por profissional.
- API RESTful documentada com Swagger/OpenAPI.
- Frontend Angular com formulários reativos, interceptores de autenticação e guarda de rotas.

## Roadmap de Evolução

| Horizonte | Iniciativas |
| --- | --- |
| Curto prazo | Implementar autenticação JWT end-to-end, adicionar testes E2E e seed inicial de dados. |
| Médio prazo | Habilitar autoagendamento pelo cliente, enviar notificações (email/SMS) e gerar relatórios de desempenho. |
| Longo prazo | Integrar com sistemas de pagamento e programas de fidelidade, ampliar multilojas e analytics avançado. |
