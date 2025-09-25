
---

# Ambiente de Desenvolvimento Docker

Este projeto fornece um ambiente Docker para desenvolvimento com **MongoDB (replica set), Postgres, MinIO e RabbitMQ**, configurado via `.env` para fácil customização.

---

## Pré-requisitos

* [Docker](https://docs.docker.com/get-docker/) >= 20.x
* [Docker Compose](https://docs.docker.com/compose/install/) >= 1.29.x
* Sistema operacional: Windows, Linux ou macOS

---

## Estrutura do projeto

```text
.
├── .env
├── docker-compose.yml
├── volumes/ (armazenamento persistente)
└── scripts/ (opcional: scripts de inicialização do MongoDB)
```

---

## Configuração do `.env`

O arquivo `.env` define variáveis de ambiente para serviços e portas. Exemplo:

```dotenv
# MongoDB
MONGO_REPLICA_SET=rs0
MONGO_INITDB_ROOT_USERNAME=root
MONGO_INITDB_ROOT_PASSWORD=root
MONGO1_PORT=27017
MONGO2_PORT=27018
MONGO3_PORT=27019

# Postgres
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
POSTGRES_DB=postgres
POSTGRES_PORT=5432

# MinIO
MINIO_ROOT_USER=minioadmin
MINIO_ROOT_PASSWORD=minioadmin
MINIO_API_PORT=9000
MINIO_CONSOLE_PORT=9001

# RabbitMQ
RABBITMQ_USER=admin
RABBITMQ_PASS=admin
RABBITMQ_AMQP_PORT=5672
RABBITMQ_MANAGEMENT_PORT=15672
```

> **Dica:** Modifique apenas o `.env` para alterar portas, usuários ou senhas. O `docker-compose.yml` utiliza essas variáveis.

---

## Serviços incluídos

| Serviço   | Imagem                | Porta padrão | Descrição                                |
| --------- | --------------------- | ------------ | ---------------------------------------- |
| MongoDB 1 | mongo:6.0             | 27017        | Membro primário do replica set           |
| MongoDB 2 | mongo:6.0             | 27018        | Membro secundário do replica set         |
| MongoDB 3 | mongo:6.0             | 27019        | Membro arbitro do replica set            |
| Postgres  | postgres:15           | 5432         | Banco de dados relacional                |
| MinIO     | minio/minio           | 9000         | Armazenamento de objetos (S3 compatível) |
| RabbitMQ  | rabbitmq:3-management | 5672         | Message broker AMQP com painel web       |

---

## Como usar

1. **Configurar `.env`**
   Copie `.env.example` para `.env` e ajuste conforme necessário:

   ```bash
   cp .env.example .env
   ```

2. **Subir os containers**

   ```bash
   docker-compose up -d
   ```

3. **Verificar logs**

   ```bash
   docker-compose logs -f mongo1
   docker-compose logs -f mongo2
   docker-compose logs -f mongo3
   ```

4. **Acessar serviços**

   * MinIO Web Console: `http://localhost:${MINIO_CONSOLE_PORT}`
   * RabbitMQ Management: `http://localhost:${RABBITMQ_MANAGEMENT_PORT}`
   * MongoDB: `mongodb://root:root@localhost:27017/?replicaSet=rs0`
   * Postgres: `postgres://postgres:postgres@localhost:5432/postgres`

---

## Volumes

O ambiente utiliza volumes nomeados para persistência:

* `mongo1_data`, `mongo2_data`, `mongo3_data`: dados do MongoDB
* `mongo1_config`, `mongo2_config`, `mongo3_config`: configuração do MongoDB
* `postgres-data`: dados do Postgres
* `minio_data`: dados do MinIO

> Não é necessário recriar os volumes manualmente. O Docker Compose cria automaticamente.

---

## Reinicializar o ambiente

Para parar e remover containers, redes e volumes:

```bash
docker-compose down -v
```

Para subir novamente:

```bash
docker-compose up -d
```

---

## Observações

* As credenciais e portas estão todas no `.env`.
* MongoDB replica set precisa de inicialização via script (`mongo-setup`) caso queira automatizar o `rs.initiate()`.
* Todos os serviços estão na mesma rede Docker para comunicação interna.

---