# Script para atualizar a API RBB no WSL
param(
    [switch]$Help = $false,
    [switch]$Force = $false
)

if ($Help) {
    Write-Host "=== RBB API Update Script ===" -ForegroundColor Green
    Write-Host ""
    Write-Host "Usage:"
    Write-Host "  .\update-wsl.ps1                       # Atualização normal"
    Write-Host "  .\update-wsl.ps1 -Force                # Força rebuild completo (sem cache)"
    Write-Host "  .\update-wsl.ps1 -Help                 # Mostra esta ajuda"
    Write-Host ""
    Write-Host "Este script irá:"
    Write-Host "  1. Parar o container atual"
    Write-Host "  2. Reconstruir a imagem com as novas alterações"
    Write-Host "  3. Subir o container atualizado"
    Write-Host "  4. Verificar se a API está funcionando"
    Write-Host ""
    exit 0
}

Write-Host "=== Atualizando RBB API no WSL ===" -ForegroundColor Green

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

Write-Host "`n1. Parando container atual..." -ForegroundColor Cyan
try {
    wsl bash -c "cd /home/ubuntu/docker && sudo docker compose stop rbb-website-api"
    Write-Host "✅ Container parado com sucesso!" -ForegroundColor Green
} catch {
    Write-Warning "⚠️  Falha ao parar container (pode não estar rodando)"
}

Write-Host "`n2. Reconstruindo imagem..." -ForegroundColor Cyan
$buildArgs = if ($Force) { "--no-cache" } else { "" }
$buildCommand = "cd /home/ubuntu/docker && sudo docker compose build $buildArgs rbb-website-api"

try {
    wsl bash -c $buildCommand
    Write-Host "✅ Imagem reconstruída com sucesso!" -ForegroundColor Green
} catch {
    Write-Error "❌ Erro ao reconstruir imagem"
    exit 1
}

Write-Host "`n3. Subindo container atualizado..." -ForegroundColor Cyan
try {
    wsl bash -c "cd /home/ubuntu/docker && sudo docker compose up -d rbb-website-api"
    Write-Host "✅ Container iniciado com sucesso!" -ForegroundColor Green
} catch {
    Write-Error "❌ Erro ao iniciar container"
    exit 1
}

Write-Host "`n4. Aguardando API inicializar..." -ForegroundColor Cyan
Start-Sleep -Seconds 5

Write-Host "`n5. Verificando status da API..." -ForegroundColor Cyan
try {
    $response = Invoke-WebRequest -Uri "http://localhost:5045/health" -UseBasicParsing -TimeoutSec 10
    if ($response.StatusCode -eq 200) {
        Write-Host "✅ API está funcionando corretamente!" -ForegroundColor Green
        Write-Host "   Status: $($response.Content)" -ForegroundColor Gray
    } else {
        Write-Warning "⚠️  API respondeu com status: $($response.StatusCode)"
    }
} catch {
    Write-Warning "⚠️  Não foi possível verificar o health check da API"
    Write-Host "   Verifique manualmente em: http://localhost:5045/health" -ForegroundColor Gray
}

Write-Host "`n=== Verificação Final ===" -ForegroundColor Magenta
try {
    wsl bash -c "cd /home/ubuntu/docker && sudo docker compose ps rbb-website-api"
} catch {
    Write-Warning "⚠️  Não foi possível verificar status do container"
}

Write-Host "`n=== ATUALIZAÇÃO CONCLUÍDA ===" -ForegroundColor Green
Write-Host ""
Write-Host "🌐 API disponível em: http://localhost:5045" -ForegroundColor Yellow
Write-Host "🔍 Health Check: http://localhost:5045/health" -ForegroundColor Yellow
Write-Host ""
Write-Host "Para ver logs da API:" -ForegroundColor White
Write-Host "wsl bash -c `"cd /home/ubuntu/docker && sudo docker compose logs -f rbb-website-api`"" -ForegroundColor Gray
Write-Host ""