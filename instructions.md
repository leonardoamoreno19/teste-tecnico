# API de Registro de Vendas - Cursor AI

## Visão Geral

Este documento fornece diretrizes detalhadas para a implementação de uma API de registro de vendas seguindo o padrão `DDD` e utilizando `External Identities` para referenciar entidades de outros domínios com a denormalização das descrições das entidades.

A API deve permitir operações completas de CRUD para os registros de vendas e incluir a publicação de eventos para acompanhar o ciclo de vida das vendas.

## Funcionalidades da API

A API deve ser capaz de lidar com os seguintes atributos de uma venda:

- **Número da venda**
- **Data da venda**
- **Cliente**
- **Valor total da venda**
- **Filial onde a venda foi realizada**
- **Produtos**
- **Quantidades**
- **Preços unitários**
- **Descontos**
- **Valor total de cada item**
- **Status (Cancelada/Não Cancelada)**

Além do CRUD padrão, seria um diferencial implementar a publicação de eventos para:

- `SaleCreated`
- `SaleModified`
- `SaleCancelled`
- `ItemCancelled`

> **Nota:** Não é necessário publicar os eventos em um Message Broker. O registro pode ser feito no log da aplicação ou da forma mais conveniente.

---

## Regras de Negócio

A API deve implementar as seguintes regras de negócio para descontos baseados em quantidade:

1. **Faixas de Desconto:**

   - Compras de 4 ou mais itens idênticos recebem **10% de desconto**.
   - Compras entre 10 e 20 itens idênticos recebem **20% de desconto**.

2. **Restrições:**

   - O limite máximo permitido por produto é **20 unidades**.
   - **Não é permitido desconto** para compras abaixo de **4 unidades**.

---

## Desenvolvimento

1. **Banco de Dados:** Inicie pelo banco de dados, escolhendo entre PostgreSQL (relacional) ou MongoDB (não relacional), e faça o mapeamento objeto-relacional utilizando EF Core e Automapper.
2. **Classes de Manipulação de Dados:** Implemente as classes para manipulação dos dados usando os design patterns Mediator e Automapping.
3. **Camada de Serviços:** Implemente a camada de serviços com Rebus.
4. **Regras de Negócio:** Desenvolva as regras de negócio.
5. **Registro em Application Log:** Implemente o registro em application log.

---

## Tech Stack

A API será construída utilizando as seguintes tecnologias:

### Backend:

- **.NET 8.0** ([GitHub](https://github.com/dotnet/core))
- **C#** ([GitHub](https://github.com/dotnet/csharplang))

### Testes:

- **xUnit** ([GitHub](https://github.com/xunit/xunit))

### Frontend:

- **Angular** ([GitHub](https://github.com/angular/angular))

### Banco de Dados:

- **PostgreSQL** ([GitHub](https://github.com/postgres/postgres))
- **MongoDB** ([GitHub](https://github.com/mongodb/mongo))

---

## Frameworks

### Backend:

- **Mediator** - Facilita a comunicação entre objetos desacoplados ([GitHub](https://github.com/jbogard/MediatR))
- **Automapper** - Mapeamento automático de objetos ([GitHub](https://github.com/AutoMapper/AutoMapper))
- **Rebus** - Implementação leve de Service Bus ([GitHub](https://github.com/rebus-org/Rebus))

### Testes:

- **Faker** - Geração de dados falsos para testes ([GitHub](https://github.com/bchavez/Bogus))
- **NSubstitute** - Biblioteca de mocking para testes ([GitHub](https://github.com/nsubstitute/NSubstitute))

### Banco de Dados:

- **EF Core** - ORM para .NET ([GitHub](https://github.com/dotnet/efcore))

---

## Estrutura de Pastas

A estrutura do backend segue a organização abaixo:

```
backend/
├── src/
│   ├── Ambev.DeveloperEvaluation.API/
│   │   ├── Controllers/
│   │   ├── Configurations/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   ├── Ambev.DeveloperEvaluation.Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   └── Mappings/
│   ├── Ambev.DeveloperEvaluation.Domain/
│   │   ├── Entities/
│   │   │   └── User.cs
│   │   ├── Interfaces/
│   │   └── Services/
│   ├── Ambev.DeveloperEvaluation.Infrastructure/
│   │   ├── Context/
│   │   ├── Repositories/
│   │   └── Migrations/
│   └── Ambev.DeveloperEvaluation.Tests/
│       ├── Integration/
│       └── Unit/
├── Dockerfile
├── docker-compose.yml
└── docker-compose.override.yml
```

---

## Considerações Finais

Este documento serve como guia para a implementação da API de vendas, garantindo a padronização e conformidade com as práticas recomendadas. A API deve ser desenvolvida considerando princípios de **DDD**, **boas práticas de desenvolvimento**, e **alta testabilidade**. Qualquer modificação ou melhoria deve ser alinhada com a equipe para garantir a coesão do projeto.
