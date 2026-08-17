# APICep

API ASP.NET Core para consulta de CEP utilizando a BrasilAPI.

## Objetivo

Essa aplicação fornece um endpoint simples para consultar informações de endereço a partir de um CEP, retornando dados como logradouro, bairro, cidade e estado.

## Funcionalidades atuais

- Consulta de CEP via endpoint HTTP
- Validação e normalização do formato do CEP
- Integração com a BrasilAPI
- Cache em memória para otimizar requisições
- Tratamento centralizado de erros conhecidos
- Configuração external via padrão Options (ASP.NET Core)
- Documentação automática com Swagger/OpenAPI
- Logging de eventos (cache hit/miss)

## Estrutura do projeto

- **Configurations**: classes de configuração (padrão Options)
- **Controllers**: recebe as requisições HTTP
- **Services**: contém a lógica de negócio
- **Clients**: encapsula a comunicação com a BrasilAPI
- **DTOs**: representa os modelos de entrada e saída
- **Validators**: valida e normaliza os dados recebidos
- **Exceptions**: exceções customizadas
- **Loggers**: utilitários de logging
- **Middlewares**: middleware centralizado de tratamento de exceções

## Endpoint

### Buscar CEP

GET /api/cep/{cep}

Exemplo:

```bash
curl https://localhost:5001/api/cep/01001000
```

Resposta esperada (HTTP 200):

```json
{
  "cep": "01001-000",
  "logradouro": "Praça da Sé",
  "bairro": "Sé",
  "cidade": "São Paulo",
  "estado": "SP"
}
```

### Formatos aceitos

O endpoint aceita CEPs em diferentes formatos:

```text
01001000    (sem formatação)
01001-000   (com hífen)
01001 000   (com espaço)
```

### Códigos de resposta

| Status | Descrição |
|--------|----------|
| **200** | CEP encontrado com sucesso |
| **400** | CEP em formato inválido |
| **404** | CEP não encontrado |
| **500** | Erro interno da API |

Os códigos seguem a documentação da BrasilAPI para manter compatibilidade e previsibilidade.

## Tecnologias

- .NET 10
- ASP.NET Core Web API
- Swagger / OpenAPI
- HttpClient
- Memory Cache
- Options Pattern (Configuration)

## Como executar

1. Clone o repositório
2. Entre na pasta do projeto
3. Execute:

```bash
dotnet run
```

4. Acesse a documentação em:

```text
https://localhost:5001/swagger
```

## Status do projeto

O projeto está em desenvolvimento contínuo.

### Implementados ✅

- Estrutura em camadas (Controllers, Services, Clients)
- Validação e normalização de CEP
- Cache em memória
- Configuração externa via padrão Options
- Logging básico (eventos de cache)
- Tratamento centralizado de exceções
- Documentação com Swagger/OpenAPI

### Próximas melhorias 🔄

- Testes automatizados (unitários e integração)
- Padronização de respostas de erro (ProblemDetails)
- Resiliência HTTP (timeout, retry, circuit breaker)
- Expandir logging e observabilidade

## Padrão de nomenclatura

O projeto segue um padrão consistente de nomenclatura:

- **Pastas**: em inglês (Controllers, Services, Clients, DTOs, etc.)
- **Arquivos**: padrão misto (português + sufixo da classe/pasta)
  - Exemplos: `CepService.cs`, `BrasilApiClient.cs`, `CepValidator.cs`
- **Variáveis, funções e métodos**: em português
  - Exemplos: `BuscarCepAsync()`, `NormalizarCep()`, `cepNormalizado`
  - Exceção: `var` (palavra-chave) sempre mantida em inglês

Este padrão equilibra legibilidade com convenções da plataforma .NET.

## Git ignore

O projeto já conta com um arquivo de ignore para evitar versionar arquivos temporários, artefatos de build e configurações locais.

# APICep_BrasilAPI
