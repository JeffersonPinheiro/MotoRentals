# 🏍️ MotoRentals

API desenvolvida em **.NET 8** utilizando **Clean Architecture**, com suporte a **MongoDB** para persistência de dados, **RabbitMQ** para mensageria e upload de arquivos (CNH).  

O sistema implementa casos de uso de administração de motos e gerenciamento de entregadores, incluindo regras de negócio de locação, penalidades e notificações de eventos.  

---

## 📌 Tecnologias utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- [MongoDB](https://www.mongodb.com/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Docker & Docker Compose](https://docs.docker.com/)
- [Swagger / OpenAPI](https://swagger.io/)

---

## ⚙️ Pré-requisitos

Antes de iniciar, certifique-se de ter instalado:

- [Docker](https://www.docker.com/get-started) e [Docker Compose](https://docs.docker.com/compose/)
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (se quiser rodar fora do Docker)

---

## 🚀 Rodando a aplicação com Docker

A maneira mais simples é subir todos os serviços (API, MongoDB, RabbitMQ) via **Docker Compose**:

```bash
# Clonar o repositório
git clone https://github.com/JeffersonPinheiro/MotoRentals.git
cd MotoRentals/AlugarMottu

# Construir e subir os containers
docker-compose up --build
