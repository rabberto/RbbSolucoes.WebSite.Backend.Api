# 🐳 Integrando RBB API ao Docker Compose do WSL

## 📋 Passos para Integração

### 1. Copiar o arquivo YAML para o WSL

Execute no PowerShell para copiar o arquivo para seu WSL:

```powershell
# Navegar para o diretório da API
cd c:\study\RbbSolucoes.Website.Backend.Api

# Copiar arquivo para o WSL Ubuntu
wsl cp docker/rbb-api-only.yaml /home/ubuntu/docker/yamls/
# Ou se preferir com MongoDB incluído:
# wsl cp docker/rbb-api.yaml /home/ubuntu/docker/yamls/rbb-api.yaml
```

### 2. Atualizar o docker-compose.yaml principal

Adicione no seu arquivo `/home/ubuntu/docker/docker-compose.yaml`:

```yaml
include:
  - yamls/containers-hub-fraud.yaml
  - yamls/mysql.yaml
  - yamls/rbb-api-only.yaml  # <- Adicionar esta linha
  # - yamls/portainer.yaml
  # - yamls/grafana.yaml
  # ... resto dos includes
```

### 3. Construir e executar

No WSL Ubuntu:

```bash
cd /home/ubuntu/docker
docker-compose up -d rbb-website-api
```

## 🔧 Configurações Importantes

### Contexto de Build

O arquivo YAML usa o contexto `/mnt/c/study/RbbSolucoes.Website.Backend.Api` que é o caminho do WSL para seu diretório Windows.

### Redes

A API será conectada às redes:
- `local-default` - Rede padrão
- `local-database` - Para conectar com MongoDB/MySQL

### Variáveis de Ambiente

Ajuste a connection string conforme seu setup:

```yaml
environment:
  # Para MongoDB existente
  - ConnectionStrings__MongoDB=mongodb://mongodb:27017/rbbsolucoes
  
  # Para MySQL (se preferir)
  - ConnectionStrings__MySQL=Server=mysql;Database=rbbsolucoes;Uid=root;Pwd=password;
```

## 🚀 Comandos Úteis

```bash
# Subir apenas a API
docker-compose up -d rbb-website-api

# Ver logs da API
docker-compose logs -f rbb-website-api

# Reconstruir a API
docker-compose up -d --build rbb-website-api

# Parar a API
docker-compose stop rbb-website-api

# Ver status de todos os containers
docker-compose ps
```

## 🔍 Verificação

Após subir, acesse:
- **API**: http://localhost:5045
- **Health Check**: http://localhost:5045/health

## 📁 Estrutura Final

```
/home/ubuntu/docker/
├── docker-compose.yaml        # Arquivo principal (modificado)
└── yamls/
    ├── containers-hub-fraud.yaml
    ├── mysql.yaml
    ├── rbb-api-only.yaml      # Novo arquivo
    └── ... outros yamls
```

## 🔄 Alternativas de Integração

### Opção 1: API Only (Recomendado)
Use `rbb-api-only.yaml` se você já tem MongoDB rodando

### Opção 2: API + MongoDB
Use `rbb-api.yaml` se quiser um MongoDB dedicado para a API

### Opção 3: Integração Direta
Você pode adicionar o serviço diretamente no `docker-compose.yaml` principal ao invés de usar include