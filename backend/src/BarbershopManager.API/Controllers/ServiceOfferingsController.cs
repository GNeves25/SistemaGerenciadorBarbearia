using BarbershopManager.Application.DTOs;
using BarbershopManager.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarbershopManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ServiceOfferingsController : ControllerBase
{
    private readonly ServiceOfferingService _service;

    public ServiceOfferingsController(ServiceOfferingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceOfferingDto>>> GetAsync(CancellationToken cancellationToken)
        => Ok(await _service.GetAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceOfferingDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceOfferingDto>> CreateAsync([FromBody] ServiceOfferingDto dto, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(dto with { Id = Guid.Empty }, cancellationToken);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ServiceOfferingDto dto, CancellationToken cancellationToken)
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
