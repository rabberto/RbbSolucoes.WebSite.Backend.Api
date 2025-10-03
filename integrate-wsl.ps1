# Script para integrar a API RBB ao Docker Compose do WSL
param(
    [switch]$WithMongoDB = $false,
    [switch]$Help = $false
)

if ($Help) {
    Write-Host "=== RBB API Integration Script ===" -ForegroundColor Green
    Write-Host ""
    Write-Host "Usage:"
    Write-Host "  .\integrate-wsl.ps1                    # API apenas (usa MongoDB existente)"
    Write-Host "  .\integrate-wsl.ps1 -WithMongoDB       # API + MongoDB dedicado"
    Write-Host "  .\integrate-wsl.ps1 -Help              # Mostra esta ajuda"
    Write-Host ""
    exit 0
}

Write-Host "=== Integrando RBB API ao WSL Docker Environment ===" -ForegroundColor Green

# Verificar se estamos no diretório correto
if (!(Test-Path "Dockerfile")) {
    Write-Error "Execute este script no diretório raiz do projeto (onde está o Dockerfile)"
    exit 1
}

# Verificar se o WSL está rodando
try {
    wsl --list --running | Out-Null
} catch {
    Write-Error "WSL não está rodando ou não está instalado"
    exit 1
}

# Escolher qual arquivo usar
$yamlFile = if ($WithMongoDB) { "rbb-api.yaml" } else { "rbb-api-only.yaml" }
$description = if ($WithMongoDB) { "API + MongoDB" } else { "API apenas" }

Write-Host "Configuração: $description" -ForegroundColor Yellow
Write-Host "Arquivo: docker/$yamlFile" -ForegroundColor Yellow

# Copiar arquivo para o WSL
Write-Host "`nCopiando arquivo YAML para o WSL..." -ForegroundColor Cyan
try {
    wsl cp "docker/$yamlFile" "/home/ubuntu/docker/yamls/"
    Write-Host "✅ Arquivo copiado com sucesso!" -ForegroundColor Green
} catch {
    Write-Error "❌ Erro ao copiar arquivo para o WSL"
    exit 1
}

# Mostrar instruções para atualizar docker-compose.yaml
Write-Host "`n=== PRÓXIMOS PASSOS ===" -ForegroundColor Magenta
Write-Host ""
Write-Host "1. Adicione esta linha ao seu /home/ubuntu/docker/docker-compose.yaml:" -ForegroundColor White
Write-Host "   include:" -ForegroundColor Gray
Write-Host "     - yamls/$yamlFile" -ForegroundColor Yellow
Write-Host ""
Write-Host "2. No WSL, execute:" -ForegroundColor White
Write-Host "   cd /home/ubuntu/docker" -ForegroundColor Gray
Write-Host "   docker-compose up -d rbb-website-api" -ForegroundColor Yellow
Write-Host ""
Write-Host "3. Acesse a API em:" -ForegroundColor White
Write-Host "   http://localhost:5045" -ForegroundColor Yellow
Write-Host ""

# Perguntar se deve mostrar o conteúdo do arquivo para facilitar edição manual
$showContent = Read-Host "Deseja ver o conteúdo do arquivo para adicionar manualmente ao docker-compose.yaml? (y/n)"
if ($showContent -eq 'y' -or $showContent -eq 'Y') {
    Write-Host "`n=== CONTEÚDO DO ARQUIVO ===" -ForegroundColor Cyan
    Get-Content "docker/$yamlFile" | Write-Host -ForegroundColor Gray
}

Write-Host "`n✅ Integração preparada com sucesso!" -ForegroundColor Green