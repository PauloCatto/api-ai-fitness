# 🚀 AiFitnessAgent.Api

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/download)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Neon-4169E1?style=for-the-badge&logo=postgresql)](https://neon.tech)
[![Render](https://img.shields.io/badge/Render-Deployed-262626?style=for-the-badge&logo=render)](https://api-ai-fitness.onrender.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

O **AiFitnessAgent.Api** é o núcleo de processamento robusto por trás do AI Fitness Agent. Desenvolvido com **ASP.NET Core 10**, ele fornece uma infraestrutura escalável, segura e integrada com Inteligência Artificial para gerenciar planos de treino, perfis de usuários e persistência de dados.

## 🧠 Arquitetura e Decisões Técnicas

O backend foi desenhado seguindo princípios de **Clean Architecture** e **Separation of Concerns**, garantindo que a lógica de negócio seja independente da infraestrutura.

- **Web API (REST)**: Endpoints otimizados para consumo por SPAs (Angular).
- **Entity Framework Core**: Abstração de banco de dados robusta com suporte a migrações.
- **Service Layer**: Lógica de negócio encapsulada em serviços injetáveis (Auth, User, AI).
- **JWT Security**: Autenticação stateless com tokens seguros e expiração controlada.
- **BCrypt**: Hashing de senhas com salting para segurança máxima de dados.

## 🛠️ Tecnologias Utilizadas

- **Runtime**: .NET 10 (última versão estável)
- **Framework**: ASP.NET Core Web API 10
- **Banco de Dados**: PostgreSQL (Hospedado no Neon.tech)
- **ORM**: Entity Framework Core 10
- **Segurança**: JWT Bearer + BCrypt.Net
- **IA**: Integração Nativa com Google Gemini 2.5 Flash
- **Deploy**: [Render (api-ai-fitness.onrender.com)](https://api-ai-fitness.onrender.com)

## 📁 Estrutura do Projeto

```text
AiFitnessAgent.Api/
├── Controllers/       # Endpoints da API (Auth, User, Config)
├── Data/              # Contexto do Banco de Dados (AppDbContext)
├── DTOs/              # Data Transfer Objects (Request/Response)
├── Models/            # Entidades do Domínio
├── Services/          # Serviços de Negócio (AuthService, UserService)
├── Migrations/        # Histórico de Alterações do Banco
└── Program.cs         # Configuração da Injeção de Dependência e Middlewares
```

## 🚀 Como Executar

### Pré-requisitos
- .NET 10 SDK instalado.
- Instância de PostgreSQL (recomendado Neon.tech).

### 🌐 Ambiente de Produção
A API está rodando em: `https://api-ai-fitness.onrender.com`
Documentação OpenAPI: `https://api-ai-fitness.onrender.com/openapi/v1.json`

### 🔑 Variáveis de Ambiente Necessárias (Render)
Para o funcionamento correto no deploy, configure as seguintes variáveis:
- `Jwt__Key`: Sua chave secreta JWT
- `Jwt__Issuer`: api-ai-fitness
- `Jwt__Audience`: api-ai-fitness-users
- `ConnectionStrings__DefaultConnection`: String do Postgres (formato Host=...)
- `Gemini__ApiKey`: Chave do Google AI
- `FRONTEND_URL`: URL da Vercel (sem barra final)

1.  Clone o repositório.
2.  Configure a string de conexão no `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=seu-host;Database=seu-db;Username=seu-user;Password=sua-senha;SSL Mode=Require"
    }
    ```
3.  Execute as migrações para criar o banco de dados:
    ```bash
    dotnet ef database update
    ```
4.  Rode a aplicação:
    ```bash
    dotnet run
    ```
    *A API estará disponível em `http://localhost:5294`*

## 🛡️ Endpoints Principais

- `POST /api/auth/register`: Registro de novos usuários.
- `POST /api/auth/login`: Autenticação e geração de token JWT.
- `GET /api/user/profile`: Recuperação de dados do usuário logado.
- `POST /api/user/onboarding`: Configuração inicial do perfil (metas, nível, limitações).

---
Desenvolvido por **Paulo Catto** como parte do ecossistema AI Fitness Agent.
