# FCG Cloud Games - Payment Microservice

<div align="center">
  
![AWS](https://img.shields.io/badge/AWS-232F3E?style=for-the-badge&logo=amazon-aws&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQS](https://img.shields.io/badge/AWS_SQS-FF4F8B?style=for-the-badge&logo=amazon-aws&logoColor=white)
![Elasticsearch](https://img.shields.io/badge/Elasticsearch-005571?style=for-the-badge&logo=elasticsearch&logoColor=white)
![Prometheus](https://img.shields.io/badge/Prometheus-E6522C?style=for-the-badge&logo=prometheus&logoColor=white)

**Projeto de Pós-Graduação FIAP**

</div>

## 📋 Sobre o Projeto

O **FCG Cloud Games - Payment Microservice** é um microserviço desenvolvido para a pós-graduação da FIAP, responsável por gerenciar todo o fluxo de pagamentos da plataforma FCG Cloud Games. Este repositório implementa uma solução completa de processamento de pagamentos síncronos e assíncronos utilizando arquitetura hexagonal, padrões modernos de desenvolvimento e serviços AWS.

O objetivo principal é demonstrar a aplicação de conceitos de computação em nuvem, arquitetura serverless e integração de serviços AWS em um cenário real de aplicação.

## 🎯 Funcionalidades

- 💳 **Processamento de Pagamentos**: Gestão completa do ciclo de vida de pagamentos
- 🔄 **Pagamentos Assíncronos**: Processamento via mensageria com AWS SQS
- 📊 **Busca e Indexação**: Integração com Elasticsearch para consultas rápidas
- 🎭 **Arquitetura Hexagonal**: Separação clara entre domínio, aplicação e infraestrutura
- 🚀 **CQRS com MediatR**: Separação de comandos e queries
- 📈 **Observabilidade**: Monitoramento com Prometheus e métricas customizadas
- ⚡ **Worker Service**: Consumidor dedicado de mensagens SQS
- 🧪 **Testes Abrangentes**: Cobertura com testes unitários e de integração
- 🔒 **Validação Robusta**: Validação de dados com FluentValidation
- 🌐 **API REST**: Endpoints bem documentados para integração

## 🏗️ Arquitetura

<img width="771" height="831" alt="image" src="https://github.com/user-attachments/assets/fe41b17d-b195-44ff-adb0-b9fd2cf4731a" />

### Componentes
- API Gateway: Ponto de entrada HTTP/HTTPS para as requisições
- AWS SQS: Serviço de Mensageria da AWS para filas FIFO de pagamentos
- ElasticSearch: Serviço da AWS para armazenamento de dados
- .NET 9: Runtime da aplicação

### Visão Geral

```
┌─────────────────┐
│   API Gateway   │
└────────┬────────┘
         │
         ▼
┌─────────────────────────────────────────────────────────┐
│                    Payment API                          │
│  ┌──────────────────────────────────────────────────┐  │
│  │           Application Layer (MediatR)            │  │
│  │  ┌────────────────┐    ┌───────────────────┐    │  │
│  │  │    Commands    │    │      Queries      │    │  │
│  │  └────────────────┘    └───────────────────┘    │  │
│  └──────────────────────────────────────────────────┘  │
│  ┌──────────────────────────────────────────────────┐  │
│  │              Domain Layer                        │  │
│  │  ┌────────────┐  ┌──────────┐  ┌─────────────┐  │  │
│  │  │  Entities  │  │  Events  │  │  Services   │  │  │
│  │  └────────────┘  └──────────┘  └─────────────┘  │  │
│  └──────────────────────────────────────────────────┘  │
│  ┌──────────────────────────────────────────────────┐  │
│  │         Infrastructure Layer                     │  │
│  │  ┌──────────┐  ┌──────────────┐  ┌───────────┐  │  │
│  │  │   SQS    │  │ Elasticsearch│  │Prometheus │  │  │
│  │  └──────────┘  └──────────────┘  └───────────┘  │  │
│  └──────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
         │
         ▼
┌─────────────────┐         ┌──────────────────┐
│   AWS SQS       │────────▶│  Worker Service  │
│  (Queue)        │         │  (Consumer)      │
└─────────────────┘         └──────────────────┘
                                     │
                                     ▼
                            ┌──────────────────┐
                            │  Elasticsearch   │
                            │  (Status Update) │
                            └──────────────────┘
```
#### API Layer
- **Controllers**: Endpoints REST para operações de pagamento
- **Contracts**: Contratos de entrada e saída da API
- **Middlewares**: Tratamento de exceções e logging
- **Mappings**: Mapeamento entre DTOs e entidades

#### Application Layer
- **Commands**: Operações que modificam estado (CreatePayment, ProcessPayment)
- **UseCases**: Implementação dos casos de uso do sistema
  - CreatePayment: Criação de novo pagamento
  - GetPaymentById: Consulta de pagamento por ID
- **Contracts**: DTOs de entrada (CreatePaymentInput)
- **Validators**: Validação com FluentValidation (CreatePaymentInputValidator)

#### Domain Layer
- **Entities**:
  - `Payment`: Entidade principal com OrderId, UserId, Amount, Currency, Status, PaymentMethod e Provider
- **EntityBase**: Classe base para entidades com Id
- **IPaymentRepository**: Interface para persistência
- **IPaymentEventPublisher**: Interface para publicação de eventos

#### Infrastructure Layer - Data
- **PaymentRepository**: Implementação de persistência com Elasticsearch

#### Infrastructure Layer - IoC
- **DependencyInjection**: Registro de todas as dependências
- **ElasticSearchConfig**: Configuração do cliente Elasticsearch
- **HealthChecks**: Verificações de saúde da aplicação
- **Observability**: Configuração do OpenTelemetry
- **Pipelines**: Configuração de pipelines de comportamento (MediatR)

#### Infrastructure Layer - Messaging
  - **PaymentEventPublisher**: Publicação de eventos no AWS SQS

#### Worker Service
- **Message Consumer**: Processa mensagens da fila SQS
- **Payment Processor**: Executa o processamento de pagamento
- **Status Updater**: Atualiza status no Elasticsearch
- **Error Handler**: Gerenciamento de falhas e retry

### Fluxo de Pagamento Assíncrono

```
1. Cliente envia requisição POST /api/payment
        │
        ▼
2. PaymentController recebe a requisição
        │
        ▼
3. CreatePaymentInputValidator valida os dados
        │
        ▼
4. UseCase cria entidade Payment
        │
        ▼
5. PaymentRepository persiste no Elasticsearch
        │
        ▼
6. PaymentEventPublisher envia mensagem para SQS
        │
        ▼
7. Resposta 201 Created retornada ao cliente
        │
        ▼
8. Worker.cs (FCG.Payments.Worker) consome mensagem do SQS
        │
        ▼
9. Worker processa o pagamento
        │
        ▼
10. Worker atualiza status no Elasticsearch
        │
        ▼
11. Cliente consulta GET /api/payment/{id} para verificar status
```

## 🛠️ Tecnologias Utilizadas

### Core
- **Linguagem**: C# (.NET 9)
- **Arquitetura**: Hexagonal (Ports & Adapters)
- **Padrões**: CQRS, Mediator, Repository, Unit of Work

### Bibliotecas e Frameworks
- **MediatR**: Implementação do padrão Mediator
- **FluentValidation**: Validação de dados
- **AutoMapper**: Mapeamento de objetos
- **Serilog**: Logging estruturado

### AWS Services
- **Amazon SQS**: Fila de mensagens para processamento assíncrono
- **Elasticsearch Service**: Busca e indexação de dados
- **CloudWatch**: Logs e monitoramento

### Observabilidade
- **Prometheus**: Métricas e monitoramento
- **prometheus-net**: Cliente .NET para Prometheus
- **Custom Metrics**: Métricas de negócio personalizadas

### Testes
- **xUnit**: Framework de testes
- **FluentAssertions**: Assertions fluentes
- **Moq**: Mock de dependências
- **TestContainers**: Testes de integração com containers
- **Bogus**: Geração de dados fake

## 📦 Estrutura do Projeto

```
FCG.Payments/ (9 projetos)
│
├── 📂 FCG.Payments.Api
│   ├── 📁 Controllers/
│   │   └── PaymentController.cs
│   ├── 📁 Contracts/
│   ├── 📁 Mappings/
│   ├── 📁 Middlewares/
│   ├── 🐳 Dockerfile
│   ├── 🌐 FCGPaymentService.http
│   ├── ⚙️ Program.cs
│   └── ⚙️ appsettings.json
│
├── 📂 FCG.Payments.Application
│   ├── 📁 _Common/
│   ├── 📁 Contracts/
│   │   └── CreatePaymentInput.cs
│   ├── 📁 UseCases/
│   │   ├── CreatePayment/
│   │   ├── GetPaymentById/
│   │   └── ...
│   └── 📁 Validators/
│       └── CreatePaymentInputValidator.cs
│
├── 📂 FCG.Payments.Domain
│   ├── 📁 _Common/
│   │   └── EntityBase.cs
│   ├── 📁 Entities/
│   │   └── Payment.cs
│   ├── 📄 IPaymentEventPublisher.cs
│   └── 📄 IPaymentRepository.cs
│
├── 📂 FCG.Payments.Infra.Data
│   └── 📄 PaymentRepository.cs
│
├── 📂 FCG.Payments.Infra.IoC
│   ├── 📁 ElasticSearchConfig/
│   ├── 📁 HealthChecks/
│   ├── 📁 Observability/
│   ├── 📁 Pipelines/
│   └── 📄 DependencyInjection.cs
│
├── 📂 FCG.Payments.Infra.Messaging
│   └── 📄 PaymentEventPublisher.cs
│
├── 📂 FCG.Payments.Worker
│   ├── 📁 Connected Services/
│   ├── 📁 Properties/
│   ├── 📁 Dto/
│   ├── 🐳 Dockerfile
│   ├── ⚙️ Program.cs
│   ├── ⚙️ appsettings.json
│   └── 🔧 Worker.cs
│
├── 📂 Tests/
│   ├── 📂 FCG.Payments.IntegrationTests
│   │   ├── 📁 _Common/
│   │   ├── 📁 Controllers/
│   │   ├── 📁 Factories/
│   │   └── 📄 FcgFixture.cs
│   │
│   └── 📂 FCG.Payments.UnitTests
│       ├── 📁 _Common/
│       ├── 📁 Factories/
│       ├── 📁 UseCases/
│       ├── 📁 Validators/
│       └── 📄 FcgFixture.cs
│
└── 📄 FCG.Payments.sln
```

## 🚀 Como Usar

### Pré-requisitos

- .NET 9 SDK
- Docker e Docker Compose
- AWS CLI configurado
- Conta AWS com permissões para SQS e Elasticsearch

### Configuração

1. **Clone o repositório**
```bash
git clone https://github.com/M4theusVieir4/fcg-payment-service.git
cd fcg-payment-service
```

2. **Configure as variáveis de ambiente**
```bash
cp appsettings.example.json appsettings.json
# Edite appsettings.json com suas configurações
```

3. **Configure AWS**
```json
{
  "ElasticSearchSettings": {
    "Endpoint": "http://elasticsearch:9200",
    "AccessKey": "",
    "Secret": "",
    "Region": "us-east-1",
    "IndexName": "payments"
  },
  "AWS": {
    "SQS": {
      "PaymentsQueueUrl": "https://sqs.us-east-1.amazonaws.com/123456789012/PaymentsQueue",
      "Region": "us-east-1",
      "AccessKey": "SUA_CHAVE_AWS",
      "SecretKey": "SUA_CHAVE_SECRETA"
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

4. **Execute com Docker Compose**
```bash
docker-compose up -d
```

5. **Ou execute localmente**
```bash
# API
cd src/PaymentService.API
dotnet run

# Worker Service (em outro terminal)
cd src/PaymentService.Worker
dotnet run
```

### Endpoints da API

#### Criar Pagamento (Assíncrono)
```
curl -X 'POST' \
  'https://localhost:7157/api/payment' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "orderId": "a3c9f1d2-4b7e-4f99-9c2f-8e7a5b1d2c3f",
  "userId": "b1d2f3e4-5a6b-7c8d-9e0f-1a2b3c4d5e6f",
  "amount": 299.99,
  "currency": "EUR",
  "status": "Pending",
  "paymentMethod": "PayPal",
  "provider": "PayPal",
  "createdAt": "2025-10-04T15:30:00Z",
  "updatedAt": "2025-10-04T15:30:00Z"
}'
```
**Requisição**
```json
{
  "orderId": "a3c9f1d2-4b7e-4f99-9c2f-8e7a5b1d2c3f",
  "userId": "b1d2f3e4-5a6b-7c8d-9e0f-1a2b3c4d5e6f",
  "amount": 299.99,
  "currency": "EUR",
  "status": "Pending",
  "paymentMethod": "PayPal",
  "provider": "PayPal",
  "createdAt": "2025-10-04T15:30:00Z",
  "updatedAt": "2025-10-04T15:30:00Z"
}
```

**Resposta (201 Created)**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "orderId": "a3c9f1d2-4b7e-4f99-9c2f-8e7a5b1d2c3f",
  "userId": "b1d2f3e4-5a6b-7c8d-9e0f-1a2b3c4d5e6f",
  "amount": 299.99,
  "currency": "EUR",
  "status": "Pending",
  "paymentMethod": "PayPal",
  "provider": "PayPal",
  "createdAt": "2025-10-04T15:30:00Z",
  "updatedAt": "2025-10-04T15:30:00Z"
}
```

#### Consultar Status do Pagamento
```http
GET /api/payment/{paymentId}

curl -X 'GET' \
  'https://localhost:7157/api/payment/a3c9f1d2-4b7e-4f99-9c2f-8e7a5b1d2c3f' \
  -H 'accept: text/plain'
```

**Resposta (200 OK)**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "orderId": "a3c9f1d2-4b7e-4f99-9c2f-8e7a5b1d2c3f",
  "userId": "b1d2f3e4-5a6b-7c8d-9e0f-1a2b3c4d5e6f",
  "amount": 299.99,
  "currency": "EUR",
  "status": "Completed",
  "paymentMethod": "PayPal",
  "provider": "PayPal",
  "createdAt": "2025-10-04T15:30:00Z",
  "updatedAt": "2025-10-04T15:45:00Z"
}
```

## 📊 Métricas do Prometheus

O serviço expõe diversas métricas customizadas:

### Métricas de Negócio

```
# Total de pagamentos criados
payment_requests_total{status="pending|completed|failed"}

# Tempo de processamento de pagamento
payment_processing_duration_seconds

# Total de valor processado
payment_amount_total{currency="BRL|USD"}

# Pagamentos por método
payment_method_total{method="credit_card|pix|boleto"}

# Taxa de sucesso
payment_success_rate
```

### Métricas de Sistema

```
# Mensagens consumidas do SQS
sqs_messages_consumed_total

# Mensagens com erro
sqs_messages_error_total

# Tamanho da fila
sqs_queue_size

# Tempo de processamento do worker
worker_processing_duration_seconds

# Operações do Elasticsearch
elasticsearch_operations_total{operation="index|search|update"}
```

### Acessar Métricas

```bash
# Endpoint de métricas da API
curl http://localhost:5000/metrics

# Endpoint de métricas do Worker
curl http://localhost:5001/metrics
```

## 🧪 Testes

### Executar Todos os Testes

```bash
dotnet test
```

### Testes Unitários

```bash
cd tests/PaymentService.UnitTests
dotnet test --logger "console;verbosity=detailed"
```

### Testes de Integração

```bash
cd tests/PaymentService.IntegrationTests
dotnet test --logger "console;verbosity=detailed"
```

### Cobertura de Código

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
```

### Exemplos de Testes

#### Teste Unitário - Command Handler
```csharp
[Fact]
public async Task ShouldCreatePaymentAsync()
{
    var input = ModelFactory.CreatePaymentInput;

    var output = await UseCase.Handle(input, CancellationToken);

    output.ShouldNotBeNull();
    output.Id.ShouldNotBe(Guid.Empty);
    output.OrderId.ShouldBe(input.OrderId);
    output.UserId.ShouldBe(input.UserId);
    output.Amount.ShouldBe(input.Amount);
    output.Currency.ShouldBe(input.Currency);
    output.Status.ShouldBe(input.Status);
    output.PaymentMethod.ShouldBe(input.PaymentMethod);
    output.Provider.ShouldBe(input.Provider);
    output.CreatedAt.ShouldBe(input.CreatedAt);
    output.UpdatedAt.ShouldBe(input.UpdatedAt);

    await _paymentRepository
        .Received(1)
        .IndexAsync(
            Arg.Any<Payment>(),
            CancellationToken
        );
}
```

#### Teste de Integração - SQS
```csharp
[Fact]
public async Task SqsPublisher_ShouldPublishMessage_AndWorkerShouldConsume()
{
    // Arrange
    using var testContainer = new SqsTestContainer();
    await testContainer.StartAsync();

    // Act
    await _sqsPublisher.PublishAsync(paymentMessage);
    await Task.Delay(5000); // Aguarda processamento

    // Assert
    var payment = await _elasticsearchService.GetPaymentAsync(paymentId);
    payment.Status.Should().Be(PaymentStatus.Completed);
}
```

## 🔍 Monitoramento e Observabilidade

### Logs Estruturados (Serilog)

```csharp
Log.Information("Payment {PaymentId} created for user {UserId}", 
    paymentId, userId);

Log.Warning("Payment {PaymentId} processing delayed, attempt {Attempt}", 
    paymentId, attemptNumber);

Log.Error(ex, "Failed to process payment {PaymentId}", paymentId);
```

### Dashboards Prometheus/Grafana

Dashboards pré-configurados disponíveis em `/docs/dashboards`:
- Payment Processing Overview
- SQS Queue Monitoring
- Error Rate and Latency
- Business Metrics

### Health Checks

```http
GET /health
GET /health/ready
GET /health/live
```

## 🔒 Segurança

- ✅ Validação de entrada de dados
- ✅ Uso de IAM roles para permissões mínimas necessárias
- ✅ Emails verificados no SES para prevenir spam
- ✅ HTTPS obrigatório via API Gateway

## 🚢 Deploy

### Docker

```bash
# Build
docker build -t fcg-payment-api:latest -f Dockerfile.api .
docker build -t fcg-payment-worker:latest -f Dockerfile.worker .

# Run
docker run -p 5000:80 fcg-payment-api:latest
docker run fcg-payment-worker:latest`

### Otimizações

- Cache de consultas frequentes (Redis)
- Batch processing de mensagens SQS
- Connection pooling para Elasticsearch
- Índices otimizados no Elasticsearch
- Async/await em todas as operações I/O

## 🎓 Contexto Acadêmico

Este projeto foi desenvolvido como parte do curso de pós-graduação da FIAP, demonstrando:

- ✅ **Arquitetura Hexagonal**: Separação clara de responsabilidades
- ✅ **Event-Driven Architecture**: Processamento assíncrono com mensageria
- ✅ **Cloud-Native**: Uso de serviços gerenciados AWS
- ✅ **Observabilidade**: Métricas, logs e traces
- ✅ **DevOps**: CI/CD, containerização, IaC
- ✅ **Clean Code**: Princípios SOLID e boas práticas
- ✅ **Testing**: TDD, cobertura de testes unitários e de integração

## 🐛 Troubleshooting

### Worker não está consumindo mensagens

```bash
# Verificar permissões IAM
aws sqs get-queue-attributes --queue-url YOUR_QUEUE_URL

# Verificar logs do worker
docker logs payment-worker -f

# Verificar mensagens na fila
aws sqs get-queue-attributes --queue-url YOUR_QUEUE_URL \
  --attribute-names ApproximateNumberOfMessages
```

### Elasticsearch não está indexando

```bash
# Verificar conectividade
curl -X GET "your-elasticsearch-endpoint:9200/_cluster/health"

# Verificar índice
curl -X GET "your-elasticsearch-endpoint:9200/payments/_search"
```

### Métricas não aparecem no Prometheus

```bash
# Verificar endpoint de métricas
curl http://localhost:5000/metrics

# Verificar configuração do Prometheus
cat prometheus.yml
```

## 👥 Autores

- **Paulo** - Infraestrutura AWS (Terraform)
- **Geovanne** - Microsserviço Jogos
- **Letícia** -  Microsserviço Usuários 
- **Matheus** -  Microsserviço Pagamentos
- **Marcelo** -  Microsserviço Catálogos

**Pós-Graduação FIAP - FCG Cloud Games**

---

## 📄 Licença

Este projeto foi desenvolvido para fins acadêmicos como parte do curso de pós-graduação da FIAP.

---

<div align="center">
  <p>FIAP - Pós-Graduação em Arquitetura de Software</p>
</div>
