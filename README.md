# APICep

API ASP.NET Core para consulta de CEP utilizando a BrasilAPI.

## Objetivo

Essa aplicação fornece um endpoint simples para consultar informações de endereço a partir de um CEP, retornando dados como logradouro, bairro, cidade e estado.

## Funcionalidades atuais

- Consulta de CEP via endpoint HTTP
- Validação básica do formato do CEP
- Integração com a BrasilAPI
- Tratamento centralizado de erros conhecidos
- Documentação automática com Swagger/OpenAPI

## Estrutura do projeto

- Controllers: recebe as requisições HTTP
- Services: contém a lógica de negócio
- Clients: encapsula a comunicação com a BrasilAPI
- DTOs: representa os modelos de entrada e saída
- Validators: valida os dados recebidos

## Endpoint

### Buscar CEP

GET /api/cep/{cep}

Exemplo:

```bash
curl https://localhost:5001/api/cep/01001000
```

Resposta esperada:

```json
{
  "cep": "01001-000",
  "logradouro": "Praça da Sé",
  "bairro": "Sé",
  "cidade": "São Paulo",
  "estado": "SP"
}
```

## Tecnologias

- .NET 10
- ASP.NET Core Web API
- Swagger / OpenAPI
- HttpClient

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

O projeto está em construção. As próximas melhorias planejadas são:

- Cache
- Logging
- Testes automatizados
- Configuração externa da URL da BrasilAPI

## Melhorias sugeridas

- Melhorar a organização de nomes e classes
- Padronizar respostas de erro
- Adicionar observabilidade e logs
- Criar testes de unidade e integração

## Git ignore

O projeto já conta com um arquivo de ignore para evitar versionar arquivos temporários, artefatos de build e configurações locais.

# APICep_BrasilAPI
