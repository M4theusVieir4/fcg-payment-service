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
- **Middlewares**: Tratamento de exceções e logging
- **Filters**: Validação e formatação de respostas

#### Application Layer
- **Commands**: Operações que modificam estado (CreatePayment, ProcessPayment)
- **Queries**: Operações de leitura (GetPayment, ListPayments)
- **Handlers**: Lógica de processamento com MediatR
- **Validators**: Validação de entrada com FluentValidation
- **DTOs**: Objetos de transferência de dados

#### Domain Layer
- **Entities**: Payment, Transaction, PaymentMethod
- **Value Objects**: Money, PaymentStatus, PaymentType
- **Domain Events**: PaymentCreated, PaymentProcessed, PaymentFailed
- **Domain Services**: PaymentProcessor, PaymentValidator
- **Interfaces**: Contratos para portas (ports)

#### Infrastructure Layer
- **Repositories**: Implementação de persistência
- **SQS Service**: Publicação e consumo de mensagens
- **Elasticsearch Service**: Indexação e busca
- **Prometheus Metrics**: Coleta e exposição de métricas
- **External APIs**: Integração com gateways de pagamento

#### Worker Service
- **Message Consumer**: Processa mensagens da fila SQS
- **Payment Processor**: Executa o processamento de pagamento
- **Status Updater**: Atualiza status no Elasticsearch
- **Error Handler**: Gerenciamento de falhas e retry

### Fluxo de Pagamento Assíncrono

```
1. Cliente solicita pagamento
        │
        ▼
2. API cria registro inicial
        │
        ▼
3. Mensagem publicada no SQS
        │
        ▼
4. Worker Service consome mensagem
        │
        ▼
5. Processamento do pagamento
        │
        ▼
6. Atualização no Elasticsearch
        │
        ▼
7. Cliente consulta status
```

## 🛠️ Tecnologias Utilizadas

### Core
- **Linguagem**: C# (.NET 8)
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
PaymentService/
├── src/
│   ├── PaymentService.API/
│   │   ├── Controllers/
│   │   │   └── PaymentsController.cs
│   │   ├── Middlewares/
│   │   │   └── ExceptionHandlingMiddleware.cs
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── PaymentService.Application/
│   │   ├── Commands/
│   │   │   ├── CreatePayment/
│   │   │   │   ├── CreatePaymentCommand.cs
│   │   │   │   ├── CreatePaymentCommandHandler.cs
│   │   │   │   └── CreatePaymentCommandValidator.cs
│   │   │   └── ProcessPayment/
│   │   │       ├── ProcessPaymentCommand.cs
│   │   │       └── ProcessPaymentCommandHandler.cs
│   │   ├── Queries/
│   │   │   ├── GetPayment/
│   │   │   │   ├── GetPaymentQuery.cs
│   │   │   │   └── GetPaymentQueryHandler.cs
│   │   │   └── ListPayments/
│   │   │       ├── ListPaymentsQuery.cs
│   │   │       └── ListPaymentsQueryHandler.cs
│   │   ├── DTOs/
│   │   ├── Mappings/
│   │   └── Interfaces/
│   │
│   ├── PaymentService.Domain/
│   │   ├── Entities/
│   │   │   ├── Payment.cs
│   │   │   └── Transaction.cs
│   │   ├── ValueObjects/
│   │   │   ├── Money.cs
│   │   │   ├── PaymentStatus.cs
│   │   │   └── PaymentMethod.cs
│   │   ├── Events/
│   │   │   ├── PaymentCreatedEvent.cs
│   │   │   └── PaymentProcessedEvent.cs
│   │   ├── Services/
│   │   │   └── PaymentDomainService.cs
│   │   └── Interfaces/
│   │       ├── IPaymentRepository.cs
│   │       └── IPaymentGateway.cs
│   │
│   ├── PaymentService.Infrastructure/
│   │   ├── Repositories/
│   │   │   └── PaymentRepository.cs
│   │   ├── Messaging/
│   │   │   ├── SqsPublisher.cs
│   │   │   └── SqsConsumer.cs
│   │   ├── Search/
│   │   │   └── ElasticsearchService.cs
│   │   ├── Metrics/
│   │   │   └── PrometheusMetrics.cs
│   │   └── ExternalServices/
│   │       └── PaymentGatewayService.cs
│   │
│   └── PaymentService.Worker/
│       ├── Workers/
│       │   └── PaymentProcessorWorker.cs
│       ├── Handlers/
│       │   └── PaymentMessageHandler.cs
│       ├── Program.cs
│       └── appsettings.json
│
├── tests/
│   ├── PaymentService.UnitTests/
│   │   ├── Application/
│   │   │   ├── Commands/
│   │   │   └── Queries/
│   │   ├── Domain/
│   │   │   ├── Entities/
│   │   │   └── Services/
│   │   └── Infrastructure/
│   │
│   └── PaymentService.IntegrationTests/
│       ├── API/
│       │   └── PaymentsControllerTests.cs
│       ├── Infrastructure/
│       │   ├── SqsTests.cs
│       │   └── ElasticsearchTests.cs
│       └── Worker/
│           └── PaymentProcessorTests.cs
│
├── docs/
│   ├── API_DOCUMENTATION.md
│   ├── ARCHITECTURE.md
│   └── DEPLOYMENT.md
│
├── docker-compose.yml
├── Dockerfile
└── README.md
```

## 🚀 Como Usar

### Pré-requisitos

- .NET 8 SDK
- Docker e Docker Compose
- AWS CLI configurado
- Conta AWS com permissões para SQS e Elasticsearch

### Configuração

1. **Clone o repositório**
```bash
git clone https://github.com/seu-usuario/fcg-payment-service.git
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
  "AWS": {
    "Region": "us-east-1",
    "SQS": {
      "QueueUrl": "https://sqs.us-east-1.amazonaws.com/123456789/payment-queue",
      "MaxMessages": 10,
      "WaitTimeSeconds": 20
    },
    "Elasticsearch": {
      "Endpoint": "https://your-elasticsearch-endpoint.us-east-1.es.amazonaws.com",
      "Index": "payments"
    }
  },
  "Prometheus": {
    "Port": 9090,
    "Endpoint": "/metrics"
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
```http
POST /api/payments
Content-Type: application/json

{
  "userId": "user-123",
  "gameId": "game-456",
  "amount": 59.90,
  "currency": "BRL",
  "paymentMethod": "credit_card",
  "paymentDetails": {
    "cardNumber": "4111111111111111",
    "cardHolderName": "João Silva",
    "expirationDate": "12/25",
    "cvv": "123"
  }
}
```

**Resposta (201 Created)**
```json
{
  "paymentId": "pay_1234567890",
  "status": "pending",
  "message": "Payment request received and queued for processing",
  "createdAt": "2025-10-05T14:30:00Z"
}
```

#### Consultar Status do Pagamento
```http
GET /api/payments/{paymentId}
```

**Resposta (200 OK)**
```json
{
  "paymentId": "pay_1234567890",
  "userId": "user-123",
  "gameId": "game-456",
  "amount": 59.90,
  "currency": "BRL",
  "status": "completed",
  "paymentMethod": "credit_card",
  "transactionId": "txn_9876543210",
  "createdAt": "2025-10-05T14:30:00Z",
  "processedAt": "2025-10-05T14:30:15Z"
}
```

#### Listar Pagamentos do Usuário
```http
GET /api/payments?userId={userId}&page=1&pageSize=20
```

#### Cancelar Pagamento
```http
POST /api/payments/{paymentId}/cancel
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
public async Task CreatePaymentCommandHandler_ShouldCreatePayment_WhenValidRequest()
{
    // Arrange
    var command = new CreatePaymentCommand { /* ... */ };
    var handler = new CreatePaymentCommandHandler(/* dependencies */);

    // Act
    var result = await handler.Handle(command, CancellationToken.None);

    // Assert
    result.Should().NotBeNull();
    result.PaymentId.Should().NotBeEmpty();
    result.Status.Should().Be(PaymentStatus.Pending);
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

✅ Validação de entrada de dados
✅ Uso de IAM roles para permissões mínimas necessárias
✅ Emails verificados no SES para prevenir spam
✅ HTTPS obrigatório via API Gateway

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

<div align="center">
  <p>Feito com ❤️ pela equipe FCG Cloud Games</p>
  <p>FIAP - Pós-Graduação em Arquitetura de Software</p>
</div>
