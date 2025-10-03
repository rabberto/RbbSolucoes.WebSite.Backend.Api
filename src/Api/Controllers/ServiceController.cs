using System;
using Microsoft.AspNetCore.Mvc;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Services;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Middleware;

namespace RbbSolucoes.Website.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController(IServiceApplication serviceApplication) : ControllerBase
{
    [HttpGet]
    [RequireBasicAuth]
    public async Task<IActionResult> GetAll()
    {
        var services = await serviceApplication.GetAllAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> GetById(Guid id)
    {
        var service = await serviceApplication.GetByIdAsync(id);
        if (service == null)
            return NotFound();

        return Ok(service);
    }

    [HttpPost]
    [RequireBasicAuth]
    public async Task<IActionResult> Create([FromBody] CreateUpdateServiceDto createUpdateServiceDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var responseServiceDto = await serviceApplication.CreateAsync(createUpdateServiceDto);

        return CreatedAtAction(nameof(GetById), new { id = responseServiceDto.Id }, responseServiceDto);
    }

    [HttpPut("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateServiceDto createUpdateServiceDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedServiceDto = await serviceApplication.UpdateAsync(id, createUpdateServiceDto);

        if (updatedServiceDto == null)
            return NotFound();

        return Ok(updatedServiceDto);
    }

    [HttpDelete("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await serviceApplication.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
