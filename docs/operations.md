# Operações, Deploy e Suporte

## Configuração de Ambiente

1. **Pré-requisitos**
   - Instale `.NET 7 SDK`, `Node.js 18+` e `Angular CLI`.
   - Provisionar instância do **SQL Server** (LocalDB ou remoto).
2. **Variáveis de Ambiente**
   - `ASPNETCORE_ENVIRONMENT`: `Development`, `Staging` ou `Production`.
   - `ConnectionStrings__Default`: string de conexão utilizada pelo EF Core.
   - `Jwt__Issuer`, `Jwt__Audience`, `Jwt__Secret`: parâmetros utilizados pelo `AuthService`.
3. **Configuração do Frontend**
   - Ajustar URLs de API em `frontend/src/environments/*.ts` conforme ambiente.

## Execução Local

```bash
# Backend
cd backend
 dotnet restore BarbershopManager.sln
 dotnet ef database update --project src/BarbershopManager.Infrastructure --startup-project src/BarbershopManager.API
 dotnet run --project src/BarbershopManager.API

# Frontend
cd frontend
npm install
npm run start
```

A API fica disponível em `https://localhost:5001` e o frontend em `http://localhost:4200`.

## Deploy em Produção

1. **Backend**
   - Publicar com `dotnet publish -c Release`.
   - Containerizar via Docker ou hospedar em Azure App Service/IIS.
   - Configurar migrações automáticas com `dotnet ef database update` no pipeline.
2. **Frontend**
   - `npm run build -- --configuration production`.
   - Fazer upload do artefato para CDN ou storage estático (Azure Blob Storage, Amazon S3, etc.).
   - Configurar reverse proxy (NGINX/CloudFront) apontando para a API.
3. **Banco de Dados**
   - Utilizar Azure SQL ou SQL Server gerenciado.
   - Habilitar backups automáticos e políticas de retenção.

## Monitoramento e Observabilidade

- **Logs**: Serilog (backend) com envio para Application Insights ou Elastic Stack.
- **Métricas**: latência média de API, tempo de execução de consultas, taxas de erro.
- **Alertas**: disponibilidade da API, falhas de autenticação, crescimento anormal de tempo de resposta.

## Plano de Suporte

| Nível | Responsabilidade | Tempo de Resposta |
| --- | --- | --- |
| N1 | Atendimento ao usuário final, reset de senha, dúvidas operacionais. | Até 4h úteis |
| N2 | Suporte técnico ao sistema (verificação de logs, ajustes de configuração). | Até 8h úteis |
| N3 | Equipe de desenvolvimento (correção de bugs, novas releases). | Até 24h úteis |

## Procedimento de Rollback

1. Identificar versão estável anterior (tag/release no Git).
2. Reimplantar build anterior do backend e frontend.
3. Restaurar backup de banco se houver migrações incompatíveis.
4. Comunicar stakeholders e registrar incidente.

## Plano de Continuidade

- Deploy blue/green para minimizar downtime.
- Backups diários com testes periódicos de restauração.
- Documentação de contatos críticos (provedor cloud, telecom, equipe técnica).
