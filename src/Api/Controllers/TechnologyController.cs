using System;
using Microsoft.AspNetCore.Mvc;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Technology;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Middleware;

namespace RbbSolucoes.Website.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TechnologyController(ITechnologyApplication technologyApplication) : ControllerBase
{
    [HttpGet("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await technologyApplication.GetByIdAsync(id);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [RequireBasicAuth]
    public async Task<IActionResult> GetAll()
    {
        var result = await technologyApplication.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    [RequireBasicAuth]
    public async Task<IActionResult> Create([FromBody] CreateUpdateTechnologyDto createUpdateTechnologyDto)
    {
        if (ModelState.IsValid is false)
            return BadRequest(ModelState);

        var result = await technologyApplication.CreateAsync(createUpdateTechnologyDto);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateTechnologyDto createUpdateTechnologyDto)
    {
        if (ModelState.IsValid is false)
            return BadRequest(ModelState);

        var result = await technologyApplication.UpdateAsync(id, createUpdateTechnologyDto);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await technologyApplication.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
