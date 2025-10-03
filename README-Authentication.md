# Autenticação Basic Authentication

A API agora possui autenticação básica (Basic Authentication) implementada para proteger endpoints específicos.

## Configuração

As credenciais de autenticação são configuradas nos arquivos `appsettings.json`:

### Produção (appsettings.json)
```json
{
  "basicAuth": {
    "username": "admin",
    "password": "admin123"
  }
}
```

### Desenvolvimento (appsettings.Development.json)
```json
{
  "basicAuth": {
    "username": "dev",
    "password": "dev123"
  }
}
```

## Como Proteger Endpoints

Para proteger um endpoint, adicione o atributo `[RequireBasicAuth]` no método do controller:

```csharp
[HttpPost]
[RequireBasicAuth]
public async Task<IActionResult> Create([FromBody] CreateDto dto)
{
    // Seu código aqui
}
```

## Endpoints Protegidos

Atualmente, os seguintes endpoints estão protegidos:

### AboutController
- `POST /api/about` - Criar novo registro
- `PUT /api/about/{id}` - Atualizar registro
- `DELETE /api/about/{id}` - Deletar registro

### Endpoints Públicos (sem autenticação)
- `GET /api/about` - Listar todos
- `GET /api/about/{id}` - Buscar por ID

## Como Usar no Cliente

### Usando cURL
```bash
# Exemplo para criar um novo registro
curl -X POST "https://localhost:7000/api/about" \
  -H "Authorization: Basic YWRtaW46YWRtaW4xMjM=" \
  -H "Content-Type: application/json" \
  -d '{"title": "Sobre nós", "description": "Descrição..."}'
```

### Usando JavaScript/Fetch
```javascript
const username = 'admin';
const password = 'admin123';
const credentials = btoa(`${username}:${password}`);

fetch('/api/about', {
  method: 'POST',
  headers: {
    'Authorization': `Basic ${credentials}`,
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    title: 'Sobre nós',
    description: 'Descrição...'
  })
});
```

### Usando C# HttpClient
```csharp
var client = new HttpClient();
var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("admin:admin123"));
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

var response = await client.PostAsync("/api/about", content);
```

## Swagger/OpenAPI

O Swagger UI agora inclui suporte para Basic Authentication. Você pode:

1. Acessar `/swagger`
2. Clicar no botão "Authorize" 
3. Inserir suas credenciais
4. Testar os endpoints protegidos diretamente no Swagger

## Códigos de Status

- **200/201** - Sucesso com credenciais válidas
- **401** - Não autorizado (credenciais inválidas ou ausentes)
- **400** - Requisição inválida

## Segurança

⚠️ **Importante**: 
- Use HTTPS em produção
- Altere as credenciais padrão
- Considere usar variáveis de ambiente para as credenciais
- Para maior segurança, considere implementar JWT ou OAuth2 futuramente

## Variáveis de Ambiente (Recomendado)

Para maior segurança, você pode configurar as credenciais via variáveis de ambiente:

```bash
export BASICAUTH__USERNAME=seu_usuario
export BASICAUTH__PASSWORD=sua_senha_forte
```

E no `appsettings.json`:
```json
{
  "basicAuth": {
    "username": "${BASICAUTH__USERNAME}",
    "password": "${BASICAUTH__PASSWORD}"
  }
}
```