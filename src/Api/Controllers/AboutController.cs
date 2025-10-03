using System;
using Microsoft.AspNetCore.Mvc;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.About;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Middleware;

namespace RbbSolucoes.Website.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutController(IAboutApplication aboutService) : ControllerBase
{
    [HttpGet]
    [RequireBasicAuth]
    public async Task<IActionResult> GetAll()
    {
        var aboutInfo = await aboutService.GetAllAsync();
        return Ok(aboutInfo);
    }

    [HttpGet("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> GetById(Guid id)
    {
        var aboutInfo = await aboutService.GetByIdAsync(id);
        if (aboutInfo == null)
            return NotFound();

        return Ok(aboutInfo);
    }

    [HttpPost]
    [RequireBasicAuth]
    public async Task<IActionResult> Create([FromBody] CreateUpdateAboutDto createUpdateAboutDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var responseAboutDto = await aboutService.CreateAsync(createUpdateAboutDto);

        return CreatedAtAction(nameof(GetById), new { id = responseAboutDto.Id }, responseAboutDto);
    }

    [HttpPut("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateAboutDto createUpdateAboutDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedAboutDto = await aboutService.UpdateAsync(id, createUpdateAboutDto);

        if (updatedAboutDto == null)
            return NotFound();

        return Ok(updatedAboutDto);
    }
    
    [HttpDelete("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await aboutService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
