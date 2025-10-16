using BarbershopManager.Application.DTOs;
using BarbershopManager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BarbershopManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarbersController : ControllerBase
{
    private readonly BarberService _service;

    public BarbersController(BarberService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BarberDto>>> GetAsync(CancellationToken cancellationToken)
        => Ok(await _service.GetAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<BarberDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BarberDto>> CreateAsync([FromBody] BarberDto dto, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(dto with { Id = Guid.Empty }, cancellationToken);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] BarberDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
        {
            return BadRequest("Id mismatch");
        }

        await _service.UpdateAsync(dto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
