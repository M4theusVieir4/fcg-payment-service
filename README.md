# FCP - FIAP Cloud Payments

**Microsserviço para gerenciamento de pagamentos na FGC**

---

## Resumo

Este repositório contém um microsserviço desenvolvido em .NET para o gerenciamento de **pagamentos** na FGC (FIAP Cloud Games).  
O projeto segue a **Arquitetura Hexagonal (Ports & Adapters)**, promovendo desacoplamento entre regras de negócio e infraestrutura, facilitando testes, manutenção e integração com diferentes tecnologias.  

O microsserviço utiliza **AWS SQS** para envio e consumo de mensagens de pagamento. As mensagens são processadas por um **Worker Service** responsável pelo processamento e validação dos pagamentos.

---

## Índice

- [Resumo](#resumo)  
- [Visão Geral](#visão-geral)  
- [Camadas do Projeto](#camadas-do-projeto)  
- [Estrutura de Pastas](#estrutura-de-pastas)  
- [Principais Componentes](#principais-componentes)  
- [Testes](#testes)  
- [Como Executar](#como-executar)  
- [Observações](#observações)  
- [Referências](#referências)  

---

## Visão Geral

O microsserviço é responsável pelo gerenciamento de pagamentos na FGC. Ele segue os princípios da **Arquitetura Hexagonal**, garantindo:

- Separação clara de responsabilidades  
- Testabilidade facilitada  
- Flexibilidade para integração com diferentes tecnologias  

A comunicação de pagamentos é feita via **AWS SQS**:  
o microsserviço envia mensagens de pagamento para a fila, e o **Worker Service** consome essas mensagens e processa os pagamentos.

---

## Camadas do Projeto

- **Domínio (`FCP.Payments.Domain`)**  
  Contém entidades de negócio, regras e interfaces (ports) que definem contratos essenciais, como repositórios e serviços de pagamento.

- **Aplicação (`FCP.Payments.Application`)**  
  Implementa os casos de uso (use cases), orquestrando operações do domínio e validando dados de entrada.

- **Infraestrutura (`FCP.Payments.Infrastructure.AWS`)**  
  Implementa os adaptadores para tecnologias externas, incluindo integração com **AWS SQS**, persistência e envio de mensagens.

- **API (`FCP.Payments.Api`)**  
  Camada de apresentação que expõe endpoints HTTP para requisições de pagamento e enfileiramento de mensagens.

- **Worker Service (`FCP.Payments.Worker`)**  
  Serviço que consome mensagens de pagamento da fila SQS e executa o processamento de pagamentos.

---

## Estrutura de Pastas

| Pasta | Papel na Arquitetura Hexagonal | Tipo |
|-------|-------------------------------|------|
| `FCP.Payments.Domain/` | Núcleo do domínio, entidades, regras, ports | Domínio/Port |
| `FCP.Payments.Application/` | Casos de uso e validações | Aplicação |
| `FCP.Payments.Infrastructure.AWS/` | Integrações externas e persistência | Adapter |
| `FCP.Payments.Api/` | API REST e controllers | Adapter |
| `FCP.Payments.Worker/` | Worker Service para processamento de mensagens | Adapter |

> As pastas de infraestrutura, API e Worker representam os *adapters* da arquitetura hexagonal, conectando o núcleo do domínio a tecnologias externas, filas de mensagens e interface HTTP.

> Existe também uma pasta `tests/` na raiz, contendo testes unitários e de integração organizados por contexto.

---

## Principais Componentes

- **Entidades (Domínio)**: Objetos de negócio e regras centrais do sistema  
- **Use Cases (Aplicação)**: Operações que orquestram fluxo entre domínio e adaptadores  
- **Ports (Domínio)**: Interfaces que definem contratos para comunicação com o núcleo do sistema  
- **Adapters (Infraestrutura, API e Worker)**: Implementações concretas conectando domínio a tecnologias externas (SQS, bancos, HTTP)  
- **Validações**: Garantem integridade e consistência dos dados de entrada  

---

## Testes

Os testes estão localizados na pasta `tests/` e cobrem:

- Casos de uso  
- Validações  
- Endpoints da API  
- Processamento de mensagens do Worker  

---

## Como Executar

1. Clone o repositório:
```bash
git clone https://github.com/M4theusVieir4/fcg-payment-service.git
