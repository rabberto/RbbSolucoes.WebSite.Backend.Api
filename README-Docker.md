# RBB Soluções Website Backend API - Docker Setup 

Este repositório contém a API backend do website RBB Soluções com configuração Docker.

## 🐳 Executando com Docker

### Opção 1: Apenas API (MongoDB Local)

Se você já tem MongoDB rodando localmente:

```bash
# Construir e executar apenas a API
docker-compose up --build

# Executar em background
docker-compose up -d --build

# Parar o serviço
docker-compose down
```

### Opção 2: API + MongoDB (Completo)

Se você quiser subir MongoDB junto:

```bash
# Usar o docker-compose completo
docker-compose -f docker-compose.full.yml up --build

# Executar em background
docker-compose -f docker-compose.full.yml up -d --build

# Parar os serviços
docker-compose -f docker-compose.full.yml down

# Parar e remover volumes (dados do MongoDB)
docker-compose -f docker-compose.full.yml down -v
```

### Opção 3: Docker Build Manual

Se você quiser apenas a API:

```bash
# Construir a imagem
docker build -t rbb-website-api .

# Executar o container (conecta ao MongoDB local)
docker run -p 5045:8080 --network host --name rbb-api rbb-website-api
```

## 🔗 Endpoints Disponíveis

### Configuração Atual (Apenas API):
- **API**: http://localhost:5045
- **MongoDB**: Seu container local existente

### Configuração Completa (docker-compose.full.yml):
- **API**: http://localhost:5045
- **MongoDB**: localhost:27017
- **Mongo Express (Web UI)**: http://localhost:8081
  - Usuário: `admin`
  - Senha: `admin123`

## ⚙️ Configuração

### Variáveis de Ambiente

O Docker Compose define as seguintes variáveis:

- `ASPNETCORE_ENVIRONMENT=Development`
- `ASPNETCORE_URLS=http://+:8080`

### Conectando ao MongoDB Local

A configuração atual (`docker-compose.yml`) usa `network_mode: host`, que permite que o container acesse seu MongoDB local diretamente através de `localhost:27017`.

Certifique-se de que sua string de conexão no `appsettings.json` esteja configurada para:
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017/rbbsolucoes"
  }
}
```

### MongoDB (se usar docker-compose.full.yml)

- **Host**: `mongodb` (dentro do container) / `localhost:27017` (externo)
- **Usuário**: `admin`
- **Senha**: `password123`
- **Database**: `rbbsolucoes`

## 📁 Estrutura dos Arquivos Docker

- `Dockerfile`: Configuração para construir a imagem da API
- `docker-compose.yml`: Apenas a API (conecta ao MongoDB local)
- `docker-compose.full.yml`: API + MongoDB + Mongo Express completo
- `.dockerignore`: Arquivos ignorados durante o build
- `docker/mongo-init.js`: Script de inicialização do MongoDB (usado no full)

## 🛠️ Desenvolvimento

Para desenvolvimento local, você pode:

1. Usar o docker-compose atual que conecta ao seu MongoDB local:
```bash
docker-compose up --build
```

2. Ou executar apenas o container da API:
```bash
docker run -p 5045:8080 --network host rbb-website-api
```

3. Alternativamente, usar o docker-compose completo se precisar de um ambiente isolado:
```bash
docker-compose -f docker-compose.full.yml up --build
```

## 🏗️ Build de Produção

Para produção, ajuste as variáveis de ambiente no `docker-compose.yml`:

```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Production
  - ConnectionStrings__MongoDB=mongodb://mongodb:27017/rbbsolucoes
```

## 📋 Logs

Para visualizar logs:

```bash
# Logs de todos os serviços
docker-compose logs -f

# Logs apenas da API
docker-compose logs -f rbb-api

# Logs do MongoDB
docker-compose logs -f mongodb
```

## 🔧 Troubleshooting

Se encontrar problemas:

1. Verifique se as portas estão disponíveis
2. Execute `docker-compose down -v` para limpar volumes
3. Reconstrua as imagens: `docker-compose build --no-cache`
4. Verifique os logs para erros específicos
