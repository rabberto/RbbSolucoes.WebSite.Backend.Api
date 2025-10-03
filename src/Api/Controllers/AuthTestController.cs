using Microsoft.AspNetCore.Mvc;
using RbbSolucoes.Website.Backend.Api.Middleware;

namespace RbbSolucoes.Website.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthTestController : ControllerBase
{
    /// <summary>
    /// Endpoint público - não requer autenticação
    /// </summary>
    [HttpGet("public")]
    public IActionResult GetPublicData()
    {
        return Ok(new { 
            message = "Este é um endpoint público", 
            timestamp = DateTime.UtcNow,
            requiresAuth = false 
        });
    }

    /// <summary>
    /// Endpoint protegido - requer autenticação básica
    /// </summary>
    [HttpGet("protected")]
    [RequireBasicAuth]
    public IActionResult GetProtectedData()
    {
        var username = User.Identity?.Name ?? "Usuário desconhecido";
        
        return Ok(new { 
            message = "Este é um endpoint protegido", 
            authenticatedUser = username,
            timestamp = DateTime.UtcNow,
            requiresAuth = true 
        });
    }

    /// <summary>
    /// Endpoint para criar dados - requer autenticação
    /// </summary>
    [HttpPost("protected/create")]
    [RequireBasicAuth]
    public IActionResult CreateData([FromBody] object data)
    {
        var username = User.Identity?.Name ?? "Usuário desconhecido";
        
        return Ok(new { 
            message = "Dados criados com sucesso", 
            createdBy = username,
            data = data,
            timestamp = DateTime.UtcNow 
        });
    }

    /// <summary>
    /// Endpoint para verificar o status da autenticação
    /// </summary>
    [HttpGet("status")]
    public IActionResult GetAuthStatus()
    {
        var isAuthenticated = User.Identity?.IsAuthenticated == true;
        var username = User.Identity?.Name;

        return Ok(new { 
            isAuthenticated = isAuthenticated,
            username = username,
            timestamp = DateTime.UtcNow
        });
    }
}